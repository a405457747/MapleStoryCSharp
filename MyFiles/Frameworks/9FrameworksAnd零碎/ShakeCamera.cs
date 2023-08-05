using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private float shakeTime;
    private float fps;
    private float frameTime;
    private float shakeDelta;
    private Camera cam;
    private static bool isshakeCamera;

    private float maxShakeTime;
    private float maxFPS;
    private float maxFrameTime;
    private float maxShakeDelta;

    private void Awake()
    {
        isshakeCamera = false;
        cam = Camera.main;
        //参数这里来设置呢
        ReturnOriginalData();
        DataRecovery();
    }

    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                    DataRecovery();
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        cam.rect = new Rect(shakeDelta * (-1.0f + 2.0f * Random.value), shakeDelta * (-1.0f + 2.0f * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }

    void DataRecovery()
    {
        shakeTime = maxShakeTime;
        fps = maxFPS;
        frameTime = maxFrameTime;
        shakeDelta = maxShakeDelta;
    }

    void ReturnOriginalData(float maxShakeTime = 1f, float maxFPS = 20f, float maxFrameTime = 0.03f, float maxShakeDelta = 0.005f)
    {
        this.maxShakeTime = maxShakeTime;
        this.maxFPS = maxFPS;
        this.maxFrameTime = maxFrameTime;
        this.maxShakeDelta = maxShakeDelta;
    }

    public static void shakeCamera()
    {
        isshakeCamera = true;
    }
}
