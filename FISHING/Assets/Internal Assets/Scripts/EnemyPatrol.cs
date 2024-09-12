using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    [SerializeField]
    private Transform[] _patrolPoints;
    [SerializeField]
    private int _targetPoint;
    [SerializeField]
    private float _patrollingSpeed = 5f;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _followingSpeed = 5f;
    [SerializeField]
    private float _distanceBetween = 1000f; // It's rudiment, remove later if there is time

    private float _distanse;

    private bool _isTriggered = false;
    private bool _isBaited = false;
    private GameObject _currentBait;

    private void Awake() {
        _player = GameObject.Find("Player");
    }

    private void Start() {
        _targetPoint = 0;
    }

    private void Update() {
        if ( _isBaited ) {
            FollowBait();
        } else {
            if ( _isTriggered ) {
                FollowPlayer();

            } else {
                Patrol();
            }
        }
    }

    // Somehow later combine FollowBait() and FollowPlayer()
    private void FollowBait() {
        _distanse = Vector2.Distance(transform.position, _currentBait.transform.position);
        Vector2 direction = _currentBait.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ( _distanse < _distanceBetween ) {
            transform.position = Vector2.MoveTowards(transform.position, _currentBait.transform.position, _followingSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        // Change later ("0" and basic logic)
        if ( _distanse <= 0 ) {
            Destroy(_currentBait);
        }

    }
    private void FollowPlayer() {
        _distanse = Vector2.Distance(transform.position, _player.transform.position);
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_distanse < _distanceBetween) {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _followingSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    private void Patrol() {
        if ( transform.position == _patrolPoints[_targetPoint].position ) {
            IncreaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[_targetPoint].position, _patrollingSpeed * Time.deltaTime);
    }

    private void IncreaseTargetInt() {
        _targetPoint++;
        if ( _targetPoint >= _patrolPoints.Length ) {
            _targetPoint = 0;
        }
    }

    // Probably needs to change to OntriggerStay2D()
    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.CompareTag("Player") ) {
            _isTriggered = true;
        }
        if ( collision.CompareTag("Bait") ) {
            _isBaited = true;
            _currentBait = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if ( collision.CompareTag("Player") ) {
            _isTriggered = false;
        }
        if ( collision.CompareTag("Bait") ) {
            _isBaited = false;
            if ( _currentBait != null ) {
                _currentBait = null;
            }
        }
    }
}
