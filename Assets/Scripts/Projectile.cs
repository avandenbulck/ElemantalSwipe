﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ElementType elementType;
    public GameObject prefabToSpawnOnDeath;
    public Transform pointToSpawnPrefabOnDeath;
    public float speed;

    public void Shoot(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction.normalized * speed;
    }

    public void Hit(bool objectWasVulnerable)
    {
        if (!objectWasVulnerable)
        {
            Instantiate(prefabToSpawnOnDeath, pointToSpawnPrefabOnDeath.position, Quaternion.identity);
            AudioManager.instance.PlayResistantHitSound();
        }
        
        Destroy(this.gameObject);
    }
}
