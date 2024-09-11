using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObjects : MonoBehaviour {
    [Header("Anchor Points")]
    [SerializeField]
    private Transform _mouthAnchor;
    [SerializeField]
    private Transform _backAnchor;

    [Header("Object References")]
    [SerializeField]
    private GameObject _currentObject;
    [SerializeField]
    private GameObject _stashedObject;

    private GameObject _tempObject; // Temporary storage for object swapping
    private GameObject _pickableObject; // Object in range that can be picked up

    private Rigidbody2D _rb;
    private PickableObject _pickable; // Reference to pickable object component

    [Header("Throw Settings")]
    [SerializeField] private float throwForce = 20f; // Force applied when throwing

    private void Update() {
        UpdateObjectPositions();
        HandleMouseInput();
        HandleScrollInput();
    }

    // Update the positions of the held and stashed objects
    private void UpdateObjectPositions() {
        if ( _currentObject != null ) {
            _currentObject.transform.position = _mouthAnchor.position;
        }
        if ( _stashedObject != null ) {
            _stashedObject.transform.position = _backAnchor.position;
        }
    }

    // Handle input for picking up, dropping, or interacting with objects
    private void HandleMouseInput() {
        if ( Input.GetMouseButtonDown(1) ) { // Right mouse button
            if ( _currentObject != null ) {
                Throw(); // Throw the object if currently holding one
            } else if ( _pickableObject != null ) {
                Take(); // Pick up the object if in range
            }
        }
    }

    // Handle mouse scroll input for swapping objects
    private void HandleScrollInput() {
        if ( Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f ) {
            Swap();
            Debug.Log("Swapped objects");
        }
    }

    // Swap the current held object with the stashed object
    private void Swap() {
        _tempObject = _stashedObject;
        _stashedObject = _currentObject;
        _currentObject = _tempObject;
        _tempObject = null;
    }

    // Pick up the object
    private void Take() {
        _currentObject = _pickableObject;
        _currentObject.transform.position = _mouthAnchor.position;
        _currentObject.transform.parent = transform; // Make the player the parent of the object
    }

    // Throw the object, applying force in the direction towards the mouse
    private void Throw() {
        if ( _currentObject == null ) return;

        _currentObject.transform.parent = null; // Detach the object from the player
        _currentObject.AddComponent<Rigidbody2D>(); // Add Rigidbody2D for physics
        _rb = _currentObject.GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0.25f;

        // Calculate direction to the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z-axis is zero for 2D
        Vector2 throwDirection = ( mousePosition - transform.position ).normalized;

        // Apply force to throw the object in the direction of the mouse
        _rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        _currentObject = null;

        Debug.Log("Object thrown");
    }

    // Detect when a pickable object enters the player's range
    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.gameObject.layer == 10 ) { // Assuming pickable objects are on layer 10
            _pickableObject = collision.gameObject;
        }
    }

    // Detect when a pickable object exits the player's range
    private void OnTriggerExit2D(Collider2D collision) {
        if ( collision.gameObject.layer == 10 ) {
            _pickableObject = null;
        }
    }
}
