using System;
using UnityEngine;

namespace Player
{
  public class Player : FollowableByDucklingEntity

  {
    [Header("Movements")]
    public float gravityValue = 8f;
    // public float jumpHeight = 1f;
    public float speed = 2f;

    [Header("Gameplay")]
    public float honkRadius = 20f;
    // [SerializeField] private float dashStrength = 40f;
    // public float DashStrength => dashStrength;

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
      // gameplayManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    void FixedUpdate()
    {
      if (input.controlsEnabled)
      {
        // float horizontalVelocity = input.movementVector.x * runningSpeed;
        // rigidBody.velocity = new Vector2(horizontalVelocity, rigidBody.velocity.y);

      }
    }

    public bool IsGrounded2D()
    {
      if (characterController)
        return characterController.isGrounded;

      Vector2 center = new(characterController.bounds.center.x, characterController.bounds.min.y);
      Vector2 size = new(characterController.bounds.size.x, 0.1f);
      RaycastHit2D[] results = new RaycastHit2D[1];
      return Physics2D.BoxCastNonAlloc(center, size, 0f, Vector2.down, results, LayerMask.GetMask("Ground")) != 0;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
      // if (other.CompareTag("Threat"))
      //   state.TransitionToState(State.DEAD);
      // if (other.CompareTag("LevelEnd"))
      //   gameplayManager.StartNextLevel();
    }

    public void Honk()
    {
      foreach (Duckling existingDuckling in Duckling.entities)
      {
        var distance = (transform.position - existingDuckling.transform.position).magnitude;
        if (distance < honkRadius)
          existingDuckling.AcquirePlayerTarget();
      }
    }

    void OnDrawGizmos()
    {
      Gizmos.color = new Color(200, 200, 0, 0.3f);
      Gizmos.DrawSphere(transform.position, honkRadius);
    }
  }
}