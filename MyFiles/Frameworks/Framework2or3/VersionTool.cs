/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VersionTool : EditorWindow
{
    private static readonly List<Ver> versions = new List<Ver>();
    private string addVersionExplain;

    private string addVersionNumber;
    private static string VersionPath => EditorGame.GetMyOtherVersionPath();
    private static string CreatePackagePath => EditorGame.GetMyOtherPath();

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        addVersionNumber = EditorGUILayout.TextField("增加的版本数", addVersionNumber);
        addVersionExplain = EditorGUILayout.TextArea(addVersionExplain, GUILayout.Height(position.height - 250));
        if (GUILayout.Button(new GUIContent("开始增加版本")))
        {
            int.TryParse(addVersionNumber, out var number);
            if (number != 0 && string.IsNullOrEmpty(addVersionExplain) == false)
            {
                StartSetVersion(addVersionExplain, number);

                addVersionNumber = "";
                addVersionExplain = "";
                Repaint();
            }
            else
            {
                Log.LogError("Error");
            }
        }

        if (GUILayout.Button(new GUIContent("开始打包"))) StartBuild();

        EditorGUILayout.EndVertical();
        //GUIUtility.ExitGUI();
    }

    [MenuItem("Framework/VersionTool/OpenWindow")]
    public static void OpenWindow()
    {
        var w = (VersionTool) GetWindow(typeof(VersionTool));
        w.position = new Rect(0, 0, 500, 500);
        w.Show();
    }

    private static void StartSetVersion(string e, int addNumber)
    {
        LoadByVersionXML();
        SaveByVersionXML(e, addNumber);

        PlayerSettings.Android.bundleVersionCode += 1;
        PlayerSettings.bundleVersion = StringHelper.GetFormatVersion(GetLastVersion());

        EditorGame.CopyToBoard(e);
        Log.LogPrint("Set Success");
    }

    private static string[] GetScenesStr()
    {
        return new[]
        {
            "Assets/Game/Scenes/Start.unity"
        };
    }

    private static string GetPackagePath(PackageType t, Channel c)
    {
        LoadByVersionXML();
        var temp = $"{c}.{PlayerSettings.applicationIdentifier}.{StringHelper.GetFormatVersion(GetLastVersion())}";
        temp = temp.Replace(".", "_");

        var res =
            $"{CreatePackagePath}/{temp}.{t}";
        return res;
    }

    private static void StartBuild()
    {
        var data = Factorys.GetAssetFactory().LoadScriptableObject<VersionToolData>();

        var applicationIdentifier = "";
        BuildTarget buildTarget = default;
        PackageType pkgType = default;

        if (data.Channel == Channel.Amazon)
        {
            applicationIdentifier = $"com.amazon.{PlayerSettings.companyName}.{PlayerSettings.productName}";
            pkgType = PackageType.apk;
            buildTarget = BuildTarget.Android;
        }
        else if (data.Channel == Channel.Google)
        {
            applicationIdentifier = $"com.{PlayerSettings.companyName}.{PlayerSettings.productName}";
            pkgType = PackageType.aab;
            buildTarget = BuildTarget.Android;
        }
        else
        {
            applicationIdentifier = $"com.{PlayerSettings.companyName}.{PlayerSettings.productName}";
            pkgType = PackageType.ipa;
            buildTarget = BuildTarget.iOS;
        }

        // EditorUtility.SetDirty(data);
        // EditorGlobal.Refresh();

        var config = Factorys.GetAssetFactory()
            .LoadScriptableObject<ProductConfigList>().list[0];
        PlayerSettings.companyName = config.CompanyName;
        PlayerSettings.productName = config.ProductName;
        PlayerSettings.applicationIdentifier = applicationIdentifier;
        /*PlayerSettings.Android.keyaliasName = "a";
        PlayerSettings.keyaliasPass = "yn1234";
        PlayerSettings.keystorePass = "yn1234";
        PlayerSettings.Android.keystoreName = EditorGame.GetMyOtherPath() + @"\user.keystore";*/

        if (config.LandScape)
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
        else
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;


        var pkgPath = GetPackagePath(pkgType, data.Channel);
        if (File.Exists(pkgPath)) File.Delete(pkgPath);

        BuildPipeline.BuildPlayer(GetScenesStr(), pkgPath, buildTarget,
            BuildOptions.None);

        Application.OpenURL(CreatePackagePath);
        Log.LogPrint("打包成功");
        //EditorUtility.DisplayDialog("版本工具", "打包成功", "ok", "exit");
    }



    private static void LoadByVersionXML()
    {
        var xmlDocument = new XmlDocument();

        var str = File.ReadAllText(VersionPath, Encoding.UTF8);

        xmlDocument.LoadXml(str);
        var contentXmls = xmlDocument.GetElementsByTagName("Ver");

        versions.Clear();

        foreach (XmlNode contentXml in contentXmls)
        {
            var temp = new Ver
            {
                Number = int.Parse(contentXml.Attributes["Number"].Value),
                Explain = contentXml.Attributes["Explain"].Value
            };
            versions.Add(temp);
        }
    }

    private static int GetLastVersion()
    {
        return versions[versions.Count - 1].Number;
    }

    private static void SaveByVersionXML(string explain, int addVersion)
    {
        versions.Add(new Ver {Explain = explain, Number = GetLastVersion() + addVersion});

        var xmlDoc = new XmlDocument();
        xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");

        XmlNode rootNode = xmlDoc.CreateElement("Root");
        foreach (var v in versions)
        {
            XmlNode ver = xmlDoc.CreateElement("Ver");

            var number = xmlDoc.CreateAttribute("Number");
            number.Value = v.Number.ToString();

            var Explain = xmlDoc.CreateAttribute("Explain");
            Explain.Value = v.Explain;

            ver.Attributes.Append(number);
            ver.Attributes.Append(Explain);

            rootNode.AppendChild(ver);
        }

        xmlDoc.AppendChild(rootNode);
        xmlDoc.Save(VersionPath);
    }

    [Serializable]
    public class Ver
    {
        public int Number;
        public string Explain;
    }
}