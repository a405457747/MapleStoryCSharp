
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapleStory;
using UnityEngine;

public class ListNode
{
    public ListNode next;
    public int val;

    public ListNode(int x)
    {
        val = x;
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

public class Solution
{
    private ListNode listnode = new ListNode(1)
    {
        next = new ListNode(1) { next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(3) } } }
    };

    private ListNode listnode2 = new ListNode(1)
    {
        next = new ListNode(2)
        {
            next = new ListNode(6)
            {
                next = new ListNode(3)
                {
                    next = new ListNode(4) { next = new ListNode(5) { next = new ListNode(6) } }
                }
            }
        }
    };

    private ListNode listnode3 = new ListNode(1)
    {
        next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(4) { next = new ListNode(5) } } }
    };

    private ListNode listnode4 = new ListNode(2)
    {
        next = new ListNode(4)
        {
            next = new ListNode(3)
        }
    };

    private ListNode listnode5 = new ListNode(5)
    {
        next = new ListNode(6)
        {
            next = new ListNode(4)
        }
    };


    public TreeNode root1 = new TreeNode(4)
    {
        left = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3) },
        right = new TreeNode(7) { left = new TreeNode(6), right = new TreeNode(9) }
    };

    private TreeNode root10 = new TreeNode(5)
    {
        left = new TreeNode(3) { left = new TreeNode(2) { left = new TreeNode(1) }, right = new TreeNode(4) },
        right = new TreeNode(6) { right = new TreeNode(8) { left = new TreeNode(7), right = new TreeNode(9) } }
    };

    private TreeNode root11 = new TreeNode(5) { left = new TreeNode(2), right = new TreeNode(13) };

    private TreeNode root12 = new TreeNode(1)
    {
        left = new TreeNode(2) { left = new TreeNode(3), right = new TreeNode(4) },
        right = new TreeNode(2) { left = new TreeNode(4), right = new TreeNode(3) }
    };

    private TreeNode root13 = new TreeNode(5)
    {
        left = new TreeNode(8),
        right = new TreeNode(5)
    };

    private TreeNode root14 = new TreeNode(3)
    {
        left = new TreeNode(9) { left = new TreeNode(15), right = new TreeNode(7) },
        right = new TreeNode(20) { left = new TreeNode(15), right = new TreeNode(7) }
    };

    private TreeNode root19 = new TreeNode(3)
    {
        left = new TreeNode(9),
        right = new TreeNode(10)
    };

    private TreeNode root3 = new TreeNode(4)
    {
        left = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3) },
        right = new TreeNode(7)
    };

    private TreeNode root4 = new TreeNode(4)
    { left = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3) } };

    private TreeNode root5 = new TreeNode(3)
    {
        left = new TreeNode(9),
        right = new TreeNode(20) { left = new TreeNode(15), right = new TreeNode(7) }
    };

    private TreeNode root6 = new TreeNode(4) { left = new TreeNode(1), right = new TreeNode(2) };

    private TreeNode root7 = new TreeNode(3)
    {
        left = new TreeNode(4) { left = new TreeNode(1), right = new TreeNode(2) },
        right = new TreeNode(5)
    };

    private TreeNode root8 = new TreeNode(1)
    { left = new TreeNode(2) { right = new TreeNode(5) }, right = new TreeNode(3) };

    private TreeNode root9 = new TreeNode(3)
    {
        left = new TreeNode(5)
        {
            left = new TreeNode(6),
            right = new TreeNode(2) { left = new TreeNode(7), right = new TreeNode(4) }
        },
        right = new TreeNode(1) { left = new TreeNode(9), right = new TreeNode(8) }
    };

    public void PreOrder(TreeNode node, Action<int> callback)
    {
        if (node == null) return;


        //Debug.Log(node.val);
        callback(node.val);
        // System.Threading.Thread.Sleep(2000);

        PreOrder(node.left, callback);
        PreOrder(node.right, callback);
    }

    //21
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        if (list1 == null)
        {
            return list2;
        }
        else if (list2 == null)
        {
            return list1;
        }
        else if (list1.val < list2.val)
        {
            list1.next = MergeTwoLists(list1.next, list2);
            return list1;
        }
        else
        {
            list2.next = MergeTwoLists(list1, list2.next);
            return list2;
        }
    }
}



public class LeetcodeTest : MonoBehaviour
{
    private void Start()
    {
        var s = new Solution();

        Time.timeScale = 0.1f;
        s.PreOrder(s.root1, FindObjectOfType<BinaryTreeView>().ShowOne);
        // s.MergeTwoLists(null,null);

        //LinqTest();

    }

    private void ModifierTest()
    {
        //C#默认修饰符
        /*
        (1) 类、结构的默认修饰符是internal。
        (2) 类中所有的成员默认修饰符是private。
        (3) 接口默认修饰符是internal。
        (4) 接口的成员默认修饰符是public。
        (5) 枚举类型成员默认修饰符是public。
        (6) 委托的默认修饰符是internal
        */
    }

    private void LinqTest2()
    {
        var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        var numQuery = from num in numbers group num by (num % 3);
        foreach (var numKeyh in numQuery)
        {

        }
    }

    private void LinqTest()
    {
        var a = new int[] { 1, 2, 3, 4, 5 };
        var res = a.Aggregate((x, y) => x + y);
        var res2 = a.First(item => item >= 3);
        var res3 = a.ElementAt(2);
        LogNote.Debug(res, res2, res3);

        var liists = a.Cast<string>();
        LogNote.Debug(liists);

        IEnumerable<int> a2 = Enumerable.Range(1, 5);
        LogNote.Debug(a2);
        a2 = a2.Take(2).ToList();

        var a3 = a.Select(item => item >= 3);
        var a4 = a3.Where(item => item);

        ArrayList a5 = new ArrayList();
        LogNote.Debug(a5);

        var b = new List<int>();

        //b.RemoveAt(item=>item==2);
        b.RemoveRange(0, 3);

        b.ForEach(item => { print(item); });

        int v = 33;
        ref int temp = ref v;

        (int age, string name) yuanzhu = (22, "billgate");
        print("yuanzhu age :" + yuanzhu.age);

    }


    private void CSharpeSpeedEdit()
    {
		//f1 api导航超级好用的
		
        //alt+enter打开rider的灯泡

        //用好linq，反正再差也比python强

        //tab
        /*        while (false)
                {
                    print("true");
                }*/

        void jj(int age, string name)
        {
            print(age);
        }

        //ctrl+l

        //ctrl+alt+space

        jj(33, "ssk");

        //ctrl+e+c

        //propg

        //ctrl+e+u
        print("nihao");
        print("wohao");

        //Ctrl+Alt+Shift+上下左右


        //alt+f12

        //alt+上下箭头，方法之间导航

        //ctrl+shift+/  ctrl+alt+/
        print("sk");

        //多行游标alt+shit+上下
        print("hello");
        print("hello");

        //ctrl+KK 

        //ctrl+shift+f9 

        //shift+shift  alt+/
		
	    //ctrl+f10跳到指定地方呢
    }
}