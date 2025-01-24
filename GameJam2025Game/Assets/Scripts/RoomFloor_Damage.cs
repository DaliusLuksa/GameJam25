using UnityEngine;

public class DamagingFloor : MonoBehaviour
{
    [SerializeField]  private ColorsEnum roomColor = ColorsEnum.UNKNOWN;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float damageInterval = 1f;

    private float nextDamageTime = 0f;
    private bool isPlayerInTrigger = false;
    private Player_Health player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            player = other.GetComponent<Player_Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            player = null;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && player != null && Time.time >= nextDamageTime)
        {
            if (player.isDamageable(roomColor))
            {
                player.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    private void OnValidate()
    {
        if (roomColor == ColorsEnum.UNKNOWN)
        {
            Debug.LogError($"{nameof(roomColor)} must be assigned in the Inspector!", this);
        }
    }
}
