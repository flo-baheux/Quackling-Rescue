using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerAudioComponent : MonoBehaviour
{
  public List<AudioClip> honkSounds;
  // public List<AudioClip> dashSounds;
  private AudioSource SFXAudioSource;
  private Player player;

  void Awake()
  {
    SFXAudioSource = GetComponent<AudioSource>();
    player = GetComponent<Player>();

  }

  void Start()
  {
    player.OnHonk += HandleHonk;
  }

  public void HandleHonk(float cooldown) => SFXAudioSource.PlayOneShot(honkSounds.OrderBy(n => Guid.NewGuid()).ToArray()[0]);

  // public void PlayDashSound() => SFXAudioSource.PlayOneShot(dashSounds.OrderBy(n => Guid.NewGuid()).ToArray()[0]);
}


