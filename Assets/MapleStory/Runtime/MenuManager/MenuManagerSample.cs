using System;
using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;
using UnityEngine.UI;


public class MenuManagerSample : MonoBehaviour
{
    public Image img;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            img.gameObject.GetComponent<IActive>().Show();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            img.gameObject.GetComponent<IActive>().Hide();
        }
    }
}
