using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health = 10;
    [SerializeField] private ColorsEnum playerColor = ColorsEnum.UNKNOWN;

    [SerializeField] private Animator animator;

    private Sound_Effect soundEffect;

    private bool _isAlive = true;

    public bool IsAlive() => _isAlive;
    public bool IsDamageable(ColorsEnum roomColor) => roomColor != playerColor;

   private void Start()
    {
        soundEffect = GetComponent<Sound_Effect>();
    }

    public void TakeDamage(float amount)
    {
        if (health <= 0) { return; }

        GameManager.Instance.DamageIndicator.FlashVignette();

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

        soundEffect.playSounds("death");



        // animator.SetBool("IsDead", false); for when alive
        _isAlive = false;
    }
}
