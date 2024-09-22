using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BouncingProjectile : Projectile
{
    private Rigidbody rigidBody;
    private Transform shootPoint;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        rigidBody.velocity = shootPoint.forward * projectileSpeed;
    }

    public void SetShootPoint(Transform transform)
    {
        shootPoint = transform;
    }
}