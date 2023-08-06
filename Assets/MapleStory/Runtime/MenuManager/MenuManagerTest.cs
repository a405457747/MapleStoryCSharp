using System;
using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;
using UnityEngine.UI;


//todo warn 实现类似python的装饰函数
public class MenuManagerTest : MonoBehaviour
{
  public Image img;


  private void FixedUpdate()
  {
    if (Input.GetKeyDown(KeyCode.A))
    {
      img.gameObject.GetComponent<IActive>().Show();
      
    }else if (Input.GetKeyDown(KeyCode.S))
    {
      img.gameObject.GetComponent<IActive>().Hide();
    }
  }
}
