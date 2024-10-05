using System;
using System.Linq;
using UnityEngine;

namespace Player
{

  public class PlayerAudioComponent : MonoBehaviour
  {
    public AudioClip honkSound;
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
    }

    void Update()
    {
      if (player.input.HonkPressed)
        HandleHonk(null);
    }

    public void HandleHonk(Player p) => SFXAudioSource.PlayOneShot(honkSound);

    // public void PlayDashSound() => SFXAudioSource.PlayOneShot(dashSounds.OrderBy(n => Guid.NewGuid()).ToArray()[0]);
  }

}
