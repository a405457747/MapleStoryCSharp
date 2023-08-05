using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ScollToDrawWheel : MonoBehaviour
{
    public Sprite bonusSp;
    public Sprite freeSp;

    // 奖励图片
    private Image[] ArardImgArr;

    // 转盘速度
    private float AniMoveSpeed = 6f;

    // 进度
    private float[] progress = new[] {0f, 1f, 2f, 3f, 4f};


    // 转动动画位置
    private Vector3[] AniPosV3 = new[]
        {Vector3.up * 150 * 2, Vector3.up * 150, Vector3.zero, Vector3.down * 150, Vector3.down * 150 * 2};

    // 自动暂停标识
    private bool isAutoStop;

    // 抽奖结束 停止刷新界面UI
    private bool isStopUpdatePos = true;

    private List<string> resSymbol;
    private List<float> weights;

    private RandomHelper.Weight<string> _weight;
    private Text[] _texts;

    public bool IsHightLitht
    {
        get => _isHightLitht;
        set
        {
            SetHightLight(value);
            _isHightLitht = value;
        }
    }

    private void SetHightLight(bool isHigh)
    {
        Image[] img = GetComponentsInChildren<Image>();
        Text[] txt = GetComponentsInChildren<Text>();
        foreach (var i in img)
        {
            if (i.gameObject.name == "GMask")
            {
                continue;
            }

            i.color = isHigh ? Color.white : Color.gray;
        }

        foreach (var i in txt)
        {
            i.color = isHigh ? Color.white : Color.gray;
        }
    }

    private bool _isHightLitht;

 [HideInInspector]   public List<string> allStr = new List<string>() {"x1", "x2", "x3", "free", "bonus"};


    private List<string> getParStr(string wantSymbol)
    {
        List<string> list1 = new List<string>(allStr);
        List<string> list2 = list1.Where(item => item != wantSymbol).ToList();
        RandomHelper.Shuffle(list2);

        List<string> res = new List<string>();
        res.Add(wantSymbol);
        
        res.AddRange(list2.Take(3));

        Assert.IsTrue(res.Count==4);
        return res;
    }

    public void Init(string wantSymbol)
    {
        Assert.IsTrue(allStr.Contains(wantSymbol));
        resSymbol = getParStr(wantSymbol);

        _texts = GetComponentsInChildren<Text>();
        ArardImgArr = transform.Find("GMask").transform.GetChildArray()
            .Select(item => item.GetComponent<Image>()).ToArray();
        for (var index = 0; index < _texts.Length; index++)
        {
            var t = _texts[index];
            t.text = resSymbol[index];
            textSet(t);
        }

        IsHightLitht = true;
    }

    private void textSet(Text t)
    {
        Image p = t.transform.parent.GetComponent<Image>();
        string content = t.text;
        if (content==allStr[3]||content==allStr[4])
        {
            p.enabled = true;
            t.enabled = false;

            if (content == allStr[3])
            {
                p.sprite = freeSp;
            }else if (content == allStr[4])
            {
                p.sprite = bonusSp;
            }
        }
        else
        {
            t.enabled = true;
            p.enabled = false;
        }
        Log.LogParas("i am textSet t txt is ",t.text); 
    }

    void Update()
    {
          /*if (Input.GetKeyDown(KeyCode.A))
        {
            Log.LogParas("press a");
            StartCoroutine(Click(allStr[4]));
        }*/
          
        if (isStopUpdatePos) return;

        float t = Time.deltaTime * AniMoveSpeed;
        for (int i = 0; i < ArardImgArr.Length; i++)
        {
            progress[i] += t;
            ArardImgArr[i].transform.localPosition = MovePosition(i);
        }

      
    }

  public  IEnumerator Click(int idx)
    {
        string wantSymbol = allStr[idx];
        Init(wantSymbol);
        yield return null;
        yield return StartCoroutine(DrawFun(wantSymbol));
        string res = this.SymbolRes;
        
        Log.LogParas("res is the the "+res);
    }

    // 获取下一个移动到的位置
    Vector3 MovePosition(int i)
    {
        int index = Mathf.FloorToInt(progress[i]);
        if (index > AniPosV3.Length - 2)
        {
            //保留其小数部分，不能直接赋值为0
            progress[i] -= index;
            index = 0;
            // 索引为2的到底了，索引为0的就在正中心
            if (i == 2 && isAutoStop)
            {
                isStopUpdatePos = true;
            }

            return AniPosV3[index];
        }
        else
        {
            return Vector3.Lerp(AniPosV3[index], AniPosV3[index + 1], progress[i] - index);
        }
    }

    /// <summary>
    /// 点击抽奖
    /// </summary>
    /// <param name="wantSymbol"></param>
    public IEnumerator DrawFun(string wantSymbol)
    {
       // Game.I.audioSystem.PlaySound(AudioEnum.public_nearmiss);
        isAutoStop = false;
        isStopUpdatePos = false;
        yield return null;

        yield return StartCoroutine(SetMoveSpeed(1.25f,wantSymbol));
        yield return new WaitUntil(() => isStopUpdatePos);
       // Game.I.audioSystem.PlaySound(AudioEnum.public_wildchange);
        yield return new WaitForSeconds(0.25f);


        // DoTween 按钮下拉动画
        // Transform tran = DrowBtn.transform;
        //tran.DOLocalMoveY(-60, 0.2f).OnComplete(() =>
        //{
        //      tran.DOLocalMoveY(50, 0.2f);
        //
        //});
    }

    public string SymbolRes { get; set; }

    void CreateResult(string wantSymbol)
    {
        SymbolRes = wantSymbol;
//        Log.LogParas("res is ", SymbolRes);

        List<string> tmp = new List<string>(resSymbol);
        RandomHelper.Shuffle(tmp);
        int resIndex = tmp.IndexOf(SymbolRes);
        Tool.Swap<string>(resIndex, 0, tmp);

        for (var index = 0; index < _texts.Length; index++)
        {
            var t = _texts[index];
            t.text = tmp[index];

            textSet(t);
        }
    }

    // 抽奖动画速度控制
    IEnumerator SetMoveSpeed(float time,string wantSymbol)
    {
        AniMoveSpeed = 10;
        yield return new WaitForSeconds(time);
        AniMoveSpeed = 5;
        CreateResult(wantSymbol);
        yield return null;
        isAutoStop = true;
        yield return null;
    }
}