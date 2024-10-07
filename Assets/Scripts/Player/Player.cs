using System;
using System.Collections;
using UnityEngine;


public class Player : FollowableByDucklingEntity

{
  [Header("Movements")]
  public float gravityValue = 8f;
  // public float jumpHeight = 1f;
  public float speed = 2f;

  [Header("Gameplay")]
  public float honkRadius = 20f;
  public float honkCooldown = 5f;
  private bool honkIsOnCooldown = false;
  public Action<float> OnHonk;

  // Components
  [Header("Components")]
  [NonSerialized] public CharacterController characterController;
  public PlayerStateComponent state { get; private set; }
  public PlayerInputComponent input { get; private set; }

  public Vector3 playerVelocity;
  // Gameplay Manager
  private GameManager gameplayManager;

  void Awake()
  {
    state = GetComponent<PlayerStateComponent>();
    input = GetComponent<PlayerInputComponent>();
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    // Don't fall when grounded
    bool isGrounded = IsGrounded3D();
    if (isGrounded && playerVelocity.y < 0f)
      playerVelocity.y = -0.01f;
    else if (!isGrounded)
      playerVelocity.y += gravityValue * Time.deltaTime;

    if (input.controlsEnabled)
    {
      characterController.Move(input.movementVector3D * Time.deltaTime * speed);
      if (input.HonkPressed)
        Honk();
    }

    transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    characterController.Move(playerVelocity * Time.deltaTime);
  }

  public bool IsGrounded3D()
  {
    // NB: isGrounded not working as expected
    // if (characterController)
    //   return characterController.isGrounded;

    Vector3 center = new(characterController.bounds.center.x, characterController.bounds.min.y, characterController.bounds.center.z);
    Vector3 size = new(characterController.bounds.size.x / 4, 0.1f, characterController.bounds.size.z / 4);
    Collider[] results = new Collider[1];
    return Physics.OverlapBoxNonAlloc(center, size, results, Quaternion.identity, LayerMask.GetMask("Ground")) != 0;
  }

  public void RespawnToPosition(Vector2 position)
  {
    // rigidBody.position = position;
    // state.TransitionToState(State.FALLING);
    // input.controlsEnabled = true;
  }

  public void Honk()
  {
    if (honkIsOnCooldown)
      return;

    StartCoroutine(HonkCooldownCoroutine());
    OnHonk?.Invoke(honkCooldown);
    foreach (Duckling existingDuckling in Duckling.entities)
    {
      var distance = (transform.position - existingDuckling.transform.position).magnitude;
      if (distance < honkRadius)
        existingDuckling.SwitchTarget(gameObject);
    }
  }

  private IEnumerator HonkCooldownCoroutine()
  {
    honkIsOnCooldown = true;
    yield return new WaitForSeconds(honkCooldown);
    honkIsOnCooldown = false;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = new Color(200, 200, 0, 0.3f);
    Gizmos.DrawSphere(transform.position, honkRadius);
  }
}
