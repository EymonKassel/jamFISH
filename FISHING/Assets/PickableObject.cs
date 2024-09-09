using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {
    private bool _canPickUp = false;
    private HoldingObjects _holdingObjects;

    private void Awake() {
        _holdingObjects = GameObject.Find("Player").GetComponent<HoldingObjects>();
    }
    private void Update() {
        if ( _canPickUp ) {
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.CompareTag("Player") ) {
            _canPickUp = true;
            Debug.Log(_canPickUp);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if ( collision.CompareTag("Player") ) {
            _canPickUp = false;
            Debug.Log(_canPickUp);
        }
    }
}
