using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListNode
{
    public ListNode next;
    public int val;

    public ListNode(int x)
    {
        val = x;
        next = null;
    }
}

public class TreeNode
{
    public TreeNode left;
    public TreeNode right;
    public int val;

    public TreeNode(int x)
    {
        val = x;
    }
}

internal class Person
{
    public int CityID { set; get; }
    public string Name { set; get; }
    public int Age { set; get; }
}

internal class City
{
    public int ID { set; get; }
    public string Name { set; get; }
}

internal class PetOwner
{
    public string Name { get; set; }
    public List<string> Pets { get; set; }
}

public class Node
{
    public IList<Node> children;
    public int val;

    public Node()
    {
    }

    public Node(int _val)
    {
        val = _val;
    }

    public Node(int _val, IList<Node> _children)
    {
        val = _val;
        children = _children;
    }
}

internal class Package
{
    public string Company { get; set; }
    public double Weight { get; set; }
    public long TrackingNumber { get; set; }
}

internal class Pet
{
    public string Name { get; set; }
    public Person Owner { get; set; }
}

public class LinqPlusAndLeetcodeTest : MonoBehaviour
{
    private int age = 3;

    public string bill = "gate";

    public int ijiu = 2;

    private TreeNode treenode1 = new TreeNode(10)
    {
        left = new TreeNode(5)
        {
            left = new TreeNode(3) {left = new TreeNode(3), right = new TreeNode(-1)},
            right = new TreeNode(2) {right = new TreeNode(1)}
        },
        right = new TreeNode(-3) {right = new TreeNode(11)}
    };

    private TreeNode treenode2 = new TreeNode(1)
    {
        left = new TreeNode(2) {left = new TreeNode(4)}, right = new TreeNode(3)
    };

    private TreeNode treenode3 = new TreeNode(1)
    {
        left = new TreeNode(2) {left = new TreeNode(4)}, right = new TreeNode(3)
    };

    private TreeNode treenode9 = new TreeNode(6)
    {
        left = new TreeNode(2)
        {
            left = new TreeNode(0), right = new TreeNode(4) {left = new TreeNode(3), right = new TreeNode(5)}
        },
        right = new TreeNode(8) {left = new TreeNode(7), right = new TreeNode(9)}
    };

    //探寻linq的更多可能性
    //数据结构的更多API(字符串，int,double,math,数组,列表，字典，栈，队列，HashSet,SortedSet,LinkedList,SortedDictionary,SortedList);
    //7.3C#最烧的写法
    //只用var

    private void Start()
    {
        #region 合计操作符

        //var a = new int[] { 1, 2, 3, 4 };
        //String[] bArray = new string[] { "a", "bc", "cccc" };
        //print(a.LongCount());
        //print(a.Average());
        //var aggRes = bArray.Aggregate((x, y) => x + ":" + y);//累加器第一个x是结果，第二个是当前项
        //var ji = list.Aggregate(1, (total, next) =>//第一个种子就是结果的值
        //                      next * total);
        //var ji2 = list.Aggregate(1, (a, b) => a * b, (item) => item.ToString());最后item就是最后结果
        //print(aggRes.Length);

        #endregion

        #region 元素操作符

        //var a2 = new int[] { };
        //var a = new int[] { 1, 2, 2, 3, 4, 5, 6 };
        //var b = a.First(item => item > 2);
        //var b2 = a.FirstOrDefault(item => item > 7);//默认值是0
        //print(b);
        //print(b2);
        //var b3 = a.Last(item => item < 4);
        //print(b3);
        //var b4 = a.Single(item => item < 2);//仅有一个就返回，其他就抛出异常
        //print(b4);
        //var b5 = a2.SingleOrDefault();//如果序列为空返回默认值
        //print(b5);
        //var b6 = a.ElementAt(2);
        //print(b6);
        //var b7 = a.ElementAtOrDefault(12);
        //print(b7);
        //Dictionary<int, string> dic = new Dictionary<int, string>()
        //{
        //    {1,"a" },
        //    {2,"b" },
        //};
        //print(dic.ElementAt(0));//返回字典第一个元素意义就在这里！

        #endregion

        #region 转换操作符

        //var a = new int[] { 1, 2, 3, 4 };
        //ArrayList arrayList = new ArrayList();
        //arrayList.Add("111");
        //arrayList.Add("222333");
        //arrayList.Add("333333333");
        //IEnumerable<string> lists = arrayList.Cast<string>();//linq cast 将以前版本的集合转换为IEnumerable<T>
        //foreach (var item in lists)
        //{
        //    //print(item);
        //}
        //List<Package> packages =
        // new List<Package>
        //     { new Package { Company = "Coho Vineyard",
        //          Weight = 25.2, TrackingNumber = 89453312L },
        //      new Package { Company = "Coho Vineyard",
        //          Weight = 18.7, TrackingNumber = 89112755L },
        //      new Package { Company = "Wingtip Toys",
        //          Weight = 6.0, TrackingNumber = 299456122L },
        //      new Package { Company = "Contoso Pharmaceuticals",
        //          Weight = 9.3, TrackingNumber = 670053128L },
        //      new Package { Company = "Wide World Importers",
        //          Weight = 33.8, TrackingNumber = 4665518773L } };
        //var a2 = packages.ToLookup(p => p.Company, p => p.TrackingNumber);//与字典类似，不过一个键可以匹配多个value;
        //foreach (var item in a2)
        //{
        //    print(item.Key);
        //    print("----");
        //    foreach (var item2 in item)
        //    {
        //        print(item2);
        //    }
        //}

        //var a3 = new List<int>() { 1, 2, 3 };
        //var a4 = a3.DefaultIfEmpty(5);//给空集合设置默认值啊这个
        //print(a4.ElementAt(0));
        //var a5 = a3.AsEnumerable().Where(item => item >= 2); //返回类型化为 IEnumerable<T> 的输入。
        //foreach (var item in a5)
        //{
        //    print(item);
        //}

        #endregion

        #region 集合操作符

        //var a = new int[] { 1, 2, 3, 4, 5 };
        //var a2 = a.Append(6);//数组也可以Append;
        ////print(a2.Last());
        //var b = new int[] { 6, 7, 8, 1, 1 };
        //var a3 = a.Union(b);
        ////print(a3.Count());
        //foreach (var item in a3)
        //{
        //    print(item);
        //}

        //var a4 = a.Zip<int, int, int>(b, (item1, item2) => item1 + item2);//把两个集合通过操作改成一个集合非常棒
        //foreach (var item in a4)
        //{
        //    //print(item);
        //}

        //var a5 = a.Concat(b);//和Union一样只是不去重啊太爽了
        //foreach (var item in a5)
        //{
        //    //print(item);
        //}

        #endregion

        #region 生成操作符

        //IEnumerable<int> a = Enumerable.Range(1, 5);
        //foreach (var item in a)
        //{
        //    print(item);
        //}
        //var b = Enumerable.Repeat(1, 5);//这个非常有用啊
        //foreach (var item in b)
        //{
        //    print(item);
        //}
        //var c = Enumerable.Empty<int>();
        //print(c.ElementAtOrDefault(0));

        #endregion

        #region 分区操作符

        //var b = Enumerable.Range(1, 5);
        //var b2 = b.Take(3);//一个集合取一部分非常有用啊
        //foreach (var item in b2)
        //{
        //    //print(item);
        //}
        //var b3 = b.Skip(3);//从左向右跳过3个，集合去一部有用吧

        //foreach (var item in b3)
        //{
        //    //print(item);
        //}

        #endregion

        #region 分组操作符

        //var a = Enumerable.Range(1, 5);
        //var a2 = a.GroupBy(item => item > 2);
        //foreach (var item in a2)
        //{
        //    print(item.Key);
        //    print("---");
        //    foreach (var item2 in item)
        //    {
        //        print(item2);
        //    }
        //}

        #endregion

        #region 连接操作符(有2个没有搞懂）

        //  Person[] persons = new Person[]
        //      {
        //  new Person{ CityID = 1, Name = "ABC" },
        //  new Person{ CityID = 1, Name = "EFG" },
        //  new Person{ CityID = 2, Name = "HIJ" },
        //  new Person{ CityID = 3, Name = "KLM" },
        //  new Person{ CityID = 3, Name = "NOP" },
        //  new Person{ CityID = 4, Name = "QRS" },
        //  new Person{ CityID = 5, Name = "TUV" }
        //      };
        //  City[] cities = new City[]
        //  {
        //  new City{ ID = 1,Name = "Guangzhou" },
        //  new City{ ID = 2,Name = "Shenzhen" },
        //  new City{ ID = 3,Name = "Beijing" },
        //  new City{ ID = 4,Name = "Shanghai" }
        //  };

        //  var result = persons.Join(cities, p => p.CityID, c => c.ID, (p, c) => new { PersonName = p.Name, CityNam = c.Name });//基于匹配键对两个序列的元素进行关联。
        //  foreach (var item in result)
        //  {
        //      print(item);
        //  }

        //  Person magnus = new Person { Name = "Hedlund, Magnus" };
        //  Person terry = new Person { Name = "Adams, Terry" };
        //  Person charlotte = new Person { Name = "Weiss, Charlotte" };

        //  Pet barley = new Pet { Name = "Barley", Owner = terry };
        //  Pet boots = new Pet { Name = "Boots", Owner = terry };
        //  Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
        //  Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

        //  List<Person> people = new List<Person> { magnus, terry, charlotte };
        //  List<Pet> pets = new List<Pet> { barley, boots, whiskers, daisy };

        //  var query =
        //people.GroupJoin(pets,
        //                 person => person,
        //                 pet => pet.Owner,
        //                 (person, petCollection) =>
        //                     new
        //                     {
        //                         OwnerName = person.Name,
        //                         Pets = petCollection.Select(pet => pet.Name)
        //                     });

        //  foreach (var obj in query)
        //  {
        //      // Output the owner's name.
        //      print(obj.OwnerName);
        //      print("----");
        //      // Output each of the owner's pet's names.
        //      foreach (string pet in obj.Pets)
        //      {
        //          print(pet);
        //      }
        //  }//基于键值等同性将两个序列的元素进行关联，并对结果进行分组。

        #endregion

        #region 其他操作符（自定义名字一个没搞懂）

        //    //var a = new List<int>() { 6, 1, 2, 3, 4, 5 };
        //    //a.Reverse();
        //    //a = a.OrderByDescending(item => item).ToList();
        //    //print(a[0]);
        //    var a = Enumerable.Range(1, 5);
        //    //var a2 = a.Select(item => item.ToString());
        //    //foreach (var item in a2)
        //    //{
        //    //    print(item);
        //    //}

        //    PetOwner[] petOwners =
        //{ new PetOwner { Name="Higa",
        //          Pets = new List<string>{ "Scruffy", "Sam" } },
        //      new PetOwner { Name="Ashkenazi",
        //          Pets = new List<string>{ "Walker", "Sugar" } },
        //      new PetOwner { Name="Price",
        //          Pets = new List<string>{ "Scratches", "Diesel" } },
        //      new PetOwner { Name="Hines",
        //          Pets = new List<string>{ "Dusty" } } };
        //    var query =
        //        petOwners
        //        .SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName }); //将序列的每个元素投影到 IEnumerable<T> 并将结果序列合并为一个序列。
        //    //对数组执行一对多投影，并使用结果选择器函数将源序列中的每个相应元素保留在对 Select的最终调用范围内。
        //    foreach (var item in query)
        //    {
        //        print(item.petName);
        //    }

        //var s = "ab ack wo";
        //string[] text = { "Today is 2018-06-06", "weather is sunny", "I am happy" };
        //var so = text.Select(item => item.Split(' '));//搞成多个数组,SelectMany搞成一个数组

        //foreach (var item in so)
        //{
        //    foreach (var item2 in item)
        //    {
        //        print(item2);
        //    }
        //}

        //var b = Enumerable.Range(1, 5);
        //ArrayList a = new ArrayList();
        //a.Add(1);
        //a.Add(2);
        //a.Add("3");
        //a.Add(4);
        //var c = a.OfType<int>();//筛选啊
        //foreach (var item in c)
        //{
        //    print(item);
        //}

        #endregion

        #region 字符串API

        //public int EnsureCapacity(int capacity);//确保 StringBuilder 的此实例的容量至少是指定值

        //string a = "abc";
        //a.Trim();//只有头和尾
        //print(a.ToLowerInvariant());//根据当前服务器的文化规则转换字符串
        //print( new string(a.Prepend('j').ToArray()));//先开头添加，卧槽这个方法太好用了。
        //print(a);
        //a.PadLeft(2, 'k');//字符填充啊
        //print(a.Normalize()); //返回一个新字符串，其二进制表示形式符合特定的 Unicode 范式。
        //var b = a.LastIndexOfAny(new char[] { 'b','a' }); //返回数组中任何一个字符最早出现的下标位置，索引仍然是从‘0’开始
        //print(a.IsNormalized());
        //print(a.GetTypeCode());
        //print(a.GetHashCode());
        //print(a.GetEnumerator());

        //char[] sk = new char[6];
        //a.CopyTo(0, sk, 0, 2);//把字符串给分区了，第三个是sk的索引了
        //foreach (var item in sk)
        //{
        //    print(item);
        //}
        //print(a.CompareTo("abc"));
        //print(a.Clone());//返回值不是此实例的独立副本;它只是相同数据的另一个视图。 使用 Copy 或 CopyTo 方法来创建一个与此实例具有相同值的单独 String 对象。
        //var b = a.AsQueryable(); //提供对数据类型已知的特定数据源的查询进行计算的功能。
        //var b = a.AsParallel();//启用查询的并行化。
        //print(b.AsOrdered());
        //var c = String.ReferenceEquals(a, "abc");
        //var c = String.Join(":", a);//对数组的处理
        //var c = String.IsNullOrWhiteSpace("");
        //var e = String.Intern("nn");//如果暂存了 str，则返回系统对其的引用；否则返回对值为 str 的字符串的新引用。
        //var d = String.IsInterned(e);//看看是否在池子里
        //var f = String.Format();
        //var b = String.Copy("nn");//创建一个新实例
        //var b = String.Compare("ab", "AB", false);
        //var b = String.CompareOrdinal("b", "a");//和Compare一样不过更快一点
        //print(b);

        #endregion

        #region int API

        //int c = 3;
        //print(c.CompareTo(2));
        //int.Equals(3, 3);
        //double b = double.Epsilon;// 表示大于零的最小正 Double 值。 此字段为常数。
        //var b2 = double.IsInfinity(double.MaxValue);//如果 d 的计算结果为 PositiveInfinity 或 NegativeInfinity，则为 true；否则为 false。
        //var b3 = double.NaN;//表示不是数字 (NaN) 的值。 此字段为常数。
        //var b4 = double.NegativeInfinity;//表示负无穷
        //var b5 = double.ReferenceEquals(2d, 2d);
        //print(b5);
        //long c2 = 9l;

        #endregion

        #region Math API

        //var a = 32;
        //var b = Math.BigMul(222, 2);//生产两个32位乘积
        //print(b);
        ////Math.Cosh  //返回指定角度的双曲余弦值。
        //var c = Math.DivRem(3, 2, out int bill);//返回商,out是余数呢
        //print(c);
        //print(bill);
        //var b2 = Math.Exp(9);//自然对数e的几次幂
        //var b3 = Math.IEEERemainder(2, 2);//另外一种求余数
        //var b4 = Math.Log(2);//返回指定数字的自然对数（底为 e）。
        //var b5 = Math.Log10(100);//返回指定数字以 10 为底的对数。
        //var b6 = Math.Round(2d);///四舍五入
        //var b7 = Math.Sign(2);//表示-1或1
        //var b8 = Math.Truncate(2.332f);//截断小数啊
        //print(b8);

        #endregion

        #region 数组API

        //var a = new int[] { 1, 1, 2, 2, 3 };
        //var b = a.Clone();//浅赋值
        //var c = new int[3];
        //print(b);
        //a.CopyTo(c, 0);
        //print(a.Length);
        //print(c.Length);
        //print(a.ElementAt(0));

        //var j=a.GetLongLength(0);
        //print(j);
        //var j = a.GetUpperBound(0);//获取上下边界非常的棒啊感觉呢
        //print(j);
        // var b2=a.GetValue(0);//可以传很多索引多维数组的时候
        // a.Initialize();//不需要用

        //a.IsFixedSize();//是否具有固定大小
        //ArrayList my = new ArrayList();
        //ArrayList c2 = ArrayList.FixedSize(my);

        //a.IsReadOnly();
        //a.IsSynchronized();//获取一个值，该值指示是否同步对 Array 的访问（线程安全）。
        //a.SetValue(3,0);
        //print(a[0]);
        //a.SyncRoot();//获取可用于同步对 Array 的访问的对象。
        //Array myArray = new int[] { 1, 2, 4 };
        //lock (myArray.SyncRoot)
        //{
        //    foreach (Object item in myArray)
        //        Console.WriteLine(item);
        //}

        //var b3 = Array.AsReadOnly(a);//返回指定数组的只读包装。
        //print(b3.Count());

        //var j = Array.BinarySearch(a, 2);
        //print(j);

        //Array.Clear(a, 0, 2);//这个方法还是很好用置空一部分
        //foreach (var item in a)
        //{
        //    print(item);
        //}

        //Array.ConstrainedCopy() //复制 Array 中的一系列元素（从指定的源索引开始），并将它们粘贴到另一 Array 中（从指定的目标索引开始）。 保证在复制未成功完成的情况下撤消所有更改。
        //var c3 = Array.ConvertAll(a, value => Convert.ToString(value));//非常好用哦
        //foreach (var item in c3)
        //{
        //    print(item);
        //}

        //var jiu = Array.CreateInstance(typeof(int), 5);//这个也能好用
        //foreach (var item in jiu)
        //{
        //    print(item);
        //}

        //var jiu2 = Array.Empty<string>();//返回空数组呢
        //var c33 = Array.Equals(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 });

        //var jj2 = Array.Exists(a, item => item == 2);//是否存在一个条件
        //                                             //print(jj2);
        //var jj3 = Array.Find(a, item => item == 2);
        //var jj4 = Array.FindAll(a, item => item == 2);//找到所有的啊
        //var jj5 = Array.FindIndex(a, item => item == 2);//神方法啊
        //var jj6 = Array.FindLast(a, item => item == 2);//最后一个匹配呢
        //Array.ForEach(a, item => print(item));//神方法
        //Array.Resize(ref a, 2);//神方法重新缩短大小啊
        //foreach (var item in a)
        //{
        //    print("sk:" + item);
        //}
        //Array.TrueForAll(a, item => item == 2);//每个都满足挺无聊的

        #endregion

        #region 列表

        //List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        //var list2 = list.GetRange(0, 2);//非常棒啊这个
        //foreach (var item in list2)
        //{
        //    //print(item);
        //}
        //var c = list.IndexOf(4);
        //list.InsertRange(2, list2);
        ////print(c);
        ////print("---");
        ////list.ForEach(item => print(item));
        //list.RemoveAll(item => item == 2);//很好用
        ////list.ForEach(item => print(item));
        //list.RemoveRange(0, 3);//这个也很好用啊

        //List<int> list3 = new List<int>() { 1, 1, 2, 2, 3, 4, 4, 5, 6 };
        //list3.TrimExcess();
        //list3.ForEach(item => print(item));
        //var gao = list.TrueForAll(item => item >= 1);
        ////print(gao);
        //var gao2 = list.ToDictionary(item => item);//这个是神一般的方法！！！！！！！
        //foreach (var item in gao2)
        //{
        //    print(item.Key + " ::" + item.Value);
        //}

        //var list6 = new List<int>() { 1, 2, 1, 3, 1, 6 };
        //var temp1 = list6.TakeWhile(item => item < 3);//这个方法会断啊
        //temp1 = list6.SkipWhile(item => item < 3);//这个方法也会断啊
        //temp1.ToList().ForEach(item => print(item));
        ////list.SkipWhile();
        ////list.TakeWhile();

        #endregion

        #region 字典(GetObjectData未懂可能有用)

        //Dictionary<int, string> dic = new Dictionary<int, string>()
        //{
        //    {1,"a" },
        //    {2,"b" },
        //    {3,"c" },
        //    {4,"c" },
        //};

        ////dic.Comparer();//获取用于确定字典中的键是否相等的 IEqualityComparer<T>。
        ////dic.Distinct();
        ////dic.GetObjectData;//实现 ISerializable 接口，并返回序列化 Dictionary<TKey,TValue> 实例所需的数据。
        ////dic.OnDeserialization();//实现 ISerializable 接口，并在完成反序列化之后引发反序列化事件。
        ////dic.Remove();//这个重要
        ////dic.Select();
        ////dic.Sum();
        ////dic.ToList();
        ////dic.Aggregate();

        #endregion

        #region 其他数据结构

        //Stack<int> sk = new Stack<int>() { };
        //sk.Push(1);
        //sk.Push(2);
        //var b= sk.Prepend(3);
        //foreach (var item in b)
        //{
        //    print(item);
        //}
        //Queue<int> q = new Queue<int>();

        //HashSet<int> h = new HashSet<int>() { 1, 2, 3, 3, 4, 5 };
        //HashSet<int> h2 = new HashSet<int>() { 1, 2, 3, 3 };

        //h.ExceptWith(new int[] { 1, 2 });//这个是删除呢
        //print(h.Count());

        //var bb = h.Overlaps(new int[] { 2, 3 });//确定是否至少有一个通用元素就是重叠呢有趣
        //print(bb);

        //h.RemoveWhere(item => item < 3);
        //print(h.Count());

        //var b2= h.SetEquals(new int[] { 1,  3, 4, 5 });//这个方法和队列相等没啥意思
        //print(b2);

        //h.SymmetricExceptWith(new int[] { 1, 2, 3, 3, 5, 4, 7, 9 });//这个方法不要交集神方法

        //foreach (var item in h)
        //{
        //    print(item);
        //}

        //var ggg2 = h.IsSupersetOf(h2);
        //print(ggg2);
        //h.IsProperSubsetOf();//真的不包括全集本身
        //h.IsProperSupersetOf();//真
        //h.IsSubsetOf();
        //h.IsSupersetOf();

        //SortedSet<int> sr = new SortedSet<int>();//本身也是hash;
        //sr.Add(3);
        //sr.Add(2);
        //sr.Add(2);
        //sr.Add(2);
        //sr.Add(1);
        //print(sr.Count());
        //var g = sr.GetViewBetween(1, 2);//获取值区间
        //foreach (var item in g)
        //{
        //    print(item);
        //}

        //SortedList<int, string> slist = new SortedList<int, string>();
        //slist.Add(2, "b");
        //slist.Add(1, "a");
        //slist.Add(3, "c");
        //foreach (KeyValuePair<int,string>item in slist)
        //{
        //    print(item.Key);
        //}

        //var g2 = slist[1];
        //print(g2);
        //SortedDictionary<int, string> sd = new SortedDictionary<int, string>();//差不多的

        //LinkedList<int> link = new LinkedList<int>();
        //link.AddAfter();//在 LinkedList<T> 中的现有节点后添加新的节点或值。
        //link.AddBefore();//在 LinkedList<T> 中的现有节点前添加新的节点或值。
        //link.AddFirst();//在 LinkedList<T> 的开头处添加新的节点或值。
        //link.AddLast();//在 LinkedList<T> 的结尾处添加新的节点或值。

        // slist.GetByIndex();
        // slist.GetKeyList();//获取 SortedList 对象中的键。
        //slist.IndexOfKey();//返回 SortedList 对象中指定键的从零开始的索引。
        //slist.IndexOfValue();//返回指定的值在 SortedList 对象中第一个匹配项的从零开始的索引。
        //slist.TrimToSize();//将容量设置为 SortedList 对象中元素的实际数目。禁止的时候用到

        #endregion

        #region 7.3语法写法

        //  和ref表达式 ref 和readonly修饰符和(ref readonly)这个没完成并且没搞懂
        (int, string) TebieBan(in int AInt)
        {
            Person p = null;
            print("?.：" + p?.Name);
            var dic = new Dictionary<int, string> {{2, "b"}};
            int.TryParse("22", out var bill);
            print("tryparse out in：" + bill);

            var woc = 0b0_0001;

            print("000_000:" + woc);
            double bb = default;
            print("defalut:" + bb);

            int Add(int a, int b)
            {
                return a + b;
            }

            print($"=>和子函数:{AInt}");

            int[] arr = {3, 2, 1};

            ref int GetByIndex(int[] arr2, int ix)=>return ref arr2[ix];

            ref var temp = ref GetByIndex(arr, 2);
            temp = 89;
            print("ref：" + arr[2]);

            (int age, string name) yuanzhu = (22, "billgate");
            print("yuanzhu age :" + yuanzhu.age);
            return yuanzhu;
        }

        #endregion

        //print("nihaoma");
    }

    public int FindNumbers(int[] nums)
    {
        var res = 0;
        res = nums.Count(item => item.ToString().Length % 2 == 0);
        return res;
    }
}