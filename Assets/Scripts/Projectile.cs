using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ElementType elementType;
    public GameObject prefabToSpawnOnDeath;
    public Transform pointToSpawnPrefabOnDeath;
    public float speed;
    public bool survivesOnVulnerableHit;
    public bool bounces;

    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Shoot(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
    }

    public void Hit(bool objectWasVulnerable, bool bouncableObject, Vector2 normalToBounce)
    {
        if (!objectWasVulnerable || !survivesOnVulnerableHit)
        {
            if(bounces && bouncableObject)
            {
                AudioManager.instance.PlayBounceSound();
                Vector2 newVelocity = Vector2.Reflect(rb.velocity, normalToBounce);
                rb.velocity = newVelocity;
            } else
            {
                if (!objectWasVulnerable)
                    AudioManager.instance.PlayResistantHitSound();

                Instantiate(prefabToSpawnOnDeath, pointToSpawnPrefabOnDeath.position, Quaternion.identity);
                DestroyGameObject();
            }     
        }    
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}