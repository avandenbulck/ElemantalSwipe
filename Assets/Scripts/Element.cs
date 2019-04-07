using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public ElementType elementType;
    public GameObject projectilePrefab;

    Transform trans;

    public void Awake()
    {
        trans = GetComponent<Transform>();
    }

    public void ShootProjectile(Vector2 angle)
    {
        float eulerAngleToShootAt = Vector2.SignedAngle(Vector2.up, angle.normalized);
        GameObject newProjectileGameObject = Instantiate(projectilePrefab, trans.position, Quaternion.Euler(0,0,eulerAngleToShootAt));
        Projectile newProjectile = newProjectileGameObject.GetComponent<Projectile>();
        newProjectile.Shoot(angle);
    }
}
