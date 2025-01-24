using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public bool isDamageable(ColorsEnum color) => true; // TODO: make this compare input color and player color

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player took damage. Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");

        gameObject.SetActive(false);
    }
}
