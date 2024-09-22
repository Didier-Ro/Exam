using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour, IShoot
{
    private enum Projectile_Type
    {
        Linear,
        Bounce
    }

    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject linearProjectile;
    [SerializeField] private GameObject bouncingProjectile;
    [SerializeField] private Projectile_Type projectileType;
    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Shoot()
    {
        switch (projectileType) 
        { 
            case Projectile_Type.Linear:
                GameObject projectileL = Instantiate(linearProjectile, shootPoint.position, shootPoint.rotation); 
                LinearProjectile linear = projectileL.GetComponent<LinearProjectile>();
                linear.SetShootPoint(shootPoint);
                linear.Move();
                break;
            case Projectile_Type.Bounce:
                GameObject projectileB = Instantiate(bouncingProjectile, shootPoint.position, shootPoint.rotation);
                BouncingProjectile bouncing = projectileB.GetComponent<BouncingProjectile>();
                bouncing.SetShootPoint(shootPoint);
                bouncing.Move();
                break;
        }       
    }

    public void OnShoot()
    {
        Shoot();
    }
} 
