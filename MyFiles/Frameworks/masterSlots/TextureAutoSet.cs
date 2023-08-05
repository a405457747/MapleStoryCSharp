using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TextureAutoSet : EditorWindow
{
    static int[] formatSize = new int[]
    {
        32, 64, 128, 256, 512, 1024, 2048, 4096
    };

    [MenuItem("Assets/TextureAutoSet/设置文件夹以及子文件夹下面的图片压缩格式为ASTC", priority = 0)]
    static void AutoSetASTC()
    {
        string[] guidArray = Selection.assetGUIDs;
        foreach (var item in guidArray)
        {
            string selectFloder = AssetDatabase.GUIDToAssetPath(item);
            DirectoryInfo root = new DirectoryInfo(selectFloder);
            GetFloder(root);
        }
    }

    static bool IsBackGround(string fileName)
    {
        List<string> names = new List<string>()
        {
            "bj",
            "Bj",
            "BJ",
            "bJ",

            "BG",
            "Bg",
            "bg",
            "bG",
        };

        bool isContain = false;

        foreach (var item in names)
        {
            if (fileName.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    static void GetFloder(DirectoryInfo root)
    {
        GetFile(root);
        //查找子文件夹
        DirectoryInfo[] array = root.GetDirectories();
        //Debug.Log(root);
        foreach (DirectoryInfo item in array)
        {
            GetFloder(item);
        }
    }

    static void GetFile(DirectoryInfo root)
    {
        //DirectoryInfo root = new DirectoryInfo(path);
        FileInfo[] fileDic = root.GetFiles();
        foreach (var file in fileDic)
        {
            //sDebug.Log(file);
            if (file.FullName.EndsWith(".png") || file.FullName.EndsWith(".jpg") || file.FullName.EndsWith(".tga") ||
                file.FullName.EndsWith(".psd") || file.FullName.EndsWith(".PSD") || file.FullName.EndsWith(".exr") ||
                file.FullName.EndsWith(".tif"))
            {
                //Debug.Log("-------------" + file.FullName);
                //Debug.Log(Application.dataPath);
                string fullpath = file.FullName.Replace("\\", "/");
                string path = fullpath.Replace(Application.dataPath, "Assets");
                // TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                SetPicFormat3(path,file.Name.Split('.')[0]);
            }
        }
    }

    static void SetPicFormat3(string path,string n)
    {
        ImporterSet(AssetImporter.GetAtPath(path) as TextureImporter,path,n);
    }

    static void ImporterSet(TextureImporter importer,string path,string n)
    {
        Log.LogParas(n);

        bool isNeedImport1 = false;
        bool isNeedImport2 = false;
        
        importer.textureType = TextureImporterType.Sprite;

      //  importer.textureCompression = TextureImporterCompression.Compressed;
        
        TextureImporterPlatformSettings settings = importer.GetPlatformTextureSettings("iPhone");
        bool isPowerOfTwo = IsPowerOfTwo(importer);
        TextureImporterFormat defaultAlpha =
            isPowerOfTwo ? TextureImporterFormat.PVRTC_RGBA4 : TextureImporterFormat.ASTC_4x4;
        TextureImporterFormat defaultNotAlpha =
            isPowerOfTwo ? TextureImporterFormat.PVRTC_RGB4 : TextureImporterFormat.ASTC_6x6;
        isNeedImport1 = settings.overridden == false;
        settings.overridden = true;

        if (IsBackGround(n))
        {
             settings.maxTextureSize = 1024;
        }
        
        settings.format = importer.DoesSourceTextureHaveAlpha() ? defaultAlpha : defaultNotAlpha;
        
        settings.format =TextureImporterFormat.ASTC_6x6;

        
        importer.SetPlatformTextureSettings(settings);

        settings = importer.GetPlatformTextureSettings("Android");
        isNeedImport2 = settings.overridden == false;
        settings.overridden = true;

        if (IsBackGround(n))
        {
             settings.maxTextureSize = 1024;
        }
        
        settings.allowsAlphaSplitting = false;
        bool divisible4 = IsDivisibleOf4(importer);
        defaultAlpha = divisible4 ? TextureImporterFormat.ETC2_RGBA8Crunched : TextureImporterFormat.ASTC_4x4;
        defaultNotAlpha = divisible4 ? TextureImporterFormat.ETC_RGB4Crunched : TextureImporterFormat.ASTC_6x6;

        settings.format = importer.DoesSourceTextureHaveAlpha() ? defaultAlpha : defaultNotAlpha;
        
        settings.format =TextureImporterFormat.ASTC_6x6;
        
        importer.SetPlatformTextureSettings(settings);

        if ( isNeedImport1|| isNeedImport2)
        {
            Log.LogParas("need import and path is "+path);
            AssetDatabase.ImportAsset(path);
        }

        Debug.Log("set over");
    }

    static void SetPicFormat2(string path)
    {
        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
        ti.textureCompression = TextureImporterCompression.Compressed;
        ti.mipmapEnabled = false;

        TextureImporterPlatformSettings settings = ti.GetPlatformTextureSettings("Android");
        settings.overridden = true;
        settings.format = ti.DoesSourceTextureHaveAlpha()
            ? TextureImporterFormat.ASTC_RGBA_6x6
            : TextureImporterFormat.ASTC_RGB_6x6;

        settings.compressionQuality = 50;
        ti.SetPlatformTextureSettings(settings);
        Debug.Log(settings);
    }


    static void SetPicFormat(string path)
    {
        Debug.Log(path);
        return;
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer.mipmapEnabled == true)
        {
            importer.mipmapEnabled = false;
        }

        TextureImporterPlatformSettings settings = importer.GetPlatformTextureSettings("iOS");
        settings.overridden = true;

        //判断图片大小
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        int textureSize = Mathf.Max(texture.height, texture.width);
        //Debug.Log(textureSize);
        int SizeType = FitSize(textureSize);

        //Android   iPhone
        if (importer.DoesSourceTextureHaveAlpha())
        {
            //ios版本
            importer.SetPlatformTextureSettings("iPhone", SizeType, TextureImporterFormat.ASTC_RGBA_6x6);
            //安卓版本
            //importer.SetPlatformTextureSettings("Android", SizeType, TextureImporterFormat.ETC2_RGBA8);
        }
        else
        {
            //ios版本
            importer.SetPlatformTextureSettings("iPhone", SizeType, TextureImporterFormat.ASTC_RGB_6x6);
            //安卓版本
            //importer.SetPlatformTextureSettings("Android", SizeType, TextureImporterFormat.ETC2_RGB4);
        }
    }

    static int FitSize(int picValue)
    {
        foreach (var one in formatSize)
        {
            if (picValue <= one)
            {
                return one;
            }
        }

        return 1024;
    }

    //被4整除
    static bool IsDivisibleOf4(TextureImporter importer)
    {
        (int width, int height) = GetTextureImporterSize(importer);
        return (width % 4 == 0 && height % 4 == 0);
    }

    //2的整数次幂
    static bool IsPowerOfTwo(TextureImporter importer)
    {
        (int width, int height) = GetTextureImporterSize(importer);
        return (width == height) && (width > 0) && ((width & (width - 1)) == 0);
    }

    /*//贴图不存在、meta文件不存在、图片尺寸发生修改需要重新导入
    bool IsFirstImport(TextureImporter importer)
    {
        (int width, int height) = GetTextureImporterSize(importer);
        Texture tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        bool hasMeta = File.Exists(AssetDatabase.GetAssetPathFromTextMetaFilePath(assetPath));
        return tex == null || !hasMeta || (tex.width != width && tex.height != height);
    }*/

    //获取导入图片的宽高
    static (int, int) GetTextureImporterSize(TextureImporter importer)
    {
        if (importer != null)
        {
            object[] args = new object[2];
            MethodInfo mi =
                typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(importer, args);
            return ((int) args[0], (int) args[1]);
        }

        return (0, 0);
    }
}