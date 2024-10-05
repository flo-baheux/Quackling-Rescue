using Cinemachine;
using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
  Camera cam;

  private void Start()
  {
    cam = GetComponent<Camera>();
    Matrix4x4 matrix = cam.projectionMatrix;
    matrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
    cam.projectionMatrix = matrix;
  }

  private void Update()
  {
  }
  // private void OnEnable()
  // {
  //   GetComponent<Camera>().ResetProjectionMatrix();
  //   CinemachineCore.CameraUpdatedEvent.AddListener(ApplyMirrorEffect);
  // }

  // private void OnDisable()
  // {
  //   GetComponent<Camera>().ResetProjectionMatrix();
  //   CinemachineCore.CameraUpdatedEvent.RemoveListener(ApplyMirrorEffect);
  // }

  // void ApplyMirrorEffect(CinemachineBrain brain)
  // {
  //   var cam = brain.OutputCamera;
  //   if (cam != null)
  //   {
  //     cam.ResetProjectionMatrix();
  //     cam.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
  //   }
  // }
}