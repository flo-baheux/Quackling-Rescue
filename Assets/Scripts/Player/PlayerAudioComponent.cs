using System;
using System.Linq;
using UnityEngine;

namespace Player
{

  public class PlayerAudioComponent : MonoBehaviour
  {
    public AudioClip jumpSound;
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
      player.state.jumpingState.OnEnter += HandleJump;
    }

    public void HandleJump(Player p) => SFXAudioSource.PlayOneShot(jumpSound);

    // public void PlayDashSound() => SFXAudioSource.PlayOneShot(dashSounds.OrderBy(n => Guid.NewGuid()).ToArray()[0]);
  }

}
