using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<ElementType> elementWeaknesses;
    public event Action OnDeath = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            Destroy(collision.gameObject);
            if (elementWeaknesses.Contains(projectile.elementType))
            {
                Destroy(this.gameObject);
                AudioManager.instance.PlayVunerableHitSound();
                OnDeath.Invoke();
            }
            else
                AudioManager.instance.PlayResistantHitSound();
        }
    }
}
