using System.Collections.Generic;

public class DataManager : MonoSingleton<DataManager>
{
    private List<LevelData> _datas;

    /// <summary>
    ///     我的数据
    /// </summary>
    public User SelfData { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SelfData = new User
        {
            name = "我是用户",
            head = "ni"
        };

        _datas = new List<LevelData>
        {
            new LevelData
            {
                messageComments = new List<MessageComment>
                {
                    new MessageComment
                    {
                        content = "千万别分手，不要祸害别人",
                        sender = new User
                        {
                            name = "武术大队",
                            head = "touxiang3"
                        },
                        praise = 1600
                    },
                    new MessageComment
                    {
                        content = "思路清晰，非死即伤",
                        sender = new User
                        {
                            name = "阳米先生",
                            head = "touxiang2"
                        },
                        praise = 4800
                    }
                },
                selectContent = new[] {"哈哈你们太搞笑了", "关注你一星期了，果然分手了", "看来单身是需要实力的"},
                startNumber = new[] {1, 3, 2},
                messageWorksList = new List<MessageWorks>
                {
                    new MessageWorks
                    {
                        content = "宝宝,我不开心",
                        sender = new User
                        {
                            name = "女朋友",
                            head = "女头像"
                        },
                        // isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "怎么了",
                        sender = new User
                        {
                            name = "男朋友",
                            head = "男头像"
                        },
                        // isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "我做了特别恐怖的梦",
                        sender = new User
                        {
                            name = "女朋友",
                            head = "女头像"
                        }
                    },
                    new MessageWorks
                    {
                        content = "梦见狗在追着咬我",
                        sender = new User
                        {
                            name = "女朋友",
                            head = "女头像"
                        },
                        // isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "放心吧，梦是相反的你在咬狗",
                        sender = new User
                        {
                            name = "男朋友",
                            head = "男头像"
                        },
                        isImportant = true
                    },

                    new MessageWorks
                    {
                        content = "分手吧！你居然敢这样说我",
                        sender = new User
                        {
                            name = "女朋友",
                            head = "女头像"
                        }
                    },
                    new MessageWorks
                    {
                        content = "宝宝你听我解释",
                        sender = new User
                        {
                            name = "男朋友",
                            head = "男头像"
                        }
                    },
                    new MessageWorks
                    {
                        content = "我不听",
                        sender = new User
                        {
                            name = "女朋友",
                            head = "女头像"
                        },
                        // isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "我错了,我真的错了",
                        sender = new User
                        {
                            name = "男朋友",
                            head = "男头像"
                        }
                    },
                    new MessageWorks
                    {
                        content = "我开玩笑了,后悔了",
                        sender = new User
                        {
                            name = "男朋友",
                            head = "男头像"
                        }
                    },
                    new MessageWorks
                    {
                        content = "哪后悔了？",
                        sender = new User
                        {
                            name = "男朋友",
                            head = "男头像"
                        }
                    },
                    new MessageWorks
                    {
                        content = "我不应该边玩游戏边聊天,都0-8了！",
                        sender = new User
                        {
                            name = "女朋友",
                            head = "女头像"
                        },
                        isImportant = true
                    }
                },
                messageLetters = new List<MessageLetter>
                {
                    new MessageLetter
                    {
                        content = "我是消息好友1",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        }
                    },
                    new MessageLetter
                    {
                        content = "我是好友2",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        }
                    }
                },
                talkDatas = new List<TalkData>
                {
                    new TalkData
                    {
                        messageNews = new List<MessageNew>
                        {
                            new MessageNew
                            {
                                sender = new User
                                {
                                    name = "玉皇大帝",
                                    head = "touxiang-1"
                                },

                                TalkContent = new[] {"你好我是玉皇大帝第一句（0）", "你好我是玉皇大帝第一句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是玉皇大帝第二句（0）", "你好我是玉皇大帝第二句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是自己第一句（0）", "你好我是自己第一句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是自己0", "你好我是自己1"},
                                TalkScore = new[] {-1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是玉皇大帝0", "你好我是玉皇大帝1"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是自己0", "你好我是自己1"},
                                TalkScore = new[] {-1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是玉皇大帝0", "你好我是玉皇大帝1"},
                                isFriend = true
                            }
                        }
                    },
                    new TalkData
                    {
                        messageNews = new List<MessageNew>
                        {
                            new MessageNew
                            {
                                sender = new User
                                {
                                    name = "玉皇大帝",
                                    head = "touxiang-1"
                                },

                                TalkContent = new[] {"顶你好我是玉皇大帝顶第一句（0）", "你好我是玉皇大帝第一句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第一句（0）", "你好我是自己第一句（1）"},
                                TalkScore = new[] {-1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第二句（0）", "你好我是玉皇大帝第二句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, -1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },

                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            }
                        }
                    }
                }
            },

            new LevelData
            {
                messageComments = new List<MessageComment>
                {
                    new MessageComment
                    {
                        content = "要不咋们把如来开了吧",
                        sender = new User
                        {
                            name = "女版小岳岳",
                            head = "touxiang-3"
                        },
                        praise = 2100
                    },
                    new MessageComment
                    {
                        content = "玉帝哥哥我错了！",
                        sender = new User
                        {
                            name = "斗战胜佛",
                            head = "touxiang-4"
                        },
                        praise = 3400
                    }
                },
                selectContent = new[] {"玉帝你这也太窝囊了吧", "玉帝你还有妹妹没？", "要不咱把公司注销了吧"},
                startNumber = new[] {1, 2, 3},
                messageWorksIsLeftStart = false,
                messageWorksList = new List<MessageWorks>
                {
                    new MessageWorks
                    {
                        content = "我历经17500劫",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        },
                        isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "你被打猴打过",
                        sender = new User
                        {
                            name = "如来佛祖",
                            head = "touxiang-2"
                        },
                        isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "咋们能不提猴吗？",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        }
                    },
                    new MessageWorks
                    {
                        content = "你妹妹被凡人睡过",
                        sender = new User
                        {
                            name = "如来佛祖",
                            head = "touxiang-2"
                        },
                        isImportant = true
                    },
                    new MessageWorks
                    {
                        content = "...",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        },
                        isImportant = true
                    },

                    new MessageWorks
                    {
                        content = "弟弟也被凡人睡了",
                        sender = new User
                        {
                            name = "如来佛祖",
                            head = "touxiang-2"
                        }
                    },
                    new MessageWorks
                    {
                        content = "咋们还是提猴吧",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        }
                    }
                },
                messageLetters = new List<MessageLetter>
                {
                    new MessageLetter
                    {
                        content = "我是消息好友1",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        }
                    },
                    new MessageLetter
                    {
                        content = "我是好友2",
                        sender = new User
                        {
                            name = "玉皇大帝",
                            head = "touxiang-1"
                        }
                    }
                },
                talkDatas = new List<TalkData>
                {
                    new TalkData
                    {
                        messageNews = new List<MessageNew>
                        {
                            new MessageNew
                            {
                                sender = new User
                                {
                                    name = "玉皇大帝",
                                    head = "touxiang-1"
                                },

                                TalkContent = new[] {"你好我是玉皇大帝第一句（0）", "你好我是玉皇大帝第一句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是玉皇大帝第二句（0）", "你好我是玉皇大帝第二句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是自己第一句（0）", "你好我是自己第一句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是自己0", "你好我是自己1"},
                                TalkScore = new[] {-1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是玉皇大帝0", "你好我是玉皇大帝1"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是自己0", "你好我是自己1"},
                                TalkScore = new[] {-1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"你好我是玉皇大帝0", "你好我是玉皇大帝1"},
                                isFriend = true
                            }
                        }
                    },
                    new TalkData
                    {
                        messageNews = new List<MessageNew>
                        {
                            new MessageNew
                            {
                                sender = new User
                                {
                                    name = "玉皇大帝",
                                    head = "touxiang-1"
                                },

                                TalkContent = new[] {"顶你好我是玉皇大帝顶第一句（0）", "你好我是玉皇大帝第一句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第一句（0）", "你好我是自己第一句（1）"},
                                TalkScore = new[] {-1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第二句（0）", "你好我是玉皇大帝第二句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, -1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },

                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是自己第二句（0）", "你好我是自己第二句（1）"},
                                TalkScore = new[] {1, 1}
                            },
                            new MessageNew
                            {
                                TalkContent = new[] {"顶你好我是玉皇大帝第三句（0）", "你好我是玉皇大帝第三句（1）"},
                                isFriend = true
                            }
                        }
                    }
                }
            }
        };
    }

    /*
    public List<MessageTalk> GetCurrentSelectMessageTalk(int id)
    {
        var levelData = GetCurrentSceneData();
        return levelData.messageTalks[id];
    }
    */


    public LevelData GetCurrentSceneData()
    {
        return _datas[SceneryManager.GetLevelSceneID() - 1];
    }

    public int UpdateCurrentSceneData(string selectComment, int index)
    {
        var currentSceneData = GetCurrentSceneData();
        var startNum = currentSceneData.startNumber[index];
        currentSceneData.messageComments.Add(new MessageComment
        {
            content = selectComment,
            sender = SelfData,
            praise = startNum * 2000
        });
        return startNum;
    }
}


/// <summary>
///     一个关卡数据，目前总关卡有36个，建议总关卡以9的倍数。
/// </summary>
public class LevelData
{
    
    /// <summary>
    ///     固定评论的总数据
    /// </summary>
    public List<MessageComment> messageComments;

    /// <summary>
    ///     消息页面的总数据
    /// </summary>
    public List<MessageLetter> messageLetters;

    public bool messageWorksIsLeftStart = true;

    /// <summary>
    ///     段子的总数据
    /// </summary>
    public List<MessageWorks> messageWorksList;

    /// <summary>
    ///     选择的评论内容
    /// </summary>
    public string[] selectContent;

    /// <summary>
    ///     选择的评论所得的星星数目
    /// </summary>
    public int[] startNumber;


    /// <summary>
    ///     朋友页面的总数据
    /// </summary>
    public List<TalkData> talkDatas;
}

/// <summary>
///     一个谈话内容
/// </summary>
public class TalkData
{
    public List<MessageNew> messageNews;
}