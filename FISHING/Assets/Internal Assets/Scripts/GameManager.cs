using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if ( Input.GetMouseButtonDown(2) ) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
}
