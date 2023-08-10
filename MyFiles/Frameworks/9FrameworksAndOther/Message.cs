public class Message
{
    /// <summary>
    ///     内容
    /// </summary>
    public string content;

    public User receiver;

    /// <summary>
    ///     谁发送的内容
    /// </summary>
    public User sender;

    public int worksID;
}

public class MessageWorks : Message
{
    /// <summary>
    ///     是需要聚焦的内容吗
    /// </summary>
    public bool isImportant = false;
}

public class MessageLetter : Message
{
}

public class MessageNew : Message
{
    /// <summary>
    ///     是我还是我的朋友，true是我自己
    /// </summary>
    public bool isFriend = false;

    /// <summary>
    ///     谈话的两天内容
    /// </summary>
    public string[] TalkContent;

    /// <summary>
    ///     当是我自己的适合，我的选择所得分
    /// </summary>
    public int[] TalkScore;
}

public class MessageComment : Message
{
    /// <summary>
    ///     点赞数
    /// </summary>
    public int praise;
}