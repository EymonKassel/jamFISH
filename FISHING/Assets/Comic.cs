using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Comic : MonoBehaviour {
    [SerializeField] private GameObject[] views; // Array of views
    [SerializeField] private float fadeDuration = 0.5f; // Duration for the fade-in effect
    private int currentViewIndex = 0;  // Index to track the current view
    private int currentFrameIndex = 0; // Index to track the current frame within the view

    private void Start() {
        InitializeComic();
    }

    private void Update() {
        if ( Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) ) {
            ShowNextFrame();
        }
    }

    private void InitializeComic() {
        foreach ( GameObject view in views ) {
            view.SetActive(false); // Hide all views initially
        }

        // Show the first view and fade in the first frame
        views[currentViewIndex].SetActive(true);
        StartCoroutine(FadeInFrame(views[currentViewIndex].transform.GetChild(currentFrameIndex).gameObject));
    }

    private void ShowNextFrame() {
        currentFrameIndex++;

        if ( currentFrameIndex >= views[currentViewIndex].transform.childCount ) {
            currentFrameIndex = 0;
            views[currentViewIndex].SetActive(false);

            currentViewIndex++;
            if ( currentViewIndex < views.Length ) {
                views[currentViewIndex].SetActive(true);
                HideAllFramesInPreviousView();
            } else {
                
                    Debug.Log("End of comic");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                
                return;
            }
        }

        // Fade in the next frame
        StartCoroutine(FadeInFrame(views[currentViewIndex].transform.GetChild(currentFrameIndex).gameObject));
    }

    private void HideAllFramesInPreviousView() {
        int previousViewIndex = currentViewIndex - 1;
        if ( previousViewIndex >= 0 ) {
            foreach ( Transform frame in views[previousViewIndex].transform ) {
                frame.gameObject.SetActive(false);
            }
        }
    }

    // Fade-in effect for frames
    private IEnumerator FadeInFrame(GameObject frame) {
        // Ensure the frame has a CanvasGroup component for controlling opacity
        CanvasGroup canvasGroup = frame.GetComponent<CanvasGroup>();
        if ( canvasGroup == null ) {
            canvasGroup = frame.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;  // Start with invisible frame
        frame.SetActive(true);   // Show the frame

        // Gradually increase alpha value to make it visible
        float elapsedTime = 0f;
        while ( elapsedTime < fadeDuration ) {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        canvasGroup.alpha = 1f;  // Ensure it's fully visible at the end
    }
}
