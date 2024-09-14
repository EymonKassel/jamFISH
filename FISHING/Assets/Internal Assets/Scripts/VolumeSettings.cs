using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour {
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField] 
    private Slider _musicSlider;
    [SerializeField]
    private Slider _SFXSlider;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        } else {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume() {
        float volume = _musicSlider.value;
        _mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume() {
        float volume = _SFXSlider.value;
        _mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    private void LoadVolume() {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }
}
