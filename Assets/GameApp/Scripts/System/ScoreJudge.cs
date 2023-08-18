using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreJudge : MonoBehaviour
{

    public List<User> users;

    private void Awake()
    {
        users = new List<User>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            users.Add(new User());
        }
    }

   

}
