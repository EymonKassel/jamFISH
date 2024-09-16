using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RadioactivePoisoning : MonoBehaviour {
    public Image Image; 
    public float TotalTime = 120f; 
    public float MaxAlpha = 0.2f;

    [SerializeField]
    public float TimeRemaining;
    private Color _imageColor;

    private Animator _player;

    public TextMeshPro Text;
    private void Awake() {
        _player = GameObject.Find("Player_Fish").GetComponent<Animator>();
    }

    private void Start() {
        TimeRemaining = TotalTime; 
        _imageColor = Image.color; 
        _imageColor.a = 0f; 
        Image.color = _imageColor; 
    }

    private void Update() {
        if ( TimeRemaining > 0 ) {
            TimeRemaining -= Time.deltaTime;

            float alpha = Mathf.Clamp01(1 - ( TimeRemaining / TotalTime )) * MaxAlpha;
            _imageColor.a = alpha;

            Image.color = _imageColor;

            Text.text = TimeRemaining.ToString("0");
        }

        if ( TimeRemaining <= 0 ) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
        }
        if ( TimeRemaining < 2 ) {
            PlayerMovement playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            playerMovement.enabled = false;
            _player.SetBool("IsDead", true);
        }
    }
}
