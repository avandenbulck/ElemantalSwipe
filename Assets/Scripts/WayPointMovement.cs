using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    int waypointIndex;
    Rigidbody2D rb;

    bool moving;

    void Start()
    {
        waypointIndex = 0;
        moving = true;
        rb = GetComponent<Rigidbody2D>();
        Enemy enemyScript = GetComponent<Enemy>();
        if(enemyScript != null)
            enemyScript.OnDeath += StopMoving;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length > 0)
        {
            if (moving)
            {
                MoveCloserToWaypoint();   
            }
            
        }
        else
            Debug.LogError("No waypoints on WayPointMovent");
    }

    public void StopMoving()
    {
        moving = false;
    }

    private void MoveCloserToWaypoint()
    {
        Vector2 positionToMoveTo = waypoints[waypointIndex].position;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, positionToMoveTo, speed * Time.deltaTime);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, positionToMoveTo) < float.Epsilon)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
                waypointIndex = 0;
        }
    }
}
