using System.Collections;
using TMPro;
using UnityEngine;

public class DucklingDetector : MonoBehaviour
{

  [SerializeField] private int requiredDuck = 3;
  [SerializeField] private Door interactingWith = null;
  [SerializeField] TextMeshProUGUI displayedCount;
  bool hasBeenTriggered = false;
  bool lastChangeMade = false;

  private WaitForSeconds waitTimer = new WaitForSeconds(0.2f);

  private int _currentCountIn = 0;
  public int CurrentCountIn
  {
    get => _currentCountIn;
    set
    {
      OnCurrentCountInChange();
      _currentCountIn = value;
    }
  }

  void Start()
  {
    displayedCount.text = $"0 / {requiredDuck}";
    StartCoroutine(DetectDucklings());
  }

  // Update is called once per frame
  void Update()
  {
    if (!hasBeenTriggered && CurrentCountIn >= requiredDuck)
    {
      interactingWith.Interact();
      hasBeenTriggered = true;
    }
  }

  private void OnCurrentCountInChange()
  {
    if (lastChangeMade)
      return;
    displayedCount.text = $"{CurrentCountIn} / {requiredDuck}";
    if (CurrentCountIn > 0 && CurrentCountIn < requiredDuck)
      displayedCount.color = Color.red;
    else if (CurrentCountIn >= requiredDuck)
      displayedCount.color = Color.green;
    else
      displayedCount.color = Color.white;

    if (hasBeenTriggered)
      lastChangeMade = true;
  }

  private IEnumerator DetectDucklings()
  {
    RaycastHit[] results = new RaycastHit[requiredDuck];
    while (!hasBeenTriggered)
    {
      yield return waitTimer;
      CurrentCountIn = Physics.BoxCastNonAlloc(transform.position, Vector3.one * 4, Vector3.up, results, Quaternion.identity, 1f, LayerMask.GetMask("Duckling"));
    }
  }
}
