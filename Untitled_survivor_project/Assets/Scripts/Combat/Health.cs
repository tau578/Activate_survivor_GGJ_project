using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attached to any game object that has health, e.g., player, enemies
public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 12f;


    public event Action<bool> OnHealthChanged; // if true, health increased, if false, health decreased
    
    public event Action OnDie;

    public bool isInvulnerable;

    public bool isDead;

    //public bool IsDead => health == 0; this is one way to creaete a bool meathod with only one line
    public float health;
    void Start()
    {
        health = maxHealth;
    }
    public void SetVulnerablity(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
    public void DealDamage(float damage)
    {

        if (isDead || isInvulnerable) { return; } // don't take damage when you are invulnerable

        health = Mathf.Max(health -= damage, 0);
        OnHealthChanged?.Invoke(false); // health decreased 
        if (health <= 0)
        {
            isDead = true;
            OnDie?.Invoke();

        }

    }
    public void Heal(int healAmount)
    {
        if (isDead || health == maxHealth) { return; } // return if health is full
        health = Mathf.Min(health += healAmount, maxHealth);
        OnHealthChanged?.Invoke(true); // health increased
    }

}
