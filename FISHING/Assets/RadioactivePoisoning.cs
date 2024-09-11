using UnityEngine;
using UnityEngine.UI;

public class RadioactivePoisoning : MonoBehaviour {
    public Image Image; // Drag your UI Image here in the Inspector
    public float TotalTime = 120f; // The timer duration in seconds
    public float MaxAlpha = 0.2f; // The maximum alpha level (default 0.5)

    [SerializeField]
    private float _timeRemaining;
    private Color _imageColor;

    void Start() {
        _timeRemaining = TotalTime; // Initialize the timer
        _imageColor = Image.color; // Get the original color of the image
        _imageColor.a = 0f; // Set the image to fully transparent at the start
        Image.color = _imageColor; // Apply the initial transparency
    }

    void Update() {
        // Decrease the time remaining as the timer runs
        if ( _timeRemaining > 0 ) {
            _timeRemaining -= Time.deltaTime;

            // Calculate the alpha level based on the remaining time
            float alpha = Mathf.Clamp01(1 - ( _timeRemaining / TotalTime )) * MaxAlpha;
            _imageColor.a = alpha;

            // Apply the updated color (with alpha) to the image
            Image.color = _imageColor;
        }
    }
}
