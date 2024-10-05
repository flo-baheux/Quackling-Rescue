using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : MonoBehaviour
{
  [NonSerialized] public PlayerInput playerInput;
  [NonSerialized] public InputActionAsset actions;

  // [SerializeField] private float jumpBuffer = 0.075f;
  // public float JumpBuffer => jumpBuffer;

  public Vector2 movementVector2D = Vector2.zero;
  public Vector3 movementVector3D = Vector3.zero;
  public bool controlsEnabled = true;

  private bool honkPressed = false, honkHeld = false, honkReleased = false;
  // private bool jumpPressed = false, jumpHeld = false, jumpReleased = false, jumpBuffered = false;

  // public bool JumpBuffered => controlsEnabled && jumpBuffered;
  // public bool JumpPressed => controlsEnabled && jumpPressed;
  // public bool JumpHeld => controlsEnabled && jumpHeld;
  // public bool JumpReleased => controlsEnabled && jumpReleased;

  public bool HonkPressed => controlsEnabled && honkPressed;
  public bool HonkHeld => controlsEnabled && honkHeld;
  public bool HonkReleased => controlsEnabled && honkReleased;


  private void Awake()
  {
    playerInput = GetComponent<PlayerInput>();
  }

  private void Start()
  {
    actions = playerInput.actions;
  }

  void Update()
  {
    // jumpPressed = actions["Jump"].WasPressedThisFrame();
    // if (jumpPressed)
    // {
    //   StartCoroutine(BufferJump());
    //   jumpHeld = true;
    // }

    // jumpReleased = actions["Jump"].WasReleasedThisFrame();
    // if (jumpReleased)
    //   jumpHeld = false;

    honkPressed = actions["Honk"].WasPressedThisFrame();
    if (honkPressed)
      honkHeld = true;

    honkReleased = actions["Honk"].WasReleasedThisFrame();
    if (honkReleased)
      honkHeld = false;


  }

  public void HandleMovement(InputAction.CallbackContext context)
  {
    Vector2 movementVector = context.ReadValue<Vector2>();
    movementVector2D = movementVector;
    movementVector3D = new Vector3(movementVector.x, 0, movementVector.y);
  }

  // private IEnumerator BufferJump()
  // {
  //   jumpBuffered = true;
  //   yield return new WaitForSeconds(JumpBuffer);
  //   jumpBuffered = false;
  // }
}
