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
        Debug.Log("Shot element " + elementType + " in direction " + angle);
        float eulerAngle = Vector2.SignedAngle(Vector2.up, angle.normalized);
        GameObject projectileGO = Instantiate(projectilePrefab, trans.position, Quaternion.Euler(0,0,eulerAngle));
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.Shoot(angle);
    }
}
