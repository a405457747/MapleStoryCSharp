using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MapleStory
{
    public class LanguageTool : MonoBehaviour
    {

        [FormerlySerializedAs("LanguageId")] [FormerlySerializedAs("languageId")]
        public int LanguageSelectId;

        public virtual void Awake()
        {


            // AutoSetLanguageId();
        }

        private void AutoSetLanguageId()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    LanguageSelectId = 0;
                    break;

                case SystemLanguage.ChineseTraditional:
                    LanguageSelectId = 1;
                    break;

                default:
                    LanguageSelectId = 2;
                    break;
            }
        }

        public string GetMessage(int messageId)
        {
            //这里效率很低，但是谁叫我用C#呢
            var data = LanguageTable.Datas.Find(item => item.ID == $"{messageId}");
            if (LanguageSelectId == 0)
            {
                return data.ChineseSimplified;
            }
            else if (LanguageSelectId == 1)
            {
                return data.ChineseTraditional;
            }
            else if (LanguageSelectId == 2)
            {
                return data.English;
            }

            return "";
        }

        public string FillMessage(params object[] paras)
        {
            int id = (int)paras[0];
            string txtContent = GetMessage(id); //根据id获取txt的内容，让策划C位出道。

            if (paras.Length == 1)
            {
                return txtContent;
            }
            else if (paras.Length == 2) //只做一个星多了不想做了
            {
                return txtContent.Replace("{}", paras[1].ToString());
            }

            return "";
        }

    }
}