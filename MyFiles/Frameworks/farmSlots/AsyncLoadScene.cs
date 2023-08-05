using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// 加载场景 —— 脚本挂载前场景
/// </summary>
public class AsyncLoadScene : MonoBehaviour
{
    public Text version;

    public Text sliderText;

    public Slider slider;          //滑动条
    int currentProgress; //当前进度
    int targetProgress;  //目标进度


    private void Start()
    {
        //  currentProgress = 0;
        // targetProgress = 0;

        version.text = "version " + UnityEngine.Application.version;

        var isPad = Differences.Pad();
        SetMatchWidthOrHeight(true, isPad);

        StartCoroutine(LoadingScene()); //开启协成
    }


    public void UpdateSlider(float p)
    {
        slider.value = p; //给UI进度条赋值
        sliderText.text = StringHelper.GetRatio(slider.value, "f0");
    }


    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns>协成</returns>
    private IEnumerator LoadingScene()
    {
        UpdateSlider(0);

        var cd = new WaitForSeconds(0.005f);

        var per = Time.fixedDeltaTime / 40;
        var speed = new WaitForSeconds(per);
        Debug.Log("speed cd is " + per);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Start"); //异步加载1号场景
        asyncOperation.allowSceneActivation = false;                          //不允许场景立即激活//异步进度在 allowSceneActivation= false时，会卡在0.89999的一个值，这里乘以100转整形

        for (int i = 1; i <= 6; i++)
        {
            yield return cd;
            UpdateSlider(0.1f * i);
        }

        //print("async pro" + asyncOperation.progress);
        while (asyncOperation.progress < 0.9f)                                //当异步加载小于0.9f的时候
        {
            print("cishu");
            //targetProgress = (int)( * 100); //异步进度在 allowSceneActivation= false时，会卡在0.89999的一个值，这里乘以100转整形
            yield return speed;
        }

        for (int i = 7; i <= 9; i++)
        {
            yield return cd;
            UpdateSlider(0.1f * i);
        }

        asyncOperation.allowSceneActivation = true; //加载完毕，这里激活场景 —— 跳转场景成功
    }

    /*
        private IEnumerator<WaitForEndOfFrame> LoadProgress()
        {
            while (currentProgress < targetProgress) //当前进度 < 目标进度时
            {
                ++currentProgress;                            //当前进度不断累加 （Chinar温馨提示，如果场景很小，可以调整这里的值 例如：+=10 +=20，来调节加载速度）
  
                yield return new WaitForEndOfFrame();         //等一帧
            }
        }
    */
    public void SetMatchWidthOrHeight(bool isLandscape, bool pad) //横1竖0
    {

        print("SetMatchWidthOrHeight frist");

        const float longNumber = 2400;
        const float shortNumber = 1080;

        var canvasScaler = GameObject.Find("tempGame/Canvas").transform.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (isLandscape)
        {
            canvasScaler.referenceResolution = new Vector2(longNumber, shortNumber);
            if (pad)
            {
                canvasScaler.matchWidthOrHeight = 0;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 1;
            }
        }
        else
        {
            canvasScaler.referenceResolution = new Vector2(shortNumber, longNumber);

            if (pad)
            {
                canvasScaler.matchWidthOrHeight = 1;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 0;
            }
        }

        Debug.Log("SetMatchWidthOrHeight Start Action");
    }
}
