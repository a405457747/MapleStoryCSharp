using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    Rigidbody2D rb2d;

    Vector2 targetPos;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetPos = new Vector2(1, 1);
    }

    private void FixedUpdate()
    {
       // rb2d.MovePosition((Vector2)transform.localPosition + targetPos*Time.fixedDeltaTime);
    }

}
