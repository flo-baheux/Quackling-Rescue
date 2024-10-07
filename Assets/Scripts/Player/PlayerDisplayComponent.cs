using System;
using System.Collections;
using System.Threading;
using Cinemachine;
using UnityEngine;


public class PlayerDisplayComponent : MonoBehaviour
{
  [SerializeField] private TrailRenderer dashTrailRenderer;
  [SerializeField] private float screenshakeStrength = 0.4f;

  private Animator animator;
  private SpriteRenderer spriteRenderer;
  private CinemachineImpulseSource cameraImpulseSource;

  [SerializeField] private Sprite noHonkSprite, honkSprite;
  [SerializeField] private GameObject honkWordLeft, honkWordRight, rangeIndicator;

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
    honkWordLeft.SetActive(false);
    honkWordRight.SetActive(false);
    rangeIndicator.SetActive(false);
    Player.OnHonk += HandleHonk;
  }

  void Update()
  {
    if (spriteRenderer && spriteRenderer.flipX == false && Player.input.movementVector2D.x < 0)
      spriteRenderer.flipX = true;
    else if (spriteRenderer && spriteRenderer.flipX == true && Player.input.movementVector2D.x > 0)
      spriteRenderer.flipX = false;




    animator.SetBool("IsMoving", Mathf.Abs(Player.input.movementVector2D.magnitude) > 0.1f);

    // animator.SetFloat("xMovementVector", Player.input.movementVector.x);
    // animator.SetFloat("yMovementVector", Player.input.movementVector.y);
    // animator.SetBool("IsGrounded", Player.state.currentState.state == State.STANDING);

  }

  // private void HandleEnterDashingState(Player p) => animator.SetTrigger("Dash");

  // private void HandleDashMovementStart()
  // {
  //   dashTrailRenderer.emitting = true;
  //   cameraImpulseSource.GenerateImpulse(screenshakeStrength);
  // }

  // private void HandleExitDashingState(Player p)
  // {
  //   animator.ResetTrigger("Dash");
  //   dashTrailRenderer.emitting = false;
  // }

  private void HandleHonk(float cooldown)
  {
    StartCoroutine(HandleHonkCoroutine());
  }

  private IEnumerator HandleHonkCoroutine()
  {
    cameraImpulseSource.GenerateImpulse(screenshakeStrength);
    spriteRenderer.sprite = honkSprite;
    rangeIndicator.transform.localScale = new Vector3(0, 1, 0);
    rangeIndicator.SetActive(true);

    float rangeIndicatorScale = 0f;
    float timer = 0f;
    while (timer <= 1.0f)
    {
      timer += Time.deltaTime;
      rangeIndicatorScale = Mathf.Clamp(rangeIndicatorScale + Time.deltaTime + 5f, 0, Player.honkRadius * 2);
      rangeIndicator.transform.localScale = new Vector3(rangeIndicatorScale, 1, rangeIndicatorScale);

      honkWordLeft.SetActive(spriteRenderer.flipX);
      honkWordRight.SetActive(!spriteRenderer.flipX);
      yield return null;
    }
    rangeIndicator.transform.localScale = new Vector3(0, 1, 0);
    spriteRenderer.sprite = noHonkSprite;
    rangeIndicator.SetActive(false);
    honkWordRight.SetActive(false);
    honkWordLeft.SetActive(false);
  }
}

