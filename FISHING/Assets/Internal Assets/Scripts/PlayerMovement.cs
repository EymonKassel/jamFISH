using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _roughness = 1f;
    public float moveSpeed = 5f; 
    private Rigidbody2D _rb;
    private Vector2 velocity = Vector2.zero; 

    private bool facingRight = true;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if ( Input.GetMouseButton(0) ) {
            MoveTowardsMouse();
        } else if ( velocity.magnitude > 0.1f ) {
            velocity = Vector2.Lerp(velocity, Vector2.zero, _roughness * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + velocity * Time.fixedDeltaTime);
        }

        RotateAndFlip();
    }

    private void MoveTowardsMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ( mousePosition - transform.position ).normalized;

        velocity = Vector2.Lerp(velocity, direction * moveSpeed, 0.5f * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + velocity * Time.fixedDeltaTime);
    }

    private void RotateAndFlip() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ( facingRight ) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        } else {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, -angle));
        }

        if ( mousePosition.x > transform.position.x && !facingRight ) {
            Flip(true);
        } else if ( mousePosition.x < transform.position.x && facingRight ) {
            Flip(false);
        }
    }

    private void Flip(bool isFacingRight) {
        facingRight = isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        scale.y *= -1;
        transform.localScale = scale;
    }
}
