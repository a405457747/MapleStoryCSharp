using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using QFramework;
using UnityEngine;
using UnityEngine.Networking;

namespace CallPalCatGames.QFrameworkExtension
{
    public class HotFixManager : QMgrBehaviour, ISingleton
    {
        private static readonly Dictionary<string, GameObject>
            PrefabDict = new Dictionary<string, GameObject>(); //字典保存从AB文件加载的游戏对象
        /*private string _luaFileDisposeName = "fixDispose";
        private readonly string _luaFileName = "fix";*/
        public override int ManagerId => QMgrID.HotFix;
        public static HotFixManager Instance => MonoSingletonProperty<HotFixManager>.Instance;

        public void OnSingletonInit()
        {
        }

        private string GetLuaFilePath(string fileName)
        {
            return $"{fileName}.lua.txt";
        }

        private string GetServerLuaFilePath(string fileName)
        {
            return $"{GetServerPreFix()}{GetLuaFilePath(fileName)}";
        }

        private string GetServerPreFix()
        {
            return "http://122.51.186.74/Book/3DPuz/";
        }

        private string GetServerABPath(string abName)
        {
            return $"{GetServerPreFix()}{abName}.ab";
        }

        private string GetLocalLuaFilePath(string fileName)
        {
            return $"{Application.persistentDataPath}/{GetLuaFilePath(fileName)}";
        }

        private void LoadServerLuaFile(string fileName)
        {
            var b = StartCoroutine(GetRequest(fileName));
        }

        private IEnumerator GetRequest(string fileName)
        {
            using (var webRequest = UnityWebRequest.Get(GetServerLuaFilePath(fileName)))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError)
                {
                }
                else
                {
                    var str = webRequest.downloadHandler.text;
                    File.WriteAllText(GetLocalLuaFilePath(fileName), str, Encoding.ASCII);
                }
            }
        }

        private byte[] MyLoader(ref string filepath)
        {
            var str = File.ReadAllText(GetLocalLuaFilePath(filepath));
            return Encoding.UTF8.GetBytes(str);
        }

        /*private void OnDisable()
        {
            _luaEnv.DoString($"require'{_luaFileDisposeName}'"); //加载fishDispose2，里面有释放的lua代码
        }

        private void OnDestroy()
        {
            _luaEnv.Dispose(); //释放环境
        }*/

        private IEnumerator LoadResourceCorotine(string resName)
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(GetServerABPath(resName));
            yield return request.SendWebRequest();
            var ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            var go = ab.LoadAsset<GameObject>(resName);
            PrefabDict.Add(resName, go);
        }

        //既然这个方法是要被Lua调用的，所以要加上这个标签，性能更好，否则将会使用反射来搞性能更差，从远程加载ab转换对象放入缓存,这里是提前用C#写好方法，然后用Lua去调用
        //[LuaCallCSharp] 
        private void LoadResource(string resName)
        {
            StartCoroutine(LoadResourceCorotine(resName));
        }

        //[LuaCallCSharp]
        private static GameObject GetGameObject(string goName) //获取缓存的ab变成的对象
        {
            return PrefabDict[goName];
        }
    }
}