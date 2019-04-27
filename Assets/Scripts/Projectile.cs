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

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shoot(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction.normalized * speed;
    }

    public void Hit(bool objectWasVulnerable, GameObject objectHit)
    {
        if (!objectWasVulnerable || !survivesOnVulnerableHit)
        {
            if(!objectWasVulnerable)
                AudioManager.instance.PlayResistantHitSound();

            Instantiate(prefabToSpawnOnDeath, pointToSpawnPrefabOnDeath.position, Quaternion.identity);
            DestroyGameObject();
        }    
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}