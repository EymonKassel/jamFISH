using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HoldingObjects : MonoBehaviour {
    [SerializeField]
    private Transform _mouthAnchor;
    [SerializeField]
    private Transform _backAnchor;
    [SerializeField]
    private GameObject _currentObject;
    [SerializeField]
    private GameObject _stashedObject;

    private GameObject _tempObject;

    private Vector2 _position;

    private Rigidbody2D _rb;

    private GameObject _pickableObject;

    private PickableObject _pickable;

    private void Update() {
        if ( _currentObject != null ) {
            _currentObject.transform.position = _mouthAnchor.position;
        }
        if ( _stashedObject != null ) {
            _stashedObject.transform.position = _backAnchor.position;
        }

        if ( Input.GetMouseButtonDown(1) ) {
            if ( _currentObject != null ) {
                Drop();
            } else {
                if ( _pickableObject != null ) {
                    Take();
                }
            }
        }

        if ( Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0 ) {
            Swap(); 
            Debug.Log("scroll");    
        }
    }
    
    private void Swap() {
        _tempObject = _stashedObject;
        _stashedObject = _currentObject;
        _currentObject = _tempObject;
        _tempObject = null;
    }

    private void Take() {
        _currentObject = _pickableObject;
        _currentObject.transform.position = _mouthAnchor.position;
    }

    private void Drop() {
        Debug.Log("push");
        _currentObject.AddComponent<Rigidbody2D>();
        _rb = _currentObject.GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0.25f;
        _currentObject.transform.parent = null;

        _pickable = _currentObject.GetComponent<PickableObject>();
        // change later
        _rb.velocity = Vector2.right * 20;
        _currentObject = null;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10 ) {
            _pickableObject = collision.gameObject; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 10 ) {
            _pickableObject = null;
        }
    }
}
