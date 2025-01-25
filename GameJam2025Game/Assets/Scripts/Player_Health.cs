using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health = 10;
    [SerializeField] private ColorsEnum playerColor = ColorsEnum.UNKNOWN;
    private bool _isAlive = true;

    public bool IsAlive() => _isAlive;
    public bool IsDamageable(ColorsEnum roomColor) => roomColor != playerColor;

    public void TakeDamage(float amount)
    {
        if (health <= 0) { return; }

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} Player has died!");

        _isAlive = false;
        gameObject.SetActive(false);
    }
}
