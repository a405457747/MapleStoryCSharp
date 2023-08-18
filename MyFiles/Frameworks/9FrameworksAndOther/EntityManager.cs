using DmFramework;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : StaticMonoBehaviour<EntityManager>
{
    [Tooltip("����ʵ����ܸ��ף�����Ӫ�����")]
    private Dictionary<int, List<EntityBase>> allPlayer;
    private PlayerInfo[] superPlayer;
    public EntityBase firstPlayer;

    [Header("�������")]
    [SerializeField]
    private GameObject playerPrafab;

    public GameObject[] playerModelPrefabs; // ���ж����Ԥ�Ƽ�ģ��

    [Header("������")]
    [SerializeField]
    private List<EntitySlot> entitySlotLeft;
    [SerializeField]
    private List<EntitySlot> entitySlotRight;

    [Header("Ԥ�����")]
    public Transform[] superPoints;

    public Transform canvensTrans;
    public GameObject playerInfoViewPrefab;

    #region Property
    public Dictionary<int, List<EntityBase>> AllPlayers { get => allPlayer; set => allPlayer = value; }
    public PlayerInfo[] SuperPlayer { get => superPlayer; }
    #endregion

    public Color[] colors;

    [SerializeField]
    private Collider[] spawnArea;

    public bool active = true; // ʵ���Ƿ��Ծ

    protected override void Awake()
    {
        base.Awake();
        AllPlayers = new Dictionary<int, List<EntityBase>>
        {
            { 1, new List<EntityBase>() },
            { 2, new List<EntityBase>() }
        };
        superPlayer = new PlayerInfo[2];

        InitSlot();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// ��ָ����Ӫ������һ����ɫ
    /// </summary>
    /// <param name="flag"></param>
    public bool JoinEntity(PlayerInfo playerInfo)
    {
        if (playerInfo == null)
        {
            return false;
        }
        EntitySlot entitySlot = GetSlot(playerInfo.flag, false);
        if (entitySlot != null)
        {
            EntityBase entityBase = InstantiateCreateEntity(playerInfo);
            entityBase.transform.position = entitySlot.transform.position;
            entitySlot.haveEntity = true;
            entitySlot.entity = entityBase;
            entityBase.OnStageChanged.AddListener((inStage) =>
            {
                if (inStage)
                {
                    RemoveSlotEntity(entitySlot.id);
                }
            });
        }
        else
        {
            Debug.Log("û�п���λ��");
        }
        return true;
    }

    private bool RemoveSlotEntity(int slot)
    {
        EntitySlot entitySlot = GetSlotById(slot);
        if (entitySlot.entity != null)
        {
            entitySlot.haveEntity = false;
            entitySlot.entity = null;
            return true;

        }
        return false;
    }

    /// <summary>
    /// ����С��
    /// </summary>
    private EntityBase InstantiateCreateEntity(PlayerInfo playerInfo)
    {
        GameObject entityObject = Instantiate(playerPrafab);
        EntityBase entity = entityObject.GetComponent<EntityBase>();
        entity.active = active;
        entity.flag = (Flag)playerInfo.flag;
        entityObject.transform.parent = transform;
        entity.OnEntityDieEvent.AddListener(() => { EntityManager.Instance.RemoveEntityBase(entity); });
        SetPlayerInfoUIByEntity(entity, playerInfo);
        AddEntityBase(entity);
        entityObject.transform.forward = playerInfo.flag == 1 ? entityObject.transform.right : -entityObject.transform.right;
        return entity;
    }

    private void SetPlayerInfoUIByEntity(EntityBase entityBase, PlayerInfo playerInfo)
    {
        GameObject playerInfoObj = Instantiate(playerInfoViewPrefab, new Vector3(0, -18, 31), Quaternion.identity, canvensTrans);
        PlayerInfoView playerInfoView = playerInfoObj.GetComponent<PlayerInfoView>();
        entityBase.playerView = playerInfoView;
        playerInfoView.Model.Name = playerInfo.name;
        playerInfoView.Model.AvatarSprite = playerInfo.avatar;
        entityBase.OnLevelTimerChanged.AddListener((value, maxValue) => playerInfoView.Model.OnBarValueChanged?.Invoke(value, maxValue));
        entityBase.OnLevelChanged.AddListener((level) => playerInfoView.Model.OnLevelChanged?.Invoke(level));
        entityBase.OnPostitionChanged.AddListener((pos) => playerInfoView.Model.OnPositionChanged?.Invoke(pos));
        entityBase.OnEntityDieEvent.AddListener(() => Destroy(playerInfoObj));
        entityBase.OnHpChanged.AddListener(playerInfoView.Model.OnHpChanged.Invoke);
        entityBase.OnStageChanged.AddListener(playerInfoView.Model.OnStageChanged.Invoke);
        playerInfoView.SetImageByFlag((int)entityBase.flag);
        entityBase.SetPlayerInfo(playerInfo);
    }

    public void AddEntityBase(EntityBase entity)
    {
        AllPlayers[(int)entity.flag].Add(entity);
    }

    public void RemoveEntityBase(EntityBase entity)
    {
        if (AllPlayers[(int)entity.flag].Contains(entity))
        {
            AllPlayers[(int)entity.flag].Remove(entity);
        }
    }

    private void InitSlot()
    {
        int i = 0;
        foreach (var item in entitySlotLeft)
        {
            item.id = i++;
        }

        foreach (var item in entitySlotRight)
        {
            item.id = i++;
        }
    }

    /// <summary>
    /// ��ָ������Ӫ�����ȡһ��Slot
    /// </summary>
    /// <param name="flag">ָ������Ӫ</param>
    /// <param name="hasEntity">��Slot�Ƿ����EntityBase</param>
    /// <returns></returns>
    public EntitySlot GetSlot(int flag, bool hasEntity)
    {
        List<EntitySlot> curFlagList = flag == 1 ? entitySlotLeft : entitySlotRight;
        List<EntitySlot> list = new List<EntitySlot>();
        foreach (var item in curFlagList)
        {
            if (item.haveEntity == hasEntity)
            {
                list.Add(item);
            }
        }

        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        }
        else
        {
            return null;
        }
    }

    private EntitySlot GetSlotById(int id)
    {
        foreach (var item in entitySlotLeft)
        {
            if (item.id == id)
            {
                return item;
            }

        }

        foreach (var item in entitySlotRight)
        {
            if (item.id == id)
            {
                return item;
            }

        }
        return null;
    }

    public EntityBase GetEntityByOpenId(string openId)
    {
        EntityBase entity = null;
        foreach (var item in allPlayer)
        {
            foreach (var item2 in item.Value)
            {
                if (item2.PlayerInfo.openId == openId)
                {
                    return item2;
                }
            }
        }
        return entity;
    }

    /// <summary>
    /// ��ָ����Ӫ�����ȡһ��EntityBase
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    public EntityBase GetRandomEntityTest(int flag = -1)
    {
        if (flag == -1)
        {
            flag = Random.Range(1, 3);
        }
        EntityBase entityBase = null;
        if (AllPlayers[flag].Count > 0)
        {
            entityBase = AllPlayers[flag][Random.Range(0, AllPlayers[flag].Count)];
        }
        return entityBase;
    }

    /// <summary>
    /// ��ָ������Ӫ�����Ƿ��ϳ������ȡһ��EntityBase
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="inStage"></param>
    /// <returns></returns>
    public EntityBase GetRandomEntityWithState(int flag, bool inStage)
    {
        List<EntityBase> entitiesWithState = new List<EntityBase>();

        // ����ָ����Ӫ��ʵ���б�
        foreach (EntityBase entityBase in AllPlayers[flag])
        {
            // ���ʵ���״̬����Ҫ��������ӵ�����״̬��ʵ���б���
            if (entityBase.inStage == inStage)
            {
                entitiesWithState.Add(entityBase);
            }
        }

        if (entitiesWithState.Count > 0)
        {
            // ������״̬��ʵ���б������ѡ��һ��ʵ��
            int randomIndex = Random.Range(0, entitiesWithState.Count);
            EntityBase randomEntity = entitiesWithState[randomIndex];
            return randomEntity;
        }
        else
        {
            // û������״̬��ʵ�壬���� null
            return null;
        }
    }

    public GameObject GetRandomModel()
    {
        int modelIndex = Random.Range(0, playerModelPrefabs.Length);
        GameObject playerModel = Instantiate(playerModelPrefabs[modelIndex]);
        playerModel.name = playerModelPrefabs[modelIndex].name;
        return playerModel;
    }

    public GameObject GetRandomModel(EntityBase entityBase)
    {
        int index = entityBase.PlayerInfo.modelIndex;
        if (index == -1)
        {
            index = Random.Range(0, playerModelPrefabs.Length);
            entityBase.PlayerInfo.modelIndex = index;
        }
        GameObject playerModel = Instantiate(playerModelPrefabs[index]);
        playerModel.name = playerModelPrefabs[index].name;
        return playerModel;
    }

    public GameObject GetModelByIndex(int index)
    {
        GameObject playerModel = Instantiate(playerModelPrefabs[index]);
        playerModel.name = playerModelPrefabs[index].name;
        return playerModel;
    }

    /// <summary>
    /// ��������ָ��Transfomλ������ĳ���ʵ��
    /// </summary>
    /// <param name="orignalTrans">ָ����Transform</param>
    /// <param name="flag">��������Ӫ</param>
    /// <param name="ignore">��Ҫ���Ե�ʵ��</param>
    /// <returns></returns>
    public EntityBase FindClosetTarget(Transform orignalTrans, int flag, List<EntityBase> ignore = null, bool superPlayer = false)
    {
        List<EntityBase> aimEntitys = AllPlayers[flag];
        // ɸѡ��Ч��Entity
        List<EntityBase> onStateEntitys = aimEntitys.FindAll((entity) =>
        {
            if (entity != null && entity.IsAttackable())
            {
                return true;
            }
            return false;
        });
        EntityBase aimEntity = orignalTrans.FindClosestTransform(onStateEntitys, (entity) =>
       {
           if (entity == null || !entity.PlayerInfo.alive || (ignore != null && ignore.Contains(entity)))
           {
               return false;
           }
           return true;
       });
        return aimEntity;
    }

    public bool TrySetSuperPlayer(PlayerInfo playerInfo, out PlayerInfo oldPlayerInfo)
    {
        int flag = playerInfo.flag - 1;
        oldPlayerInfo = superPlayer[flag];
        if (GameManager.Instance.gameStateMachine.CurrentState == GameManager.Instance.superTimeState)
        {
            return false;
        }

        if (oldPlayerInfo == null || playerInfo.giftValues > oldPlayerInfo.giftValues)
        {
            superPlayer[flag] = playerInfo;
            return true;
        }

        return false;
    }

    /// <summary>
    /// �������ϵ�ʵ�����̨֮��ģʽ
    /// </summary>
    public void StartSuperTime()
    {
        bool leftSuper = superPlayer[0] != null;
        bool rightSuper = superPlayer[1] != null;

        // ��������̨֮������̨֮��ʱ��״̬
        if (leftSuper)
        {
            SetSuperTimeState(allPlayer[1]);
        }

        if (rightSuper)
        {
            SetSuperTimeState(allPlayer[2]);
        }
    }

    private void SetSuperTimeState(List<EntityBase> players)
    {
        foreach (var item in players)
        {
            if (!item.PlayerInfo.superPlayer)
            {
                item.stateMachine.ChangeState(item.stateSuperTime);
            }
            else
            {
                item.stateMachine.ChangeState(item.stateSuperPlayer);
            }
        }
    }

    public EntityBase FindAotherSuperPplayer(EntityBase superPlayerEntity)
    {
        int targetFlag = superPlayerEntity.PlayerInfo.flag == 1 ? 2 : 1;
        PlayerInfo targetPlayerInfo = superPlayer[targetFlag - 1];
        if (targetPlayerInfo != null)
        {
            return targetPlayerInfo.entity;
        }
        else
        {
            return FindClosetTarget(superPlayerEntity.transform, targetFlag, null, true);
        }
    }

    public EntityBase GetSuperPlayer(int flag)
    {
        PlayerInfo superInfo = superPlayer[flag - 1];
        if (superInfo != null)
        {
            return superInfo.entity;
        }
        return null;
    }

    public void TryReplaceFirestPlayer(EntityBase entity)
    {
        if (firstPlayer != null)
        {
            if (firstPlayer.PlayerInfo.Score < entity.PlayerInfo.Score)
            {
                firstPlayer = entity;
            }
        }
        else
        {
            firstPlayer = entity;
        }
    }

    public void HideAllPlayer()
    {
        foreach (var item in allPlayer)
        {
            foreach (var item2 in item.Value)
            {
                item2.SetVisible(false);
                item2.active = false;
            }
        }
    }
}
