using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float _holdTime = 2f; 
    private float _holdTimer = 0f; 

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        RestartGame();
    }
    private void RestartGame() {
        if ( Input.GetMouseButton(2) ) {
            _holdTimer += Time.deltaTime;

            if ( _holdTimer >= _holdTime ) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                _holdTimer = 0f;
            }
        } else if ( Input.GetMouseButtonUp(2) ) {
            _holdTimer = 0f;
        }
    }
}