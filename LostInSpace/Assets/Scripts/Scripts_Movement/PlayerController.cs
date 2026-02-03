using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // How strong the thrust is when you click/hold the mouse
    public float thrustForce = 1f;

    // Reference to the Rigidbody2D on the Player
    private Rigidbody2D rb;

    void Start()
    {
        // Grab the Rigidbody2D component attached to this Player GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If the left mouse button is being pressed (held down)
        if (Mouse.current.leftButton.isPressed)
        {
            // Get mouse position in SCREEN coordinates (pixels)
            Vector3 mouseScreenPos = Mouse.current.position.value;

            // Convert mouse position from SCREEN space to WORLD space
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            // Direction from player -> mouse (as a Vector2 for 2D)
            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            // Rotate the player so its "up" direction points toward the mouse
            transform.up = direction;

            // Apply force in that direction (thrust)
            rb.AddForce(direction * thrustForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the player if it collides with anything
        Destroy(gameObject);
    }
}
