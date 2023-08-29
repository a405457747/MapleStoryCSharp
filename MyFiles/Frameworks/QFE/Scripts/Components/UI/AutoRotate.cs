using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class AutoRotate : MonoBehaviour//这个只给UI用的哦
    {
        public float RotateSpeed = 300;

        void Update()
        {
            transform.Rotate(-Vector3.forward * RotateSpeed * Time.deltaTime);
        }
    }
}