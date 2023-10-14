using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;


namespace MapleStory2
{
    public class QuickChangeScene : EditorWindow
    {
        private static string developeSceneName = "Main";

        [MenuItem("Tools/ChangeSceneLogo %t")]
        public static void ChangeSceneLogo()
        {
            //UnityEngine.Debug.Log("change1");
            ChangeScene("Logo");
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        [MenuItem("Tools/ChangeSceneDevelopment %g")]
        public static void ChangeSceneDevelopment()
        {
            //UnityEngine.Debug.Log("change2");
            EditorApplication.isPlaying = false;
            StartSceneSwitch();
        }

        private static void ChangeScene(string sceneName)
        {
            string scenePath = $"Assets/GameApp/Scenes/{sceneName}.unity"; // 替换为你想要切换的场景路径
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo(); //好像是一个弹窗啥的
            EditorSceneManager.OpenScene(scenePath);
        }

        //下面函数是AI写的
        private static bool isDelaying = false;
        private static float delayDuration = 0.1f; // 延时时间（单位：秒）
        private static float startTime;

        private static void StartSceneSwitch()
        {
            isDelaying = true;
            startTime = (float)EditorApplication.timeSinceStartup;
            EditorApplication.update += UpdateDelay;
        }

        private static void UpdateDelay()
        {
            float currentTime = (float)EditorApplication.timeSinceStartup;
            float elapsedTime = currentTime - startTime;

            if (isDelaying && elapsedTime >= delayDuration)
            {
                // 延时结束后打开新场景
                ChangeScene(developeSceneName);

                // 结束延时
                isDelaying = false; //allen认为这个结束有点多余，因为没有update自然也不用if判断了，加上update是用了就扔的。
                EditorApplication.update -= UpdateDelay;
            }
        }
    }
}