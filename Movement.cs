using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float dirx, movespeed = 7f;
    bool moveRight = true;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 26.89f)
        {
            moveRight = false;
        }
        if (transform.position.x < 12.51f)
        {
            moveRight = true;
        }
        
        if (moveRight == true)
        {
            transform.position = new Vector2(transform.position.x + movespeed * Time.deltaTime, transform.position.y);
        }
        if (moveRight == false)
        {
            transform.position = new Vector2(transform.position.x - movespeed * Time.deltaTime, transform.position.y);
        }
    }
}
