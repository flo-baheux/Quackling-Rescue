using Cinemachine;
using UnityEngine;

namespace Player
{
  public class PlayerDisplayComponent : MonoBehaviour
  {
    [SerializeField] private TrailRenderer dashTrailRenderer;
    [SerializeField] private float screenshakeStrength = 0.2f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CinemachineImpulseSource cameraImpulseSource;

    [SerializeField] private Sprite frontSprite, backSprite;

    private Player Player;

    private void Awake()
    {
      Player = GetComponent<Player>();
      animator = GetComponent<Animator>();
      spriteRenderer = GetComponentInChildren<SpriteRenderer>();
      cameraImpulseSource = GetComponent<CinemachineImpulseSource>();

      // dashTrailRenderer.emitting = false;
    }

    private void Start()
    {
    }

    void Update()
    {
      if (spriteRenderer && spriteRenderer.flipX == false && Player.input.movementVector2D.x < 0)
        spriteRenderer.flipX = true;
      else if (spriteRenderer && spriteRenderer.flipX == true && Player.input.movementVector2D.x > 0)
        spriteRenderer.flipX = false;

      if (spriteRenderer)
        spriteRenderer.sprite = Player.input.movementVector2D.y <= 0 ? frontSprite : backSprite;

      // animator.SetFloat("xMovementVector", Player.input.movementVector.x);
      // animator.SetFloat("yMovementVector", Player.input.movementVector.y);
      // animator.SetBool("IsGrounded", Player.state.currentState.state == State.STANDING);

    }

    // private void HandleEnterDashingState(Player p) => animator.SetTrigger("Dash");

    // private void HandleDashMovementStart()
    // {
    //   cameraImpulseSource.GenerateImpulse(screenshakeStrength);
    //   dashTrailRenderer.emitting = true;
    // }

    // private void HandleExitDashingState(Player p)
    // {
    //   animator.ResetTrigger("Dash");
    //   dashTrailRenderer.emitting = false;
    // }
  }
}
