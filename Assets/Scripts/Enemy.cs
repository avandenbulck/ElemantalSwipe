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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        Projectile collidedProjectile = collidedGameObject.GetComponent<Projectile>();
        if (collidedProjectile != null)
        {
            Destroy(collidedGameObject);
            if (elementWeaknesses.Contains(collidedProjectile.elementType))
            {           
                AudioManager.instance.PlayVunerableHitSound();
                OnDeath.Invoke();
                animator.Play("Destroyed");
                DisableColliders();
            }
            else
                AudioManager.instance.PlayResistantHitSound();
        }
    }

    private void DisableColliders()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void DestroyObject()
    {
        OnDestroy.Invoke();
        Destroy(this.gameObject);
    }
}
