using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ignore gravity
    }

    void Update()
    {
        // Capture player input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY);

        // Set the Z rotation based on movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg -90;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
