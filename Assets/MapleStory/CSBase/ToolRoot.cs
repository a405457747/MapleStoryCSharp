using System.Collections;

#if UNITY_EDITOR
using System.Diagnostics;
#endif

using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System.Text;
using Debug = UnityEngine.Debug;

namespace MapleStory
{
    public static partial class ToolRoot//集合类的工具
    {

        public static T FindComponent<T>( MonoBehaviour mono, string targetName) where T : Component
        {
        
            Transform FindTrans(Transform aParent, string aName)
            {
                foreach (Transform child in aParent)
                {
                    if (child.name == aName) return child;

                    if (child.name.Contains("GameObject") && (child.name.StartsWith("GameObject") == false))
                    {
                    }
                    else
                    {
                        var result = FindTrans(child, aName);
                        if (result != null) return result;
                    }
                }

                return null;
            }

            var t = FindTrans(mono.transform, targetName);
            return t != null ? t.GetComponent<T>() : default;
        }
        
        /*
        public static T ListAddGo<T>(string goName="", List<T> tList = null, Transform parent = null, bool stays = false,
            bool isUI = true)
            where T : MonoBehaviour
        {
            T item = default;
            if (isUI)
            {
                item = GameObject.Instantiate(Load<GameObject>(goName), parent, stays).GetComponent<T>();
            }
            else
            {
                var go = (new GameObject(typeof(T).Name+"(Clone)"));
                go.transform.SetParent(parent);
                item = Kit.GetOrAddComponent<T>(go);
            }

            if (tList != null) tList.Add(item);
            return item;
        }
        public static T ListDelGo<T>(int idx, List<T> tList) where T : MonoBehaviour
        {
            var item = tList[idx];
            Destroy(item.gameObject);
            tList.RemoveAt(idx);
            return item;
        }


        public static T DictAddGo<T>(string goName = "", List<T> tList = null, Transform parent = null,
            bool stays = false,
            bool isUI = true)
        {
            T item = default;
            return item;
        }
        
        public static T DictDelGo<T>(string goName = "", List<T> tList = null, Transform parent = null,
            bool stays = false,
            bool isUI = true)
        {
            T item = default;
            return item;
        }
        */
        
        public static T AddOrGetComponent<T>( GameObject go) where T : Component
        {
            var temp = go.GetComponent<T>();

            if (temp == null) temp = go.AddComponent<T>();

            return temp;
        }
        
        public static void Delay( MonoBehaviour mono, float second, UnityAction action)
        {
            IEnumerator delay()
            {
                yield return new WaitForSeconds(second);
                action?.Invoke();
            }

            mono.StartCoroutine(delay());
        }
        
        public static T JsonToClass<T>(string fileName) where T : class
        {
            TextAsset ta = Resources.Load<TextAsset>(fileName);
            T obj = default;
            //  obj= JsonMapper.ToObject<T>(ta.text);
            return obj;
        }

#if UNITY_EDITOR
        public static string RunPythonScript(string filename, string arguments) // 返回json字符串
        {
            Process process = new Process();

            process.StartInfo.FileName = "python.exe"; //解释器

            process.StartInfo.UseShellExecute = false;

            process.StartInfo.Arguments = "\"" + filename + "\" " + arguments;

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;

            process.StartInfo.CreateNoWindow = true;

            process.Start();
            /* 
            process.BeginOutputReadLine();
            process.OutputDataReceived += new DataReceivedEventHandler("ss");
            */

            process.WaitForExit();

            /*和上面注释的应该是等价的吧*/
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(output))
            {
                Debug.Log(output);
                return output;
            }

            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError(error);
                return error;
            }

            return "";
        }
#endif
        public static void WriteToFile(string filePath, string content)
        {
            
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }
    }
}