using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICall 
{
    void Initialize(string name);
    void Release();
    void EachFrame();
    void Show();
    void Hide();
}
