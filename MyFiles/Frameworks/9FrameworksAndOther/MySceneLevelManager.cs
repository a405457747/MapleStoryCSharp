
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QFramework;
using System.Xml;

namespace xmaolol.com
{
    //就是关卡配置和其他配置
    public class MySceneLevelManager : MonoSingleton<MySceneLevelManager>
    {
        private readonly string xmlPath = @"EnemyWaveConfig";

        public Dictionary<string, Dictionary<string, string>> levelCSVConfigDic;

        public void LoadCSVConfiguration()
        {
            MyExcelData.LoadExcelFormCSV("LevelCSVCfg", out levelCSVConfigDic);
        }

        private void LoadByXML()
        {
            XmlDocument xmlDocument = new XmlDocument();
            TextAsset tw = Resources.Load(xmlPath) as TextAsset;
            xmlDocument.LoadXml(tw.text);
            //  print($"{tableTest.wave} {tableTest.enemyType} {tableTest.hp} {tableTest.wait} {tableTest.loseMoney} {tableTest.moveSpeed} {tableTest.dropRate}");
        }
    }
}
