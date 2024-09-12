using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float holdTime = 2f; 
    private float holdTimer = 0f; 

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if ( Input.GetMouseButton(2) ) { 
            holdTimer += Time.deltaTime; 

            if ( holdTimer >= holdTime ) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                holdTimer = 0f;
            }
        } else if ( Input.GetMouseButtonUp(2) ) {
            holdTimer = 0f;
        }
    }
}