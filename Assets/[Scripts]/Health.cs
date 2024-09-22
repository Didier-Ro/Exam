using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHealth;
    private float health;

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs: EventArgs{
        public float currentHealt;
    }

    public event EventHandler OnHealthEmpty;

    private void Awake()
    {
        health = maxHealth;
    }

    public void OnImpact(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(){
                currentHealt = health
            });
            OnHealthEmpty?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs() { 
                currentHealt = health
            });
        }
    }
}