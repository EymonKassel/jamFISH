using UnityEngine;
using UnityEngine.UI;

public class RadioactivePoisoning : MonoBehaviour {
    public Image Image; 
    public float TotalTime = 120f; 
    public float MaxAlpha = 0.2f;

    [SerializeField]
    public float TimeRemaining;
    private Color _imageColor;

    void Start() {
        TimeRemaining = TotalTime; 
        _imageColor = Image.color; 
        _imageColor.a = 0f; 
        Image.color = _imageColor; 
    }

    void Update() {
        if ( TimeRemaining > 0 ) {
            TimeRemaining -= Time.deltaTime;

            float alpha = Mathf.Clamp01(1 - ( TimeRemaining / TotalTime )) * MaxAlpha;
            _imageColor.a = alpha;

            Image.color = _imageColor;
        }
    }
}
