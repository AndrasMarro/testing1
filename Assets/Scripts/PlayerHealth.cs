using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 4;
    public GameObject deathEffect;

    public SpriteRenderer playerSr;
    public PlayerMovement playerMovement;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            playerSr.enabled = false;
            playerMovement.enabled = false;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

}
