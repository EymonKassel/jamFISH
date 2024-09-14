using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float _holdTime = 2f; 
    private float _holdTimer = 0f;

    [SerializeField]
    private GameObject _uiSettingsPanel;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        HandleRestartGame();

        if ( Input.GetKeyDown(KeyCode.Escape) ) {
            OpenSettings();
        }
    }
    
    public void OpenSettings() {
        if ( _uiSettingsPanel.activeInHierarchy ) {
            _uiSettingsPanel.SetActive(false);
            Time.timeScale = 1f;
        } else {
            _uiSettingsPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void HandleRestartGame() {
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