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
        if(enemyScript != null)
            enemyScript.OnDeath += StopMoving;
    }

    public void Update()
    {
        if (moving)
        {
            Quaternion previousRot = transform.rotation;
            transform.RotateAround(rotateAroundPoint.position, Vector3.forward, speed * Time.deltaTime);
            transform.rotation = previousRot;
        }
            
    }

    public void StopMoving()
    {
        moving = false;
    }
}
