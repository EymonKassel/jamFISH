using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {
    [SerializeField]
    private Transform[] _patrolPoints;
    [SerializeField]
    private int _targetPoint;
    [SerializeField]
    private float _patrollingSpeed = 5f;
    [SerializeField]
    private float _acceleration = 2f;
    [SerializeField]
    private float _pauseDuration = 2f;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _followingSpeed = 5f;
    [SerializeField]
    private float _distanceBetween = 1000f;

    [SerializeField]
    private float _catchRange = 1f;

    private float _distanse;
    private bool _isTriggered = false;
    private bool _isBaited = false;
    private GameObject _currentBait;
    private bool _isWaiting = false;

    private float _currentSpeed = 0f;
    private Vector3 _lastPosition;

    private void Awake() {
        _player = GameObject.Find("Player");
    }

    private void Start() {
        _targetPoint = 0;
        _lastPosition = transform.position;
    }

    private void Update() {
        if ( _isBaited ) {
            FollowBait();
        } else {
            if ( _isTriggered ) {
                FollowPlayer();
                CheckCatchPlayer();
            } else {
                if ( !_isWaiting ) {
                    Patrol();
                }
            }
        }

        FlipBasedOnMovement();
    }

    private void FlipBasedOnMovement() {
        Vector3 movementDirection = transform.position - _lastPosition;

        if ( movementDirection.x > 0 ) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if ( movementDirection.x < 0 ) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        _lastPosition = transform.position;
    }

    private void FollowBait() {
        _distanse = Vector2.Distance(transform.position, _currentBait.transform.position);
        Vector2 direction = ( _currentBait.transform.position - transform.position ).normalized;

        if ( _distanse < _distanceBetween ) {
            MoveWithAcceleration(direction, _followingSpeed);
        }

        if ( _distanse <= 0 ) {
            Destroy(_currentBait);
        }
    }

    private void FollowPlayer() {
        _distanse = Vector2.Distance(transform.position, _player.transform.position);
        Vector2 direction = ( _player.transform.position - transform.position ).normalized;

        if ( _distanse < _distanceBetween ) {
            MoveWithAcceleration(direction, _followingSpeed);
        }
    }

    private void CheckCatchPlayer() {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

        if ( distanceToPlayer <= _catchRange ) {
            RestartScene(); 
        }
    }

    private void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Patrol() {
        if ( Vector3.Distance(transform.position, _patrolPoints[_targetPoint].position) <= 0.1f ) {
            StartCoroutine(WaitAtPoint());
        } else {
            Vector2 direction = ( _patrolPoints[_targetPoint].position - transform.position ).normalized;
            MoveWithAcceleration(direction, _patrollingSpeed);
        }
    }

    private IEnumerator WaitAtPoint() {
        _isWaiting = true;
        _currentSpeed = 0f;
        yield return new WaitForSeconds(_pauseDuration);
        IncreaseTargetPoint();
        _isWaiting = false;
    }

    private void MoveWithAcceleration(Vector2 direction, float maxSpeed) {
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, maxSpeed, _acceleration * Time.deltaTime);
        transform.position += (Vector3)( direction * _currentSpeed * Time.deltaTime );
    }

    private void IncreaseTargetPoint() {
        _targetPoint++;
        if ( _targetPoint >= _patrolPoints.Length ) {
            _targetPoint = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
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
            _currentBait = null;
        }
    }
}
