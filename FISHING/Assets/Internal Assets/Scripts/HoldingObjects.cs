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
    
    private GameObject _tempObject; 
    private GameObject _pickableObject; 

    [Header("Component References")]
    private Rigidbody2D _rb;
    private AudioManager _audioManager;

    [Header("Throw Settings")]
    [SerializeField] private float throwForce = 20f; 

    private void Awake() {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void Update() {
        UpdateObjectPositions();
        HandleMouseInput();
        HandleScrollInput();
    }
   
    private void UpdateObjectPositions() {
        if ( _currentObject != null ) {
            _currentObject.transform.position = _mouthAnchor.position;
        }
        if ( _stashedObject != null ) {
            _stashedObject.transform.position = _backAnchor.position;
        }
    }

    private void HandleMouseInput() {
        if ( Input.GetMouseButtonDown(1) ) { 
            if ( _currentObject != null ) {
                Throw(); 
            } else if ( _pickableObject != null ) {
                Take(); 
            }
        }
    }

    private void HandleScrollInput() {
        if ( Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f ) {
            Swap();
            Debug.Log("Swapped objects");
        }
    }

    private void Swap() {
        _tempObject = _stashedObject;
        _stashedObject = _currentObject;
        _currentObject = _tempObject;
        _tempObject = null;
        _audioManager.PlaySFX(_audioManager.SpawItem);
    }

    private void Take() {
        _currentObject = _pickableObject;
        _currentObject.transform.position = _mouthAnchor.position;
        _currentObject.transform.parent = transform; 
        _audioManager.PlaySFX(_audioManager.TakeItem);
    }

    private void Throw() {
        if ( _currentObject == null ) return;

        AddComponentsToThrownItem(_currentObject.tag);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 
        Vector2 throwDirection = ( mousePosition - transform.position ).normalized;

        _rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        _currentObject = null;
        _audioManager.PlaySFX(_audioManager.DropItem);
    }

    private void AddComponentsToThrownItem(string itemTag) {
        _currentObject.transform.parent = null;
        _currentObject.AddComponent<Rigidbody2D>();
        _rb = _currentObject.GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0.25f;
        _rb.freezeRotation = true;

        switch (itemTag) {
            case "TinyStone":
                _currentObject.AddComponent<ThrownObject>();
                _currentObject.AddComponent<TinyStone>();
                break;
            case "HugeStone":
                _currentObject.AddComponent<ThrownObject>();
                _currentObject.AddComponent<HugeStone>();
                break;
            case "Bait":
                _currentObject.AddComponent<ThrownObject>();
                _currentObject.AddComponent<Bait>();
                break;
            case "Diode":
                _currentObject.AddComponent<ThrownObject>();
                _currentObject.AddComponent<Diode>();
                break;
            case "QuestItem":
                _currentObject.AddComponent<ThrownObject>();
                _currentObject.AddComponent<QuestItem>();
                break;
            default:
                Debug.Log("Thrown object is unknown");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.gameObject.layer == 10 ) { 
            _pickableObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if ( collision.gameObject.layer == 10 ) {
            _pickableObject = null;
        }
    }
}
