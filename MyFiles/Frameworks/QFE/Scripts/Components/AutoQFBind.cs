using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class AutoQFBind : MonoBehaviour
    {
        void Start()
        {
            var monos = GetComponents<MonoBehaviour>();
            foreach (var mono in monos)
            {
             GameManager.Instance.QFrameworkContainer.Inject(mono);   
            }
        }
    }
}