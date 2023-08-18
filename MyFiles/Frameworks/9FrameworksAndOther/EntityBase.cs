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

    #region 状态
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

    #region 属性
    public bool inStage = false;
    //[HideInInspector]
    public Flag flag;
    public bool invulnerable; // 无敌状态，作为擂台之王时间拥有擂台之王阵营的非擂台之王实体使用, 此状态不会受伤也不能被选中
    public bool untargetable; // 无法选取状态
    public bool active; // entity是否活跃

    //生命值
    [SerializeField] private float hp = 100f;
    [SerializeField] private float maxHp;
    public float superPlayerHp;

    //基础攻击力
    [SerializeField] private float damageBase = 5f;
    [SerializeField] private int level;
    public int maxLevel;
    [SerializeField] private Timer levelTimer;

    private Timer reputationTimer;//获取声望值计时器
    private Timer attackTimer;//攻击间隔计时器
    public string curModeName; // 当前模型的名称
    public PlayerInfoView playerView;

    /// <summary>
    /// 伤害减免，0-1，代表当前受到的伤害的百分比,0代表无敌，1代表没有护甲
    /// </summary>
    public float damageReducation_Debug;
    public int buffCount;

    [SerializeField] private float normalAttackRange;//攻击范围

    [Header("计时")]
    public Timer nearestTimer = new Timer();//寻敌计时器
    private bool freeze;
    private Timer freezeTimer;
    private bool fired;
    private Timer firedEffectTimer = new Timer(5);
    private PlayerInfo firedSource;
    private float firedDamage = 10f;
    private Timer firedTimer = new Timer(1);
    #endregion

    #region 引用的组件
    public Animator anim;
    public LauncherBase[] luncherBases; // 0冰球 1火球 2 雷电
    public LauncherBase bigFireLauncher;// 擂台之王的攻击发射器
    public MyBuffContainer buffContainer;
    public Transform hitBox;
    public BigerAnimation bigerAnimation;
    public LineRenderer lineRenderer;
    public GameObject[] effects;
    #endregion

    [HideInInspector] public Vector3 agentTarget;//出场的目标位置
    [SerializeField] public EntityBase targetEntity;


    [SerializeField]
    private float findEnemyTime = 1.5f;//指定时间遍历一次最近敌人，更换目标

    private BaseMovement baseMovement;

    //换角色动画
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

    //List数组内序号   
    public int indexList;

    // 回调
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
        // 引用组件
        BaseMovement = GetComponent<BaseMovement>();
        buffContainer = new MyBuffContainer(this);
        bigerAnimation = GetComponent<BigerAnimation>();

        freezeTimer = new Timer(0.8f);

    }

    private void Start()
    {
        // 初始化
        stateMachine.Initialize(stateExcersice);
        #region Timer初始化
        reputationTimer = new Timer(1);
        attackTimer = new Timer(1.2f);
        nearestTimer.max = findEnemyTime;//寻敌计时器
        nearestTimer.cur = 0f;


        #endregion
        OnStageChanged?.Invoke(false);
        foreach (var item in luncherBases)
        {
            item.Init(playerInfo);
        }
        bigFireLauncher.Init(PlayerInfo);
        //初始化变量
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
    /// 锻炼状态
    /// </summary>
    public void Learn()
    {
        if (Level < maxLevel) // 成长中
        {
            if (playerInfo.autoMaxLevel == -1 || playerInfo.autoMaxLevel == 1) // 直接满级
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
        else // 正在成长
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

    public bool MoveFightArea()//opendoor状态执行代码
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
        if (invulnerable && !playerInfo.alive) // 无敌状态
        {
            return;
        }

        // 自己的擂台之王，则不受除另一个擂台之王之外的任何伤害
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

                ScoreManager.instance.AddScore(level * 5, sourcePlayer); // 获取击杀声望值

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

    public bool GetTargetEntity()//获得最近攻击目标
    {
        targetEntity = EntityManager.Instance.FindClosetTarget(transform, flag == Flag.Left ? 2 : 1);

        return targetEntity != null;
    }

    /// <summary>
    /// 判断给定的entity是否在攻击范围内
    /// </summary>
    /// <param name="entityBase">给定的entity</param>
    /// <returns>是否在攻击范围内</returns>
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
    ///  获取实体能否被攻击
    /// </summary>
    /// <param name="isSuperPlayer">是否能作为擂台之王的攻击目标</param>
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

