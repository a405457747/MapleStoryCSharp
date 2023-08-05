using System.Collections.Generic;
using System;
using System.Collections;
using Coffee.UIEffects;
using UnityEngine;
using XLua;

public static class LuaManualConfig
{
    /*[LuaCallCSharp]
    [ReflectionUse]
    public static List<Type> DOTWEEN_LUA_CALL_CS_LIST = new List<Type>()
    {
        typeof(DG.Tweening.AutoPlay),
        typeof(DG.Tweening.AxisConstraint),
        typeof(DG.Tweening.Ease),
        typeof(DG.Tweening.LogBehaviour),
        typeof(DG.Tweening.LoopType),
        typeof(DG.Tweening.PathMode),
        typeof(DG.Tweening.PathType),
        typeof(DG.Tweening.RotateMode),
        typeof(DG.Tweening.ScrambleMode),
        typeof(DG.Tweening.TweenType),
        typeof(DG.Tweening.UpdateType),

        typeof(DG.Tweening.DOTween),
        typeof(DG.Tweening.DOVirtual),
        typeof(DG.Tweening.EaseFactory),
        typeof(DG.Tweening.Tweener),
        typeof(DG.Tweening.Tween),
        typeof(DG.Tweening.Sequence),
        typeof(DG.Tweening.TweenParams),
        typeof(DG.Tweening.Core.ABSSequentiable),

        typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),

        typeof(DG.Tweening.TweenCallback),
        typeof(DG.Tweening.TweenExtensions),
        typeof(DG.Tweening.TweenSettingsExtensions),
        typeof(DG.Tweening.ShortcutExtensions),

        //dotween pro 的功能
        //typeof(DG.Tweening.DOTweenPath),
        //typeof(DG.Tweening.DOTweenVisualManager),
    };*/

    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [ReflectionUse] [LuaCallCSharp] public static List<Type> LuaCallCSharp = new List<Type>()
    {
        typeof(UnityEngine.Application),
        typeof(System.Object),
        typeof(UnityEngine.Object),
        typeof(Vector2),
        typeof(Vector3),
        typeof(Vector4),
        typeof(Quaternion),
        typeof(Color),
        typeof(Ray),
        typeof(Bounds),
        typeof(Ray2D),
        typeof(Time),
        typeof(GameObject),
        typeof(Component),
        typeof(Behaviour),
        typeof(Transform),
        typeof(RectTransform),
        typeof(Resources),
        typeof(TextAsset),
        typeof(Keyframe),
        typeof(AnimationCurve),
        typeof(AnimationClip),
        typeof(MonoBehaviour),
        typeof(ParticleSystem),
        typeof(SkinnedMeshRenderer),
        typeof(Renderer),
        typeof(WWW),
        typeof(Light),
        typeof(Mathf),
        typeof(System.Collections.Generic.List<int>),
        typeof(Action<string>),
        typeof(UnityEngine.Debug),
        typeof(LuaSystem),
        typeof(Canvas),
        typeof(TransformExpand),

        typeof(DG.Tweening.AutoPlay),
        typeof(DG.Tweening.AxisConstraint),
        typeof(DG.Tweening.Ease),
        typeof(DG.Tweening.LogBehaviour),
        typeof(DG.Tweening.LoopType),
        typeof(DG.Tweening.PathMode),
        typeof(DG.Tweening.PathType),
        typeof(DG.Tweening.RotateMode),
        typeof(DG.Tweening.ScrambleMode),
        typeof(DG.Tweening.TweenType),
        typeof(DG.Tweening.UpdateType),

        typeof(DG.Tweening.DOTween),
        typeof(DG.Tweening.DOVirtual),
        typeof(DG.Tweening.EaseFactory),
        typeof(DG.Tweening.Tweener),
        typeof(DG.Tweening.Tween),
        typeof(DG.Tweening.Sequence),
        typeof(DG.Tweening.TweenParams),
        typeof(DG.Tweening.Core.ABSSequentiable),
        typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),

        typeof(DG.Tweening.Core.TweenerCore<Vector2, Vector2, DG.Tweening.Plugins.Options.VectorOptions>),
        typeof(DG.Tweening.Core.DOGetter<Vector2>),
        typeof(DG.Tweening.Core.DOSetter<Vector2>),
        typeof(DG.Tweening.Core.DOTweenComponent),
        typeof(DG.Tweening.Core.DOTweenUtils),
        typeof(DG.Tweening.Core.DOTweenExternalCommand),
        typeof(DG.Tweening.DOTweenModuleUI),
        typeof(DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.VectorOptions>),

        typeof(DG.Tweening.TweenCallback),
        typeof(DG.Tweening.TweenExtensions),
        typeof(DG.Tweening.TweenSettingsExtensions),
        typeof(DG.Tweening.ShortcutExtensions),

        // typeof(DG.Tweening.ShortcutExtensions43),
        // typeof(DG.Tweening.ShortcutExtensions46),
        // typeof(DG.Tweening.ShortcutExtensions50),

        //dotween pro 的功能
        //typeof(DG.Tweening.DOTweenPath),
        //typeof(DG.Tweening.DOTweenVisualManager)

        typeof(System.Func<bool>),
        typeof(LuaMono),

        typeof(IOHelper),
        typeof(IOHelpLua),
        typeof(Coroutine),
        typeof(IEnumerator)
        ,
      //  typeof(Coffee.UIExtensions.UIParticle),
        typeof(UIEffect),
        typeof(Differences),
        
        typeof(Game),
        typeof(AdvertSystem),
        typeof(PurchaseSystem),
    };

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua] public static List<Type> CSharpCallLua = new List<Type>()
    {
        typeof(Canvas),
        typeof(LuaMono),
        typeof(LuaSystem),

        typeof(Action),
        typeof(Func<double, double, double>),
        typeof(Action<string>),
        typeof(Action<double>),
        typeof(Action<float>),
        typeof(Action<int>),
        typeof(Action<bool>),
        typeof(Action<LuaTable>),
        typeof(Action<LuaTable,string>),
        typeof(UnityEngine.Events.UnityAction),
        typeof(System.Collections.IEnumerator),
        
        typeof(UnityEngine.Events.UnityAction<float>),

        typeof(Func<bool>),
        typeof(UnityEngine.Events.UnityAction<float>),
    };

    //黑名单
    [BlackList] public static List<List<string>> BlackList = new List<List<string>>()
    {
        new List<string>() {"UnityEngine.Light", "shadowAngle"},
        new List<string>() {"UnityEngine.Light", "shadowRadius"},
        new List<string>() {"UnityEngine.Light", "SetLightDirty"},

        new List<string>() {"System.Xml.XmlNodeList", "ItemOf"},
        new List<string>() {"UnityEngine.WWW", "movie"},
#if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
#endif
        new List<string>() {"UnityEngine.Texture2D", "alphaIsTransparency"},
        new List<string>() {"UnityEngine.Security", "GetChainOfTrustValue"},
        new List<string>() {"UnityEngine.CanvasRenderer", "onRequestRebuild"},
        new List<string>() {"UnityEngine.Light", "areaSize"},
        new List<string>() {"UnityEngine.Light", "lightmapBakeType"},
        new List<string>() {"UnityEngine.WWW", "MovieTexture"},
        new List<string>() {"UnityEngine.WWW", "GetMovieTexture"},
        new List<string>() {"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
#if !UNITY_WEBPLAYER
        new List<string>() {"UnityEngine.Application", "ExternalEval"},
#endif
        new List<string>() {"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
        new List<string>() {"UnityEngine.Component", "networkView"}, //4.6.2 not support
        new List<string>()
            {"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
        new List<string>() {"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
        new List<string>()
            {"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
        new List<string>()
            {"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
        new List<string>()
        {
            "System.IO.DirectoryInfo", "CreateSubdirectory", "System.String",
            "System.Security.AccessControl.DirectorySecurity"
        },
        new List<string>() {"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
        new List<string>() {"UnityEngine.MonoBehaviour", "runInEditMode"},
    };
}