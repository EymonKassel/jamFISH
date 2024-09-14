using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Audio Sources")]
    [SerializeField]
    public AudioSource MusicSource;
    [SerializeField]
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip BackgroundMusic;
    public AudioClip TakeItem;
    public AudioClip DropItem;
    public AudioClip SpawItem;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        MusicSource.clip = BackgroundMusic;
        MusicSource.Play();
    }
    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }

}
