using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // Speed at which the player moves
    private Rigidbody2D _rb;
    private Vector2 velocity = Vector2.zero; // To store the player's velocity

    private bool facingRight = true; // Tracks the current facing direction of the player

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if ( Input.GetMouseButton(0) ) {
            // Move player towards mouse
            MoveTowardsMouse();
        } else if ( velocity.magnitude > 0.1f ) {
            // Slide effect when not holding the mouse button
            velocity = Vector2.Lerp(velocity, Vector2.zero, 0.5f * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + velocity * Time.fixedDeltaTime);
        }

        // Rotate the sprite to follow the mouse and flip based on direction
        RotateAndFlip();
    }

    // Move the player towards the mouse position
    private void MoveTowardsMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ( mousePosition - transform.position ).normalized;

        velocity = Vector2.Lerp(velocity, direction * moveSpeed, 0.5f * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + velocity * Time.fixedDeltaTime);
    }

    // Rotate the sprite to face the mouse and flip based on mouse position
    private void RotateAndFlip() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;

        // Calculate the angle to rotate the sprite
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the sprite, but prevent flipping upside down
        if ( facingRight ) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        } else {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, -angle));
        }

        // Flip the sprite horizontally based on the mouse position (left or right)
        if ( mousePosition.x > transform.position.x && !facingRight ) {
            Flip(true);
        } else if ( mousePosition.x < transform.position.x && facingRight ) {
            Flip(false);
        }
    }

    // Flip the player's local scale on the X axis
    private void Flip(bool isFacingRight) {
        facingRight = isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invert the X axis to flip the sprite
        scale.y *= -1;
        transform.localScale = scale;
    }
}
