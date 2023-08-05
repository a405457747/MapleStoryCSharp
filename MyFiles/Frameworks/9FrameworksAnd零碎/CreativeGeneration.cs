using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreativeGeneration : MonoBehaviour
{
    private string[] arr1 = { "2D","2.5D" ,"3D","1D"};
    private string[] arr2 = { "RPG", "塔防", "动作", "消除", "文字游戏", "模拟游戏", "射击", "音乐游戏", "跑酷游戏", "球", "车", "战旗", "卡牌", "生存游戏", "解迷题", "物理游戏", "IO类", "格斗", "潜入", "体育" };
    private string[] arr3 = { "回合制", "非回合", "放置", "ATP相关" };
    private string[] arr4 = { "写实", "卡通", "黑白" };
    private string[] arr5 = { "都市", "武侠", "军事", "中世纪", "农村", "幼儿", "青年", "老年", "奇幻", "童话", "僵尸", "细菌", "文化","选秀","快递","黑魂"};
    private string[] arr6 = { "2D俯视", "横板左右", "横板上下", "2DDNF", "纯3D" };
    private string[] arr7 = { "大海", "陆地", "城堡", "天空", "沙漠", "城镇", "地下城", "宇宙", "废土风格" };
    private string[] arr8 = { "植物类", "动物类", "人" };
    private string[] arr9 = { "现代", "古代", "史前" };





    public static string GetStr(string[] arr)
    {
        return arr[Random.Range(0, arr.Length)];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log($"{GetStr(arr1)} {GetStr(arr2)} {GetStr(arr3)} {GetStr(arr4)} {GetStr(arr5)} {GetStr(arr6)} {GetStr(arr7)} {GetStr(arr8)} {GetStr((arr9))}");
        }
    }
}
