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
    public static partial class ToolRoot
    {
        public static T FindComponent<T>(MonoBehaviour mono, string targetName) where T : Component
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

        public static T AddOrGetComponent<T>(GameObject go) where T : Component
        {
            var temp = go.GetComponent<T>();

            if (temp == null) temp = go.AddComponent<T>();

            return temp;
        }

        public static void Delay(MonoBehaviour mono, float second, UnityAction action)
        {
            IEnumerator delay()
            {
                yield return new WaitForSeconds(second);
                action?.Invoke();
            }

            mono.StartCoroutine(delay());
        }


#if UNITY_EDITOR
        public static string RunPythonScript(string filename, string arguments)
        {
            Process process = new Process();

            process.StartInfo.FileName = "python.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "\"" + filename + "\" " + arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            process.WaitForExit();

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