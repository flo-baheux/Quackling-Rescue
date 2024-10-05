using UnityEngine;

public class RotatingWall : MonoBehaviour
{
  public bool clockwise = true;
  public float rotationSpeed = 50f;

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(Vector3.up * (clockwise ? rotationSpeed : -rotationSpeed) * Time.deltaTime, Space.Self);
  }
}
