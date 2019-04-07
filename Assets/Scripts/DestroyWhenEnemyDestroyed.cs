using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenEnemyDestroyed : MonoBehaviour
{
    public Enemy enemy;

    void Start()
    {
        enemy.OnDestroy += DestroyObject;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
