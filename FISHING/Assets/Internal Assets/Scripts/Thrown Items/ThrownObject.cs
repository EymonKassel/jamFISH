using System.Collections;
using UnityEngine;

public class ThrownObject : MonoBehaviour {
    private Rigidbody2D _rb;
    public float stopTime = 2f; // Time after which the object fully stops
    public float slowDownRate = 0.9975f; // Rate at which the object slows down

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; // Disable gravity once the object is thrown

        // Start the coroutine to gradually slow down and stop the object
        StartCoroutine(SlowDownAndStop());
    }

    private IEnumerator SlowDownAndStop() {
        float elapsedTime = 0f;

        // Gradually slow down the object over the duration of stopTime
        while ( elapsedTime < stopTime ) {
            _rb.velocity *= slowDownRate; // Gradually reduce the velocity
            elapsedTime += Time.deltaTime;

            yield return null; // Wait until the next frame
        }

        // After the time has elapsed, stop the object's movement
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f; // Stop rotation if any

        // Optionally, you can re-enable gravity after the object has stopped
        //_rb.gravityScale = 1f;
    }
}
