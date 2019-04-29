using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public List<ElementType> elementWeaknesses;
    public UnityEvent OnDeath;
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
            bool isVulnerableToElement = elementWeaknesses.Contains(collidedProjectile.elementType);
            collidedProjectile.Hit(isVulnerableToElement, false, Vector2.zero);
            if (isVulnerableToElement)
            {           
                AudioManager.instance.PlayVunerableHitSound();
                OnDeath.Invoke();
                animator.Play("Destroyed");
                DisableColliders();
            }             
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

    public void DestroyGameObject()
    {
        OnDestroy.Invoke();
        Destroy(this.gameObject);
    }
}