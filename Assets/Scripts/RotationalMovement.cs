using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalMovement : MonoBehaviour
{
    public int speed;
    public Transform rotateAroundPoint;

    Rigidbody2D rb;
    bool moving;

    void Start()
    {
        moving = true;
        rb = GetComponent<Rigidbody2D>();
        Enemy enemyScript = GetComponent<Enemy>();
        enemyScript.OnDeath += StopMoving;
    }

    public void Update()
    {
        if(moving)
            transform.RotateAround(rotateAroundPoint.position, Vector3.forward, speed * Time.deltaTime);
    }

    public void StopMoving()
    {
        moving = false;
    }
}
