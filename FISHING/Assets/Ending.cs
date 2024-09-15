using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {
    private void Update() {
        if ( Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) ) {
            SceneManager.LoadScene(0);
        }
    }
}
