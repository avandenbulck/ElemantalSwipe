using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenEnemyDestroyed : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy.OnDestroy += DestroyObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
