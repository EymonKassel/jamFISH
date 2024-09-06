using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _sprintSpeedMultiplier = 2f;

    private Vector2 _movement;

    [SerializeField]
    private bool _isSprinting = false;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        // I used "GetAxis()" instead of "GetAxisRaw()" cause I wanted to get decimal number
        // It could be usefull for making smoothness movement
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        Move(_sprintSpeedMultiplier);
    }

    private void Move(float sprintSpeedMultiplier = 2f) {
        if ( Input.GetKey(KeyCode.LeftShift) ) {
            _rb.velocity = new(_movementSpeed * _movement.x * sprintSpeedMultiplier * Time.deltaTime,
                _movementSpeed * _movement.y * sprintSpeedMultiplier * Time.deltaTime);
        } else {
            _rb.velocity = new(_movementSpeed * _movement.x * Time.deltaTime,
                _movementSpeed * _movement.y * Time.deltaTime);
        }
    }
}
