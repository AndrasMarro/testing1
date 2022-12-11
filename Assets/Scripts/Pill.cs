using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int heal = -1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(heal);
            Destroy(gameObject);
        }
    }
}
