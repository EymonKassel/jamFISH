using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private float _holdTime = 1f;
    private float _holdTimer = 0f;

    [SerializeField]
    private GameObject _uiSettingsPanel;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        HandleDebugger();
        

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
    private void HandleDebugger() {
        HandleRestartCurrentScene();
        HandleRestartGame();
        HandleLoadNextScene();
        HandleLoadPreviousScene();
    }
    private void HandleRestartCurrentScene() {
        if ( Input.GetKey(KeyCode.K) ) {
            _holdTimer += Time.deltaTime;

            if ( _holdTimer >= _holdTime ) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                _holdTimer = 0f;
            }
        } else if ( Input.GetKey(KeyCode.K) ) {
            _holdTimer = 0f;
        }
    }
    private void HandleRestartGame() {
        if ( Input.GetKey(KeyCode.I) ) {
            _holdTimer += Time.deltaTime;

            if ( _holdTimer >= _holdTime ) {
                SceneManager.LoadScene(0);
                _holdTimer = 0f;
            }
        } else if ( Input.GetKey(KeyCode.I) ) {
            _holdTimer = 0f;
        }
    }
    private void HandleLoadNextScene() {
        if ( Input.GetKey(KeyCode.L) ) {
            _holdTimer += Time.deltaTime;

            if ( _holdTimer >= _holdTime ) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                _holdTimer = 0f;
            }
        } else if ( Input.GetKey(KeyCode.L) ) {
            _holdTimer = 0f;
        }
    }
    private void HandleLoadPreviousScene() {
        if ( Input.GetKey(KeyCode.J) ) {
            _holdTimer += Time.deltaTime;

            if ( _holdTimer >= _holdTime ) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                _holdTimer = 0f;
            }
        } else if ( Input.GetKey(KeyCode.J) ) {
            _holdTimer = 0f;
        }
    }
}