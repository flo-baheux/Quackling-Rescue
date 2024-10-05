using System;
using System.Collections;
using UnityEngine;

public class MainAudioController : MonoBehaviour
{
  [NonSerialized] public AudioSource BGMSource;

  public void Awake()
  {
    BGMSource = GetComponent<AudioSource>();
  }

  public void PlayBGM(AudioClip audioClip)
  {
    if (audioClip == BGMSource.clip)
      return;
    StartCoroutine(TransitionTo(BGMSource, audioClip));
  }

  IEnumerator TransitionTo(AudioSource audioSource, AudioClip clip, int toVolume = 1, int fadeOutDuration = 1, int fadeInDuration = 1)
  {
    if (audioSource.isPlaying)
      StartCoroutine(FadeOutFadeIn(audioSource, clip, toVolume, fadeOutDuration, fadeInDuration));
    else
    {
      audioSource.clip = clip;
      audioSource.volume = 0;
      audioSource.Play();
      StartCoroutine(FadeIn(BGMSource));
    }
    return null;
  }

  private IEnumerator FadeOutFadeIn(AudioSource audioSource, AudioClip clip, int toVolume = 1, int fadeOutDuration = 1, int fadeInDuration = 1)
  {
    yield return StartCoroutine(FadeOut(audioSource, fadeOutDuration));
    audioSource.clip = clip;
    audioSource.Play();
    yield return StartCoroutine(FadeIn(audioSource, toVolume, fadeInDuration));
  }

  private IEnumerator FadeOut(AudioSource audioSource, int duration = 1)
  {
    float timeElapsed = 0;

    while (audioSource.volume > 0)
    {
      audioSource.volume = Mathf.Lerp(1, 0, timeElapsed / duration);
      timeElapsed += Time.deltaTime;
      yield return null;
    }
  }

  private IEnumerator FadeIn(AudioSource audioSource, int toVolume = 1, int duration = 1)
  {
    float timeElapsed = 0;

    int targetVolume = Math.Min(toVolume, 1);
    while (audioSource.volume < targetVolume)
    {
      audioSource.volume = Mathf.Lerp(0, 1, timeElapsed / duration);
      timeElapsed += Time.deltaTime;
      yield return null;
    }
  }
}
