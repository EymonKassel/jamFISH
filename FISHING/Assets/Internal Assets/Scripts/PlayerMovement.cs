using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector3 _mousePosition;
    public float MoveSpeed = 0.01f;
    private Rigidbody2D _rb;
    private Vector2 _position = new(0f, 0f);

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if ( Input.GetMouseButton(0) ) {
            Move();
        }
        if ( Input.GetMouseButtonUp(0) ) {
            // smoothness
        }
    }

    private void Move() {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        _position = Vector2.Lerp(transform.position, _mousePosition, MoveSpeed);
        _rb.MovePosition(_position);
    }
}
