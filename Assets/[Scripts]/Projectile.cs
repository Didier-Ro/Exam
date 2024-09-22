using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Configuration")]
    public float projectileSpeed;
    public float damage;
    public float lifeTime;

    protected float currentLifeTime;

    private void Start()
    {
        currentLifeTime = lifeTime;
    }

    public void Update()
    {
        currentLifeTime -= Time.deltaTime;

        if (currentLifeTime <= 0)
        {
            DestroyProjectile();
        }
    }

    public virtual void Move(){    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamage setDamage)) 
        {
            setDamage.OnImpact(damage);
        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
