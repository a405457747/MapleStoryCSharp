/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using CallPalCatGames.Singleton;
using DG.Tweening;
using UnityEngine;
using XLua;


namespace CallPalCatGames.ActivityManager
{
    
    /// <summary>
    ///     各种活动管理器，主要做动画一个接口统一管理2D、3D、UI、看不到的物体（旋转 缩放 平移 渐变 晃动 字体过渡 插值效果 带回调 带队列），需要控制具体时间秒数的动画，可能就用DoTween直接做不封装。
    /// </summary>
    [LuaCallCSharp]
    public class ActivityManager : MonoSingleton<ActivityManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        ///     果冻效果
        /// </summary>
        /// <param name="trans"></param>
        public void JellyEffect(Transform trans, GameObjectType gameObjectType = GameObjectType.Type2D)
        {
            if (gameObjectType == GameObjectType.Type2D || gameObjectType == GameObjectType.TypeUI)
                trans.DOPunchScale(new Vector3(-0.2f, 0.2f, 0f), 0.35f, 12, 0.5f)
                    .OnComplete(() => { trans.localScale = Vector3.one; });
        }

        /// <summary>
        ///     旋转效果
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="gameObjectType"></param>
        public void RotateEffect(Transform trans, GameObjectType gameObjectType = GameObjectType.Type2D)
        {
            if (gameObjectType == GameObjectType.Type2D || gameObjectType == GameObjectType.TypeUI)
                trans.DORotate(Vector3.forward * 360, 1f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear).SetLoops(-1);
        }

        /// <summary>
        ///     变大后复原
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="gameObjectType"></param>
        public void ChangeBigToNormal(Transform transform, GameObjectType gameObjectType = GameObjectType.Type2D)
        {
            if (gameObjectType == GameObjectType.Type2D || gameObjectType == GameObjectType.TypeUI)
            {
                float initRatio = 1f, totalTime = 0.2f, bigRatio = 1.32f;
                transform.DOScale(bigRatio * initRatio, totalTime / 2f).OnComplete(() =>
                {
                    transform.DOScale(initRatio, totalTime / 2f);
                });
            }
        }

        /// <summary>
        ///     震动效果。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initCameraPos"></param>
        public void ShakeEffect(Transform transform, Vector3 initCameraPos)
        {
            transform.DOShakePosition(0.5f).OnComplete(() => { transform.localPosition = initCameraPos; });
        }
    }

    /// <summary>
    ///     根据种类不同来做动画
    /// </summary>
    public enum GameObjectType
    {
        Null,
        /// <summary>
        ///     看不见的游戏物体比如相机
        /// </summary>
        TypeOutOfSight,
        Type2D,
        Type3D,
        TypeUI
    }
}