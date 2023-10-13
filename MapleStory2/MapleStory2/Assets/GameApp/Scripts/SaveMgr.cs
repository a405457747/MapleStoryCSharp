using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MapleStory2
{
    public class SaveMgr : MonoBehaviour
    {

        internal T LoadData<T>() where T:new()
        {
            var tName = nameof(T)+".txt";
            var saveFile = Path.Combine(Application.persistentDataPath, tName);

            if (File.Exists(saveFile) == false)
            {
                File.WriteAllText(saveFile,"",new UTF8Encoding(false));
            }

            var tempStr = StrData<T>();

            if (tempStr == "")
            {
                return new T();
            }
            else
            {
                return JsonUtility.FromJson<T>(tempStr);
            }
        }

        internal void SaveData<T>(T saveObj)
        {
            var tName =nameof(T)+".txt";
            var jsonStr = JsonUtility.ToJson(saveObj, false);
            
            File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, tName), jsonStr,
                new UTF8Encoding(false));
        }
        
         string StrData<T>()
        {
            var tName =nameof(T)+".txt";
            return File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, tName),
                new UTF8Encoding(false));
        }
    }
}