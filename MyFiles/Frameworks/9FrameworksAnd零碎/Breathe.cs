using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour
{
    private Vector3 _originalScale;
    [Range(0, 1)] public float incrementScale=0.03f;
    [Range(0, 1)] public float changeSpeed=0.05f;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = _originalScale * (Mathf.PingPong(Time.time*changeSpeed, incrementScale) + 1);
    }
}