using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerFire : MonoBehaviour
{
    public float speed = 2;

    public Vector3 moveDirection;
    public float moveDistance;
    private Vector3 startPos;

    private bool movingToStart;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.Rotate(0, speed, 0);
        if (movingToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            if (transform.position == startPos)
            {
                movingToStart = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos + (moveDirection * moveDistance), speed * Time.deltaTime);
            if (transform.position == startPos + (moveDirection * moveDistance))
            {
                movingToStart = true;

            }
        }
    }   
}