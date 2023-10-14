using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    public float duration = 1f;
    public float rotationAmount = 360f;

    private void Start()
    {
        // 沿 Z 轴旋转指定角度
        transform.DORotate(new Vector3(0f, 0f, rotationAmount), duration, RotateMode.FastBeyond360)
            // 循环旋转
            .SetLoops(-1, LoopType.Restart)
            // 运动曲线为线性
            .SetEase(Ease.Linear);
    }
}
