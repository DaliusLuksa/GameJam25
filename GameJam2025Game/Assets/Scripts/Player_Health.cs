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
        if (!GameManager.Instance.IsGameInProgress() || !_isAlive || health <= 0) { return; }

        GameManager.Instance.DamageIndicator.FlashVignette();

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("IsDead", true);

        // Yeah... Don't let Motiejus cook after 2 AM
        if (soundEffect != null)
        {
            soundEffect.playSounds("death");
        }

        _isAlive = false;

        GameManager.Instance.DisableRoomOnPlayerDeath(playerColor);
    }

    public void Revive()
    {
        animator.SetBool("IsDead", false);
        _isAlive = true;

        // Reset health back to default
        health = 10;
    }
}
