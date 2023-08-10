using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//性能可能不太好一直update,日后重构之
public class InjuredWhitening : MonoBehaviour
{
    public float whiteCoolTime = 0.025f;//泛白效果的持续时间长度
    SpriteRenderer spriteRenderer;
    float whiteCoolTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //被攻击状态冷却，设置被攻击参数为0
        if (whiteCoolTimer <= 0)
            spriteRenderer.material.SetInt("_BeAttack", 0);
        else
            whiteCoolTimer -= Time.deltaTime;
    }

    public void BeAttack()
    {
        //被攻击的时候设置shader的被攻击参数为1
        spriteRenderer.material.SetInt("_BeAttack", 1);
        whiteCoolTimer = whiteCoolTime;
    }
}
