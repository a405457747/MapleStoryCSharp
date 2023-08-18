using DmFramework;
using UnityEngine;
using UnityEngine.Events;

public enum Flag
{
    Left = 1,
    Right = 2
}

[RequireComponent(typeof(BaseMovement))]
public class EntityBase : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo { get => playerInfo; }

    #region ״̬
    public StateMachine stateMachine;
    public string curstate_debug;
    public EntityStateBase stateExcersice;
    public EntityStateBase stateOpenDoor;
    public EntityStateBase stateWalkAround;
    public EntityStateBase stateAttack;
    public EntityStateBase stateDied;
    public EntityStateBase stateSuperTime;
    public EntityStateBase stateSuperPlayer;
    #endregion

    #region ����
    public bool inStage = false;
    //[HideInInspector]
    public Flag flag;
    public bool invulnerable; // �޵�״̬����Ϊ��̨֮��ʱ��ӵ����̨֮����Ӫ�ķ���̨֮��ʵ��ʹ��, ��״̬��������Ҳ���ܱ�ѡ��
    public bool untargetable; // �޷�ѡȡ״̬
    public bool active; // entity�Ƿ��Ծ

    //����ֵ
    [SerializeField] private float hp = 100f;
    [SerializeField] private float maxHp;
    public float superPlayerHp;

    //����������
    [SerializeField] private float damageBase = 5f;
    [SerializeField] private int level;
    public int maxLevel;
    [SerializeField] private Timer levelTimer;

    private Timer reputationTimer;//��ȡ����ֵ��ʱ��
    private Timer attackTimer;//���������ʱ��
    public string curModeName; // ��ǰģ�͵�����
    public PlayerInfoView playerView;

    /// <summary>
    /// �˺����⣬0-1������ǰ�ܵ����˺��İٷֱ�,0�����޵У�1����û�л���
    /// </summary>
    public float damageReducation_Debug;
    public int buffCount;

    [SerializeField] private float normalAttackRange;//������Χ

    [Header("��ʱ")]
    public Timer nearestTimer = new Timer();//Ѱ�м�ʱ��
    private bool freeze;
    private Timer freezeTimer;
    private bool fired;
    private Timer firedEffectTimer = new Timer(5);
    private PlayerInfo firedSource;
    private float firedDamage = 10f;
    private Timer firedTimer = new Timer(1);
    #endregion

    #region ���õ����
    public Animator anim;
    public LauncherBase[] luncherBases; // 0���� 1���� 2 �׵�
    public LauncherBase bigFireLauncher;// ��̨֮���Ĺ���������
    public MyBuffContainer buffContainer;
    public Transform hitBox;
    public BigerAnimation bigerAnimation;
    public LineRenderer lineRenderer;
    public GameObject[] effects;
    #endregion

    [HideInInspector] public Vector3 agentTarget;//������Ŀ��λ��
    [SerializeField] public EntityBase targetEntity;


    [SerializeField]
    private float findEnemyTime = 1.5f;//ָ��ʱ�����һ��������ˣ�����Ŀ��

    private BaseMovement baseMovement;

    //����ɫ����
    [SerializeField] private GameObject modelChangeLight;
    [SerializeField] private Timer modelChangeTimer;
    [SerializeField] private GameObject modelChangeEffectPrefab;

    [SerializeField] private GameObject shield;
    [SerializeField] private Transform lightShpere;


    #region Property
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            OnHpChanged?.Invoke(value / MaxHp);
        }
    }
    public int Level
    {
        get => level; set
        {
            level = value;
            OnLevelChanged?.Invoke(level);
            if (level >= maxLevel)
            {
                level = maxLevel;
                Auto2Fight();
            }
        }
    }

    public float MaxHp
    {
        get => maxHp; set
        {
            maxHp = value;
            Hp = maxHp;
        }
    }

    public float AttackRange
    {
        get
        {
            return buffContainer.attackRange.GetMaxProperty(normalAttackRange);
        }
    }

    public float DamageReducation
    {
        get
        {
            damageReducation_Debug = buffContainer.damageReduction.GetMinProperty(1);
            return damageReducation_Debug;
        }
    }
    public Animator Anim { get => anim; }
    public BaseMovement BaseMovement { get => baseMovement; set => baseMovement = value; }
    public Timer AttackTimer { get => attackTimer; }
    public bool Freeze { get => freeze; }
    #endregion

    //List���������   
    public int indexList;

    // �ص�
    [HideInInspector] public UnityEvent<int> OnLevelChanged;
    [HideInInspector] public UnityEvent<float, float> OnLevelTimerChanged;
    [HideInInspector] public UnityEvent<Vector3> OnPostitionChanged;
    [HideInInspector] public UnityEvent OnEntityDieEvent;
    [HideInInspector] public UnityEvent<bool> OnStageChanged;
    [HideInInspector] public UnityEvent<float> OnHpChanged;
    private void Awake()
    {
        stateMachine = new StateMachine();
        stateExcersice = new EntityStateExcersice(stateMachine, this);
        stateOpenDoor = new EntityStateOpenDoor(stateMachine, this);
        stateWalkAround = new EntityStateWalkAround(stateMachine, this);
        stateAttack = new EntityStateAttack(stateMachine, this);
        stateDied = new EntityStateDied(stateMachine, this);
        stateSuperPlayer = new EntityStateSuperPlayer(stateMachine, this);
        stateSuperTime = new EntityStateSuperTime(stateMachine, this);

        OnLevelChanged = new UnityEvent<int>();
        OnLevelTimerChanged = new UnityEvent<float, float>();
        OnPostitionChanged = new UnityEvent<Vector3>();
        OnEntityDieEvent = new UnityEvent();
        OnStageChanged = new UnityEvent<bool>();
        OnHpChanged = new UnityEvent<float>();
        // �������
        BaseMovement = GetComponent<BaseMovement>();
        buffContainer = new MyBuffContainer(this);
        bigerAnimation = GetComponent<BigerAnimation>();

        freezeTimer = new Timer(0.8f);

    }

    private void Start()
    {
        // ��ʼ��
        stateMachine.Initialize(stateExcersice);
        #region Timer��ʼ��
        reputationTimer = new Timer(1);
        attackTimer = new Timer(1.2f);
        nearestTimer.max = findEnemyTime;//Ѱ�м�ʱ��
        nearestTimer.cur = 0f;


        #endregion
        OnStageChanged?.Invoke(false);
        foreach (var item in luncherBases)
        {
            item.Init(playerInfo);
        }
        bigFireLauncher.Init(PlayerInfo);
        //��ʼ������
        superPlayerHp = 5000f;
    }

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        playerInfo.entity = this;
        this.playerInfo = playerInfo;
        SetModel(EntityManager.Instance.GetRandomModel(this));
        MaxHp = 100f;
        Hp = MaxHp;
        damageBase = 10f;
        maxLevel = 5;
        Level = 1;
        levelTimer = new Timer(5);
        playerInfo.alive = true;
    }

    private void SetModel(GameObject model)
    {
        model.transform.SetParent(transform);
        model.transform.localPosition = Vector3.zero;
        anim = model.GetComponent<Animator>();
        curModeName = model.name;
        bigerAnimation.meshRenderer = model.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    private void Update()
    {
        if (freeze && freezeTimer.LoopTimer())
        {
            SetFreeze(false);
        }
        if (fired && firedSource != null)
        {
            if (firedTimer.LoopTimer())
            {
                Hurt(firedDamage, firedSource);
            }
            if (firedTimer.LoopTimer())
            {
                SetFired(false, null);
            }

        }

        if (active)
        {
            stateMachine.CurrentState.LogicUpdate();
            GetScore();
            playerInfo.Update();
        }
        curstate_debug = stateMachine.CurrentState.GetType().Name;
        buffCount = buffContainer.buffs.Count;
        OnPostitionChanged?.Invoke(BaseMovement.GetTopPosition());
        buffContainer.Update();
    }
    /// <summary>
    /// ����״̬
    /// </summary>
    public void Learn()
    {
        if (Level < maxLevel) // �ɳ���
        {
            if (playerInfo.autoMaxLevel == -1 || playerInfo.autoMaxLevel == 1) // ֱ������
            {
                playerInfo.autoMaxLevel = playerInfo.autoMaxLevel == 1 ? 0 : -1;
                Level = maxLevel;
                OnLevelTimerChanged?.Invoke(levelTimer.max, levelTimer.max);
                return;
            }
            OnLevelTimerChanged?.Invoke(levelTimer.cur, levelTimer.max);
            if (levelTimer.LoopTimer())
            {
                Level++;
                UIManager.Instance.ShowLevelUp(transform.position);
            }
            else
            {
                Auto2Fight();
            }
        }
        else // ���ڳɳ�
        {
            Auto2Fight();
        }
    }

    private void Auto2Fight()
    {
        if (playerInfo.autoFight == -1 || playerInfo.autoFight == 1)
        {
            playerInfo.autoFight = playerInfo.autoFight == 1 ? 0 : -1;
            MoveFightArea();
        }
    }

    public bool MoveFightArea()//opendoor״ִ̬�д���
    {
        if (stateMachine.CurrentState != stateExcersice)
        {
            return false;
        }
        stateMachine.ChangeState(stateOpenDoor);
        OnStageChanged?.Invoke(true);
        return true;
    }
    public void Attack4Normal()
    {
        if (targetEntity != null)
        {
            targetEntity.Hurt((int)damageBase * (Random.Range(0.3f, 0.7f)), transform.position - targetEntity.transform.position, Vector3.zero, playerInfo);
        }
    }
    public bool Attack4Skills()
    {
        bool attacked = false;
        foreach (var item in luncherBases)
        {
            if (item != null)
            {
                if (targetEntity != null)
                {
                    attacked = item.AutoTrigger(targetEntity.hitBox.position) && attacked;
                }
            }
        }
        return attacked;
    }
    public void Hurt(float damage, Vector3 direction, Vector3 force, PlayerInfo sourcePlayer)
    {
        if (invulnerable && !playerInfo.alive) // �޵�״̬
        {
            return;
        }

        // �Լ�����̨֮�������ܳ���һ����̨֮��֮����κ��˺�
        if (stateMachine.CurrentState == stateSuperPlayer)
        {
            if (!sourcePlayer.superPlayer)
            {
                return;
            }
        }

        Hp -= damage;
        UIManager.Instance.ShowHurtHP(damage, this.transform.position);
        if (Hp <= 0f)
        {
            if (sourcePlayer != null)
            {
                if (playerInfo.alive)
                {
                    sourcePlayer.killCount++;
                }

                ScoreManager.instance.AddScore(level * 5, sourcePlayer); // ��ȡ��ɱ����ֵ

                if (force != Vector3.zero)
                {
                    BaseMovement.Rb.AddForce(force, ForceMode.Impulse);
                }
            }
            Die();
        }
    }
    public void Hurt(float damage, PlayerInfo playerInfo)
    {
        Hurt(damage, Vector3.zero, Vector3.zero, playerInfo);
    }
    public void Die()
    {
        if (playerInfo.alive)
        {
            playerInfo.alive = false;
            OnEntityDieEvent?.Invoke();
            stateMachine.ChangeState(stateDied);
        }
    }

    public bool GetTargetEntity()//����������Ŀ��
    {
        targetEntity = EntityManager.Instance.FindClosetTarget(transform, flag == Flag.Left ? 2 : 1);

        return targetEntity != null;
    }

    /// <summary>
    /// �жϸ�����entity�Ƿ��ڹ�����Χ��
    /// </summary>
    /// <param name="entityBase">������entity</param>
    /// <returns>�Ƿ��ڹ�����Χ��</returns>
    public bool InAttackRange(EntityBase entityBase)
    {
        if (entityBase == null) return false;
        return Vector3.Distance(transform.position, entityBase.transform.position) <= AttackRange;
    }

    private void GetScore()
    {
        if (stateMachine.CurrentState == stateSuperPlayer)
        {
            if (reputationTimer.LoopTimer())
            {
                ScoreManager.instance.AddScore(50, playerInfo);
            }
        }
        else if (stateMachine.CurrentState == stateSuperTime)
        {
            if (reputationTimer.LoopTimer())
            {
                ScoreManager.instance.AddScore(10, playerInfo);
            }
        }
        else if (inStage)
        {
            if (reputationTimer.LoopTimer())
            {
                ScoreManager.instance.AddScore(level, playerInfo);
            }
        }
    }

    /// <summary>
    ///  ��ȡʵ���ܷ񱻹���
    /// </summary>
    /// <param name="isSuperPlayer">�Ƿ�����Ϊ��̨֮���Ĺ���Ŀ��</param>
    /// <returns></returns>
    public bool IsAttackable(bool isSuperPlayer = false)
    {
        if (playerInfo.alive && !untargetable && (isSuperPlayer || inStage))
        {
            return true;
        }
        return false;
    }

    public void SetFreeze(bool isFreeze)
    {
        if (freeze != isFreeze)
        {
            bigerAnimation.ChangeIceMateral(isFreeze);
        }

        if (isFreeze)
        {
            freezeTimer.ResetTimer();
            if (stateMachine.CurrentState != stateDied)
            {
                anim.speed = 0;
            }
            else
            {
                anim.speed = 1;
            }

        }
        else
        {
            anim.speed = 1;
        }

        freeze = isFreeze;
    }

    public void SetFired(bool isFired, PlayerInfo firedSource)
    {
        if (isFired)
        {
            firedTimer.ResetTimer();
            this.firedSource = firedSource;
            firedEffectTimer.ResetTimer();
            effects[0].SetActive(true);
        }
        else
        {
            effects[0].SetActive(false);
        }

        fired = isFired;
    }

    public void SetVisible(bool visible)
    {
        bigerAnimation.meshRenderer.gameObject.SetActive(visible);
        playerView.gameObject.SetActive(visible);
    }

}

