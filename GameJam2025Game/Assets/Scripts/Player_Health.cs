using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health = 10;
    [SerializeField] private ColorsEnum playerColor = ColorsEnum.UNKNOWN;

    [SerializeField] private Animator animator;

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

        animator.SetBool("Walking", false);
        animator.SetBool("IsDead", true);

        // animator.SetBool("IsDead", false); for when alive
        _isAlive = false;
    }
}
