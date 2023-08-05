using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class CreateTool
{
    private static readonly string SystemInitClassStr =
        @"using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class #类名# : GameSystem
{
    public #类名#(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Release()
    {
        base.Release();
    }
//auto
   private void Awake()
	{
		#查找#
        #绑定#
	}
	#成员#
    #函数#    
}
";

    private static string format => EditorGame.format;
    private static readonly string CodeCreateDir = $"{Application.dataPath}/Game/Scripts";
    private static readonly string LuaCodeCreateDir = "";// Path.Combine(LuaSystem.LuaRoot(), "ui");

    private static readonly string UIInitClassStr = @"using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class #类名# : MonoBehaviour
{

//auto
   private void Awake()
	{
		#查找#
        #绑定#
	}
	#成员#
    #函数#    
}
";

    private static readonly string PanelInitClassStr =
        @"using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class #类名# : View
{
	public override void Initialize(string name)
	{
		base.Initialize(name);
	}

    public override void HandleEvent(string eventName, object data)
    {
    }

//auto
   private void Awake()
	{
		#查找#
        #绑定#
	}
	#成员#
    #函数#    
}
";

    private static readonly HashSet<string> PanelBlacklist = new HashSet<string>
    {
        "Button",
        "Text",
        "Toggle",
        "Background",
        "Checkmark",
        "Label",
        "Slider",
        "Fill Area",
        "Fill",
        "Handle Slide Area",
        "Handle",
        "Scrollbar",
        "Sliding Area",
        "Dropdown",
        "Arrow",
        "Template",
        "Viewport",
        "Content",
        "Item",
        "Item Background",
        "Item Checkmark",
        "Item Label",
        "Scroll View",
        "Scrollbar Horizontal",
        "Scrollbar Vertical",
        "InputField",
        "Placeholder",
        "GameObject",
        "Image"
    };


    private static readonly Dictionary<string, (string, PanelUIType)>
        PanelDic = new Dictionary<string, (string, PanelUIType)>(); //key name val path

    private static List<PanelUIType> PanelTypes;

    /*private static void CreateAssetsTxt()
    {
        CrateAssetsTxtFile(".txt");
    }*/

    public static void CrateAssetsTxtFile(string fileEx, string fileName = "new", string fileContain = "")
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var fileFullName = $"{fileName}{fileEx}";

        var realFolderPath = EditorGame.GetAssetsPathAbsolute(selectFolderPath);
        var writePath = $"{Path.Combine(realFolderPath, fileFullName)}";

        CreateTxt(writePath, fileContain);
        EditorGame.Refresh();

        var selectFilePath = Path.Combine(selectFolderPath, fileFullName);
        Selection.activeObject = AssetDatabase.LoadAssetAtPath(selectFilePath, typeof(Object));
    }

    private static void CreateTxt(string path, string fileContain = "")
    {
        if (File.Exists(path)) return;

        var utf8 = new UTF8Encoding(false);
        File.WriteAllText(path, fileContain, utf8);
    }

    [MenuItem("Framework/CreateTool/CreateDirMyOther")]
    private static void CreateDirMyOther()
    {
        var path = EditorGame.GetMyOtherPath();
        IOHelper.CreateDir(path);
    }

    [MenuItem("Framework/CreateTool/CreateVersionTxt")]
    private static void CreateVersionTxt()
    {
        var path = EditorGame.GetMyOtherVersionPath();
        var content = "<Root>\r\n" + " <Ver Number=\"1\" Explain=\"\" />\r\n" + "</Root>";
        CreateTxt(path, content);
    }

    [MenuItem("GameObject/CreateTool/SystemCreateCode", priority = 5)]
    public static void SystemCreateCode()
    {
        var selectobj = Selection.gameObjects;
        if (selectobj.Length != 1) return;

        var root = selectobj[0].transform;
        var rootName = root.name;

        if (rootName.Contains(Game.SystemSuffix) == false) return;

        var haveE = root.GetComponent<HaveEvents>();
        if (haveE != null)
            if (haveE.EventTypes.Distinct().Count() != haveE.EventTypes.Count())
                throw new Exception("have same event type");

        SystemCreateFile(rootName, haveE);
    }

    private static void SystemCreateFile(string className, HaveEvents haveE)
    {
        var memberStr = ""; //成员变量字符串
        var findStr = ""; //查询代码字符串

        var bindStr = "";
        var funcStr = "";

        if (haveE != null)
            foreach (var ev in haveE.EventTypes)
            {
                bindStr = $"{bindStr}Incident.DeleteEvent<{ev}>({ev}Callback);{format}";
                bindStr = $"{bindStr}Incident.RigisterEvent<{ev}>({ev}Callback);{format}";
            }

        var scriptPath = $"{CodeCreateDir}/System/{className}.cs";
        var classStr = "";
        if (File.Exists(scriptPath)) //如果已经存在了脚本，则只替换//auto下方的字符串
        {
            var classfile = new FileStream(scriptPath, FileMode.Open);
            var read = new StreamReader(classfile);
            var readStr = read.ReadToEnd();

            read.Close();
            classfile.Close();
            File.Delete(scriptPath);

            var splitStr = "//auto";
            var unchangeStr = Regex.Split(readStr, splitStr, RegexOptions.IgnoreCase)[0];
            var changeStr = Regex.Split(SystemInitClassStr, splitStr, RegexOptions.IgnoreCase)[1];

            classStr = $"{unchangeStr}{splitStr}{changeStr}";
        }
        else
        {
            classStr = SystemInitClassStr;
        }

        classStr = classStr.Replace("#类名#", className);
        classStr = classStr.Replace("#查找#", findStr);
        classStr = classStr.Replace("#成员#", memberStr);
        classStr = classStr.Replace("#绑定#", bindStr);
        classStr = classStr.Replace("#函数#", funcStr);

        var file = new FileStream(scriptPath, FileMode.CreateNew); //指示操作系统应创建新文件，如果文件已经存在，将引发异常
        var fileW = new StreamWriter(file, Encoding.UTF8);
        fileW.Write(classStr);
        fileW.Flush(); //强制写入，确保数据完整
        fileW.Close(); //不需要dispose，下次接着玩
        file.Close(); //fileStrem最后关闭

        EditorGame.Refresh();

        Log.LogPrint("Create System Success");
    }

    static string LuaClassStr = @"
-------------------------------------------------------                                                             
-- author : sky_allen                                                                                                                  
--  email : 894982165@qq.com      
--   time : $time$                                                                                           
-------------------------------------------------------

---@class $table$
local $table$ = class('$table$',Ui)

function $table$:init(go)
    $table$.super.init(self,go)
end

--auto
   
function $table$:ctor()
    $table$.super.ctor(self)
	#查找#
    #绑定#
end
	#成员#
    #函数#    

return $table$
";

    [MenuItem("GameObject/CreateTool/LuaPanelCreateCode %g", priority = -300)]
    public static void LuaPanelCreateCode()
    {
        var selectobj = Selection.gameObjects;
        if (selectobj.Length != 1) return;

        var root = selectobj[0].transform;
        var rootName = root.name;

        if ((rootName.Contains("Panel") == false)) return;

        /*var haveE = root.GetComponent<HaveEvents>();
        if (haveE != null)
            if (haveE.EventTypes.Distinct().Count() != haveE.EventTypes.Count())
                throw new Exception("have same event type");*/

        PanelTypes = StructHelper.GetEnums<PanelUIType>();
        PanelTypes.Remove(PanelUIType.Null);
        PanelDic.Clear();

        PanelTraverse(root, root);

        LuaUIAboutCreateFile(rootName, null, LuaClassStr);
    }

    private static void LuaUIAboutCreateFile(string className, HaveEvents haveE, string initClassStr)
    {
        var memberStr = ""; //成员变量字符串
        var findStr = ""; //查询代码字符串

        var bindStr = "";
        var funcStr = "";

        foreach (var kv in PanelDic)
        {
            var name = kv.Key;
            var childPath = kv.Value.Item1;
            var type = kv.Value.Item2;
            //memberStr =$"{memberStr}private {type} {name}=null;{format}";

            switch (type)
            {
                case PanelUIType.Text:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\"):GetComponent(\'{type}\');{format}";
                    funcStr = $"{funcStr}function {className}:{name}Refresh(t)self.{name}.text=t;end{format}";
                    break;
                case PanelUIType.Button:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\"):GetComponent(\'{type}\');{format}";

                    //bindStr = $"{bindStr}{name}.onClick.AddListener(()=>{{{name}Action?.Invoke();}});{format}";
                    bindStr = $"{bindStr}self.{name}.onClick:AddListener(function()self:{name}Action()end);{format}";
                    //memberStr = $"{memberStr}public Action {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.Toggle:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\"):GetComponent(\'{type}\');{format}";
                    bindStr =
                        $"{bindStr}self.{name}.onValueChanged:AddListener(function(t)self:{name}Action(t)end);{format}";
                    //memberStr = $"{memberStr}public Action<bool> {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.Slider:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\"):GetComponent(\'{type}\');{format}";
                    bindStr =
                        $"{bindStr}self.{name}.onValueChanged:AddListener(function(t)self:{name}Action(t)end);{format}";
                    //memberStr = $"{memberStr}public Action<float> {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.Dropdown:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\"):GetComponent(\'{type}\');{format}";
                    bindStr =
                        $"{bindStr}self.{name}.onValueChanged:AddListener(function(t)self:{name}Action(t)end);{format}";
                    /*memberStr = $"{memberStr}public Action<int> {name}Action{{get;set;}}{format}";*/
                    break;
                case PanelUIType.InputField:
                    break;
                case PanelUIType.Image:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\"):GetComponent(\'{type}\');{format}";
                    funcStr = $"{funcStr}function {className}:{name}Refresh(t)self.{name}.sprite=t;end{format}";
                    break;
                case PanelUIType.GameObject:
                    findStr =
                        $"{findStr}self.{name}=self.go.transform:Find(\"{childPath}\").gameObject;{format}";
                    funcStr =
                        $"{funcStr}function {className}:{name}SetParent(t)t.transform:SetParent(self.{name}.transform,false);end{format}";
                    break;
            }
        }

        if (haveE != null)
            foreach (var ev in haveE.EventTypes)
            {
                bindStr = $"{bindStr}Incident.DeleteEvent<{ev}>({ev}Callback);{format}";
                bindStr = $"{bindStr}Incident.RigisterEvent<{ev}>({ev}Callback);{format}";
            }

        var scriptPath = $"{LuaCodeCreateDir}/{className}.lua";
        var classStr = "";
        if (File.Exists(scriptPath)) //如果已经存在了脚本，则只替换//auto下方的字符串
        {
            var classfile = new FileStream(scriptPath, FileMode.Open);
            var read = new StreamReader(classfile);
            var readStr = read.ReadToEnd();

            read.Close();
            classfile.Close();
            File.Delete(scriptPath);

            var splitStr = "--auto";
            var unchangeStr = Regex.Split(readStr, splitStr, RegexOptions.IgnoreCase)[0];
            var changeStr = Regex.Split(initClassStr, splitStr, RegexOptions.IgnoreCase)[1];

            classStr = $"{unchangeStr}{splitStr}{changeStr}";
        }
        else
        {
            classStr = initClassStr;
        }

        classStr = classStr.Replace("$time$", DateTime.Now.ToString());
        classStr = classStr.Replace("$table$", className);
        classStr = classStr.Replace("#查找#", findStr);
        classStr = classStr.Replace("#成员#", memberStr);
        classStr = classStr.Replace("#绑定#", bindStr);
        classStr = classStr.Replace("#函数#", funcStr);

        var file = new FileStream(scriptPath, FileMode.CreateNew); //指示操作系统应创建新文件，如果文件已经存在，将引发异常
        var utf = new UTF8Encoding(false);
        var fileW = new StreamWriter(file, utf);
        fileW.Write(classStr);
        fileW.Flush(); //强制写入，确保数据完整
        fileW.Close(); //不需要dispose，下次接着玩
        file.Close(); //fileStrem最后关闭

        EditorGame.Refresh();

        Log.LogPrint("Lua Panel Or UI Create Success");
    }

    [MenuItem("GameObject/CreateTool/PanelCreateCode %h", priority = 5)]
    public static void PanelCreateCode()
    {
        var selectobj = Selection.gameObjects;
        if (selectobj.Length != 1) return;

        var root = selectobj[0].transform;
        var rootName = root.name;

        if ((rootName.Contains("Panel") == false)) return;

        var haveE = root.GetComponent<HaveEvents>();
        if (haveE != null)
            if (haveE.EventTypes.Distinct().Count() != haveE.EventTypes.Count())
                throw new Exception("have same event type");

        PanelTypes = StructHelper.GetEnums<PanelUIType>();
        PanelTypes.Remove(PanelUIType.Null);
        PanelDic.Clear();

        PanelTraverse(root, root);

        UIAboutCreateFile(rootName, haveE, PanelInitClassStr);
    }

    [MenuItem("GameObject/CreateTool/UICreateCode", priority = 5)]
    public static void UICreateCode()
    {
        var selectobj = Selection.gameObjects;
        if (selectobj.Length != 1) return;

        var root = selectobj[0].transform;
        var rootName = root.name;

        if ((rootName.Contains("UI") == false)) return;

        var haveE = root.GetComponent<HaveEvents>();
        /*if (haveE != null)
            if (haveE.EventTypes.Distinct().Count() != haveE.EventTypes.Count())
                throw new Exception("have same event type");*/

        PanelTypes = StructHelper.GetEnums<PanelUIType>();
        PanelTypes.Remove(PanelUIType.Null);
        PanelDic.Clear();

        PanelTraverse(root, root);

        UIAboutCreateFile(rootName, haveE, UIInitClassStr);
    }

    [MenuItem("Framework/CreateTool/ReadAudioNamesToEnum")]
    private static void ReadAudioNamesToEnum()
    {
        var audioPath = $"{CodeCreateDir}/Event/AudioEnum.cs";
        var head = @"public enum AudioEnum {";
        var tail = @"}";
        var middle = "";

        List<string> audioNames = Resources.LoadAll<AudioClip>("Audio").Select(item => item.name).ToList();

        foreach (var type in audioNames) middle = $"{middle}{type},{format}";
        
        var res = $"{head}{middle}{tail}";

        
        File.Delete(audioPath);
        var fs = new FileStream(audioPath, FileMode.CreateNew);
        var w = new StreamWriter(fs, Encoding.UTF8);
        w.Write(res);
        w.Flush();
        w.Close();
        fs.Close();

        EditorGame.Refresh();
        Log.LogPrint("ReadAudioNamesToEnum Success");
    }

    [MenuItem("Framework/CreateTool/ReadEventsClass", priority = 21)]
    private static void ReadEventsClass()
    {
        var eventsPath = $"{CodeCreateDir}/Event/Events.cs";
        var eventsClassPath = $"{CodeCreateDir}/Event/EventsClass.cs";
        var fileS = new FileStream(eventsClassPath, FileMode.Open);
        var readS = new StreamReader(fileS, Encoding.UTF8);
        var readContent = readS.ReadToEnd();
        readS.Close();
        fileS.Close();
        //Log.LogPrint(readContent);

        var r = new Regex(@"(?<=class\s)(\w+)(?=\s)");

        var typeStr = new List<string>();
        var cc = r.Matches(readContent);
        foreach (Match m in cc) typeStr.Add(m.Value);

        var head = @"public enum EventType {";
        var tail = @"}";
        var middle = "";
        foreach (var type in typeStr) middle = $"{middle}{type},{format}";

        var res = $"{head}{middle}{tail}";

        File.Delete(eventsPath);
        var fs = new FileStream(eventsPath, FileMode.CreateNew);
        var w = new StreamWriter(fs, Encoding.UTF8);
        w.Write(res);
        w.Flush();
        w.Close();
        fs.Close();

        EditorGame.Refresh();
        Log.LogPrint("ReadEventsClass Success");
    }

    /*private static void ReadEvents()
    {
        var eventsPath = $"{Application.dataPath}/Scripts/Events.cs";
        var eventsClassPath = $"{Application.dataPath}/Scripts/EventsClass.cs";
        var fileS = new FileStream(eventsPath, FileMode.Open);
        var readS = new StreamReader(fileS, Encoding.UTF8);
        var readContent = readS.ReadToEnd();
        readS.Close();
        fileS.Close();
        var lines = readContent.Split(new[] {"\r\n"}, StringSplitOptions.None);
        var list = lines.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();

        var res = new List<string>();

        for (var i = 0; i < list.Count(); i++)
        {
            if (i == 0 || i == 1 || i == list.Count() - 1) continue;

            var item = list[i];
            res.Add(item.Replace(",", "").Trim());
        }

        var resStr = "";
        foreach (var s in res) resStr = $"{resStr}public partial class {s} {{}};{format}";

        File.Delete(eventsClassPath);
        var fs = new FileStream(eventsClassPath, FileMode.CreateNew);
        var w = new StreamWriter(fs, Encoding.UTF8);
        w.Write(resStr);
        w.Flush();
        w.Close();
        fs.Close();
    }*/

    private static void UIAboutCreateFile(string className, HaveEvents haveE, string initClassStr)
    {
        var memberStr = ""; //成员变量字符串
        var findStr = ""; //查询代码字符串

        var bindStr = "";
        var funcStr = "";

        foreach (var kv in PanelDic)
        {
            var name = kv.Key;
            var childPath = kv.Value.Item1;
            var type = kv.Value.Item2;
            memberStr =
                $"{memberStr}private {type} {name}=null;{format}"; //memberstring += "private " + type + " " + name + " = null;\r\n\t";
            //loadedcontant += name + " = " + "transform.Find(" + "\"" + childPath + "\"" + ").GetComponent<" +    type +  ">();\r\n\t\t";
            switch (type)
            {
                case PanelUIType.Text:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").GetComponent<{type}>();{format}";
                    funcStr = $"{funcStr}public void {name}Refresh(string t)=>{name}.text=t;{format}";
                    break;
                case PanelUIType.Button:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").GetComponent<{type}>();{format}";
                    bindStr = $"{bindStr}{name}.onClick.AddListener(()=>{{{name}Action?.Invoke();}});{format}";
                    memberStr = $"{memberStr}public Action {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.Toggle:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").GetComponent<{type}>();{format}";
                    bindStr = $"{bindStr}{name}.onValueChanged.AddListener((b)=>{{{name}Action?.Invoke(b);}});{format}";
                    memberStr = $"{memberStr}public Action<bool> {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.Slider:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").GetComponent<{type}>();{format}";
                    bindStr = $"{bindStr}{name}.onValueChanged.AddListener((f)=>{{{name}Action?.Invoke(f);}});{format}";
                    memberStr = $"{memberStr}public Action<float> {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.Dropdown:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").GetComponent<{type}>();{format}";
                    bindStr = $"{bindStr}{name}.onValueChanged.AddListener((i)=>{{{name}Action?.Invoke(i);}});{format}";
                    memberStr = $"{memberStr}public Action<int> {name}Action{{get;set;}}{format}";
                    break;
                case PanelUIType.InputField:
                    break;
                case PanelUIType.Image:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").GetComponent<{type}>();{format}";
                    funcStr = $"{funcStr}public void {name}Refresh(Sprite s)=>{name}.sprite=s;{format}";
                    break;
                case PanelUIType.GameObject:
                    findStr =
                        $"{findStr}{name}=transform.Find(\"{childPath}\").gameObject;{format}";
                    funcStr =
                        $"{funcStr}private void {name}SetChild(Transform t)=>t.transform.SetParent({name}.transform, false);{format}";
                    break;
            }
        }

        if (haveE != null)
            foreach (var ev in haveE.EventTypes)
            {
                bindStr = $"{bindStr}Incident.DeleteEvent<{ev}>({ev}Callback);{format}";
                bindStr = $"{bindStr}Incident.RigisterEvent<{ev}>({ev}Callback);{format}";
            }

        var scriptPath = $"{CodeCreateDir}/Panel/{className}.cs";
        var classStr = "";
        if (File.Exists(scriptPath)) //如果已经存在了脚本，则只替换//auto下方的字符串
        {
            var classfile = new FileStream(scriptPath, FileMode.Open);
            var read = new StreamReader(classfile);
            var readStr = read.ReadToEnd();

            read.Close();
            classfile.Close();
            File.Delete(scriptPath);

            var splitStr = "//auto";
            var unchangeStr = Regex.Split(readStr, splitStr, RegexOptions.IgnoreCase)[0];
            var changeStr = Regex.Split(initClassStr, splitStr, RegexOptions.IgnoreCase)[1];

            classStr = $"{unchangeStr}{splitStr}{changeStr}";
        }
        else
        {
            classStr = initClassStr;
        }

        classStr = classStr.Replace("#类名#", className);
        classStr = classStr.Replace("#查找#", findStr);
        classStr = classStr.Replace("#成员#", memberStr);
        classStr = classStr.Replace("#绑定#", bindStr);
        classStr = classStr.Replace("#函数#", funcStr);

        var file = new FileStream(scriptPath, FileMode.CreateNew); //指示操作系统应创建新文件，如果文件已经存在，将引发异常
        var fileW = new StreamWriter(file, Encoding.UTF8);
        fileW.Write(classStr);
        fileW.Flush(); //强制写入，确保数据完整
        fileW.Close(); //不需要dispose，下次接着玩
        file.Close(); //fileStrem最后关闭

        EditorGame.Refresh();

        Log.LogPrint("Panel Or UI Create Success");
    }

    private static string PanelGetPath(Transform root, Transform cur)
    {
        var temp = cur;
        var sb = new StringBuilder();

        while (temp != root)
        {
            sb.Insert(0, temp.name);
            sb.Insert(0, "/");
            temp = temp.parent;
        }

        sb.Remove(0, 1);
        var res = sb.ToString();
        return res;
    }

    private static PanelUIType PanelGetUIType(string name)
    {
        foreach (var uiType in PanelTypes)
        {
            var typeName = uiType.ToString();
            if (name.Contains(typeName)) return uiType;
        }

        return PanelUIType.Null;
    }

    private static bool PanelIsNeedWriteObj(string name)
    {
        foreach (var black in PanelBlacklist)
            if (name.StartsWith(black))
                return false;

        return true;
    }

    private static void PanelTraverse(Transform t, Transform root)
    {
        for (var i = 0; i < t.childCount; i++)
        {
            var c = t.GetChild(i);
            var name = c.name;
            if (PanelIsNeedWriteObj(name))
            {
                if (PanelDic.ContainsKey(name) == false)
                {
                    var path = PanelGetPath(root, c);
                    var type = PanelGetUIType(name);
                    (string, PanelUIType) tu = (path, type);
                    if (tu.Item2 != PanelUIType.Null)
                    {
                        PanelDic.Add(name, tu);
                    }
                }
                else
                {
                    //todo old 
                    Log.LogWarning("already have the key name is " + name);
                }
            }

            PanelTraverse(c, root);
        }
    }


    [MenuItem("Assets/CreateTool/CreateLua", false, 1)]
    static void CreateLua()
    {
        string head = @"
-------------------------------------------------------                                                             
-- author : sky_allen                                                                                                                  
--  email : 894982165@qq.com      
--   time : $time$                                                                                           
-------------------------------------------------------

---@class $table$
local $table$ = class('$table$')

function $table$.Init()
    Log.Info('$table$.Init')
end

return $table$
";
        CreateTool.CrateAssetsTxtFile(".lua", fileContain: head);
    }

    [MenuItem("Assets/CreateTool/RenameLuaAndCopyPath", false, 2)]
    static void RenameLuaAndCopyPath()
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var realPath = EditorGame.GetAssetsPathAbsolute(selectFolderPath);

        StreamReader reader = new StreamReader(realPath, Encoding.UTF8);
        String content = reader.ReadToEnd();
        reader.Close();

        var tempStrArray = selectFolderPath.Split('/');
        var fileName = tempStrArray[tempStrArray.Length - 1].Split('.')[0];

        if (content.Contains("$table$"))
        {
            content = content.Replace("$table$", fileName);
            content = content.Replace("$time$", DateTime.Now.ToString());

            File.WriteAllText(realPath, content, new UTF8Encoding(false));

            AssetDatabase.Refresh();
        }


        string copyPath = "";
        for (int i = 0; i < tempStrArray.Length; i++)
        {
            if ((i == 0) || (i == 1) || (i == 2) || (i == tempStrArray.Length - 1))
            {
                continue;
            }

            copyPath += tempStrArray[i] + "/";
        }

        copyPath = Path.Combine(copyPath, fileName);
        EditorGame.CopyToBoard(copyPath);
    }
}

//Fill Handle Item Scrollbar
public enum PanelUIType
{
    Null,
    Button,
    Text,
    Toggle,
    Slider,
    Dropdown,
    InputField,
    GameObject,
    Image
}