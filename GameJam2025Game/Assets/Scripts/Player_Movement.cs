using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
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
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
