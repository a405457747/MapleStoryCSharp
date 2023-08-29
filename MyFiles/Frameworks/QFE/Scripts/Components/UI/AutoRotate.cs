using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class AutoRotate : MonoBehaviour//���ֻ��UI�õ�Ŷ
    {
        public float RotateSpeed = 300;

        void Update()
        {
            transform.Rotate(-Vector3.forward * RotateSpeed * Time.deltaTime);
        }
    }
}