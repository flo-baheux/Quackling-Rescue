using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
  public bool triggerOnTimer = false;
  public float timerStayOpenForSeconds = 5f;
  public float timerStayCloseForSeconds = 5f;
  public bool startsOpen = false;

  private bool open = false;
  public AudioClip openSound, closeSound;
  private AudioSource audioSource;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Start()
  {
    open = startsOpen;
    if (triggerOnTimer)
      StartCoroutine(TimerCoroutine());
  }

  private void SwitchState()
  {
    open = !open;
    audioSource.PlayOneShot(open ? openSound : closeSound);
    GetComponent<Collider>().enabled = !open;
    GetComponent<Animator>().SetTrigger(open ? "OpenDoor" : "CloseDoor");
  }

  public void Interact()
  {
    if (triggerOnTimer)
      return;
    SwitchState();
  }

  private IEnumerator TimerCoroutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(open ? timerStayOpenForSeconds : timerStayCloseForSeconds);
      SwitchState();
    }
  }
}
