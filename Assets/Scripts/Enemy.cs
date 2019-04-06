using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<ElementType> elementWeaknesses;
    public event Action OnDeath = delegate { };
    public event Action OnDestroy = delegate { };

    Animator animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
                
                AudioManager.instance.PlayVunerableHitSound();
                OnDeath.Invoke();
                animator.Play("Destroyed");
                Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = false;
                }
            }
            else
                AudioManager.instance.PlayResistantHitSound();
        }
    }

    public void DestroyObject()
    {
        OnDestroy.Invoke();
        Destroy(this.gameObject);
    }
}
