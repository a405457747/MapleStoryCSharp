using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ܿ��ܲ�̫��һֱupdate,�պ��ع�֮
public class InjuredWhitening : MonoBehaviour
{
    public float whiteCoolTime = 0.025f;//����Ч���ĳ���ʱ�䳤��
    SpriteRenderer spriteRenderer;
    float whiteCoolTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //������״̬��ȴ�����ñ���������Ϊ0
        if (whiteCoolTimer <= 0)
            spriteRenderer.material.SetInt("_BeAttack", 0);
        else
            whiteCoolTimer -= Time.deltaTime;
    }

    public void BeAttack()
    {
        //��������ʱ������shader�ı���������Ϊ1
        spriteRenderer.material.SetInt("_BeAttack", 1);
        whiteCoolTimer = whiteCoolTime;
    }
}
