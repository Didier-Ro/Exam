using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LinearProjectile : Projectile
{
    private Rigidbody rigidBody;
    private Transform shootPoint;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }
    public override void Move()
    {
        rigidBody.velocity = shootPoint.forward * projectileSpeed;  
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        DestroyProjectile();
    }

    public void SetShootPoint(Transform transformy)
    {
        shootPoint = transform;
    }
}
