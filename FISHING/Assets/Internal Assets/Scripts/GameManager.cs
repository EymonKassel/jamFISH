using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public float holdTime = 2f; // Time in seconds to hold the middle mouse button
    private float holdTimer = 0f; // Timer to track how long the button is held

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if ( Input.GetMouseButton(2) ) { // Middle mouse button is held down
            holdTimer += Time.deltaTime; // Increment the timer

            // If the button is held for the required time, reload the scene
            if ( holdTimer >= holdTime ) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        } else if ( Input.GetMouseButtonUp(2) ) {
            // Reset the timer when the button is released
            holdTimer = 0f;
        }
    }
}