using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
  [SerializeField] private Button playButton;
  [SerializeField] private Button settingsButton;
  [SerializeField] private Button CreditsButton;
  [SerializeField] private Button QuitButton;
  [SerializeField] private Button settingsBackToMenuButton;
  [SerializeField] private Button creditsBackToMenuButton;
  [SerializeField] private Slider GlobalVolumeSlider;
  [SerializeField] private Slider BGMVolumeSlider;
  [SerializeField] private Slider SFXVolumeSlider;

  [SerializeField] private GameObject mainMenuPanel;
  [SerializeField] private GameObject settingsPanel;
  [SerializeField] private GameObject creditsPanel;

  [SerializeField] private AudioMixer audioMixer;
  [SerializeField] private AudioClip UIClickSound;
  [SerializeField] private AudioClip ErrorSound;

  [SerializeField] private AudioSource mainAudioSource;

  private GameManager gameManager;

  void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
    mainAudioSource = GetComponent<AudioSource>();

    playButton.Select();
  }

  public void Play()
  {
    OnUIActionPlayClicSound();
    gameManager.GoToGameScene("MainGameScene");
  }

  public void OpenSettings()
  {
    OnUIActionPlayClicSound();
    mainMenuPanel.SetActive(false);

    settingsPanel.SetActive(true);
    GlobalVolumeSlider.Select();

    float globalVolume, bgmVolume, sfxVolume;
    audioMixer.GetFloat("GlobalVolume", out globalVolume);
    audioMixer.GetFloat("BGMVolume", out bgmVolume);
    audioMixer.GetFloat("SFXVolume", out sfxVolume);
    GlobalVolumeSlider.value = Mathf.Pow(10f, globalVolume / 20);
    BGMVolumeSlider.value = Mathf.Pow(10f, bgmVolume / 20);
    SFXVolumeSlider.value = Mathf.Pow(10f, sfxVolume / 20);
  }

  public void OpenCredits()
  {
    OnUIActionPlayClicSound();
    mainMenuPanel.SetActive(false);
    creditsBackToMenuButton.Select();
    creditsPanel.SetActive(true);
  }

  public void BackToMenu()
  {
    OnUIActionPlayClicSound();
    creditsPanel.SetActive(false);
    settingsPanel.SetActive(false);

    mainMenuPanel.SetActive(true);
    playButton.Select();
  }

  public void Quit()
  {
    OnUIActionPlayClicSound();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    Application.Quit();
  }

  public void OnGlobalVolumeChanged()
  {
    PlayerPrefs.SetFloat("GlobalVolume", GlobalVolumeSlider.value);
    audioMixer.SetFloat("GlobalVolume", Mathf.Log10(GlobalVolumeSlider.value) * 20);
  }

  public void OnBGMVolumeChanged()
  {
    PlayerPrefs.SetFloat("BGMVolume", BGMVolumeSlider.value);
    audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMVolumeSlider.value) * 20);
  }

  public void OnSFXVolumeChanged()
  {
    PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
    audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolumeSlider.value) * 20);
  }


  private void OnUIActionPlayClicSound()
  {
    mainAudioSource.PlayOneShot(UIClickSound);
  }
}

