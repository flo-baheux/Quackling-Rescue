using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{

  public float strength = 10f;

  void OnTriggerStay(Collider other)
  {
    if (other.TryGetComponent(out CharacterController characterController))
      characterController.Move(transform.up * strength * Time.deltaTime);
  }
}
