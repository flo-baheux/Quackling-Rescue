using UnityEngine;
using UnityEngine.Audio;

public class PlayerPrefsApplier : MonoBehaviour
{
  [SerializeField] private AudioMixer mixer;

  void Start()
  {
    if (PlayerPrefs.HasKey("BGMVolume"))
      mixer.SetFloat("BGMVolume", Mathf.Log10(PlayerPrefs.GetFloat("BGMVolume")) * 20);

    if (PlayerPrefs.HasKey("SFXVolume"))
      mixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
  }
}
