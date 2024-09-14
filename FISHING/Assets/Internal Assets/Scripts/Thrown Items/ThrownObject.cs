using System.Collections;
using UnityEngine;

public class ThrownObject : MonoBehaviour {
    private Rigidbody2D _rb;
    public float stopTime = 2f; 
    public float slowDownRate = 0.9975f; // Change it carefully!!!

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; 

        StartCoroutine(SlowDownAndStop());
    }

    private IEnumerator SlowDownAndStop() {
        float elapsedTime = 0f;

        while ( elapsedTime < stopTime ) {
            _rb.velocity *= slowDownRate; 
            elapsedTime += Time.deltaTime;

            yield return null; 
        }

        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
    }
}
