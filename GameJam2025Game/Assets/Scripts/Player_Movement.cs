using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private ControlScheme controlScheme = ControlScheme.WASD; // Assign control scheme in the Inspector

    private Rigidbody2D rb;
    private Vector2 movement;
    private Player player = null;

    // Enum to define control schemes for players
    public enum ControlScheme
    {
        WASD,
        Arrows,
        IJKL
    }

    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ignore gravity
    }

    void Update()
    {
        // Capture input based on the assigned control scheme
        switch (controlScheme)
        {
            case ControlScheme.WASD:
                movement = GetMovement(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
                break;

            case ControlScheme.Arrows:
                movement = GetMovement(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
                break;

            case ControlScheme.IJKL:
                movement = GetMovement(KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L);
                break;
        }

        // Set the Z rotation based on movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
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

    // Helper method to get movement based on key inputs
    private Vector2 GetMovement(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(up)) vertical = 1f;
        if (Input.GetKey(down)) vertical = -1f;
        if (Input.GetKey(left)) horizontal = -1f;
        if (Input.GetKey(right)) horizontal = 1f;

        return new Vector2(horizontal, vertical).normalized; // Normalize to keep consistent movement speed
    }
}
