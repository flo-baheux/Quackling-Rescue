using UnityEngine;
using UnityEngine.Playables;

public class StartCutscene : MonoBehaviour
{
  private PlayableDirector director;
  [SerializeField] private GameObject story4, story5;

  void Awake()
  {
    director = GetComponent<PlayableDirector>();
  }

  void Start()
  {
    story4.SetActive(false);
    story5.SetActive(false);
  }

  void OnTriggerStay(Collider other)
  {

    if (other.TryGetComponent(out Player player))
    {
      if (player.input.actions["Honk"].WasPressedThisFrame())
      {
        player.input.controlsEnabled = false;
        if (player.currentlyFollowingCount == Duckling.entities.Count)
          story4.SetActive(true);
        else
          story5.SetActive(true);
        director.Play();
      }
    }
  }
}
