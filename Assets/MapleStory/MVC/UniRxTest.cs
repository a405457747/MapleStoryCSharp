/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using static MapleStory.LogNote;
using UniRx;

public class UniRxTest : MonoBehaviour
{
    public GameObject UniRxGO;
    
    ReactiveCollection<GameObject> gos = new ReactiveCollection<GameObject>();

    private void Start()
    {

        gos.ObserveAdd(). Subscribe(_ =>
        {

                print("addItem is ");

        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           var go= GameObject.Instantiate(UniRxGO);
           gos.Add(go);//todo warn 这里报错暂时不知道如何解决。
        }
    }
}
*/