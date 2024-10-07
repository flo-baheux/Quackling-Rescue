using UnityEngine;

public class DisplaySphere : MonoBehaviour
{
  public float radius = 2f;
  public Color color = Color.red;

  void OnDrawGizmos()
  {
    Gizmos.color = color;
    Gizmos.DrawSphere(transform.position, radius);
  }
}
