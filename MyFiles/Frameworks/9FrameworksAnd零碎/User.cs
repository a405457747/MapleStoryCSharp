using System.Collections.Generic;

public class User
{
    public List<User> attentions;
    public List<User> fans;

    /// <summary>
    ///     头像的图片的名字
    /// </summary>
    public string head;

    /// <summary>
    ///     名字
    /// </summary>
    public string name;

    public List<int> worksIDs;
}