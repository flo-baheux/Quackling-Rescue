using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
  private bool open = false;
  public AudioClip openSound, closeSound;
  private AudioSource audioSource;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();
  }

  public void Interact()
  {
    open = !open;
    audioSource.PlayOneShot(open ? openSound : closeSound);
    GetComponent<Collider>().enabled = !open;
    GetComponent<Animator>().SetTrigger(open ? "OpenDoor" : "CloseDoor");
  }
}
