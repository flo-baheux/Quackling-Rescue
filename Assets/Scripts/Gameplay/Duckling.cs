using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Duckling : MonoBehaviour
{

  [NonSerialized] public static readonly HashSet<Duckling> entities = new HashSet<Duckling>();

  private FollowableByDucklingEntity mainTarget = null;
  public GameObject target = null;

  public float speed = 1.5f;

  [SerializeField] private float defaultFocusOnCharacterTimer = 5f;
  private float currentFocusOnCharacterTimer = 0f;

  private CharacterController characterController;

  private float initialY = 0;

  private Animator animator;
  private SpriteRenderer spriteRenderer;
  private bool isMoving = false;
  private Vector3 fakeGravity = new Vector3(0, -0.001f, 0);

  private bool isSwitchingTarget = false;

  void Awake()
  {
    characterController = GetComponent<CharacterController>();
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    animator = GetComponent<Animator>();
    entities.Add(this);
  }

  // Start is called before the first frame update
  void Start()
  {
    initialY = transform.position.y;
    StartCoroutine(DetectFunStuff());
    StartCoroutine(FocusManager());
  }

  // Update is called once per frame
  void Update()
  {
    MoveTowardsTarget();


    spriteRenderer.flipX = characterController.velocity.x < 0;
    animator.SetBool("IsMoving", isMoving);
    characterController.Move(fakeGravity * Time.deltaTime);
    transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
  }

  void MoveTowardsTarget()
  {
    if (!target)
      return;
    Vector3 offset = target.transform.position - transform.position;

    if (offset.magnitude >= 1.5f)
    {
      characterController.Move(speed * Time.deltaTime * offset.normalized);
      isMoving = true;
    }
    else
      isMoving = false;
  }

  IEnumerator DetectFunStuff()
  {
    while (true)
    {
      yield return null;

      if (isSwitchingTarget || currentFocusOnCharacterTimer > 0)
        continue;
      // Find closest fun stuff within range

      FunEntity funTarget = null;
      float maxDist = float.PositiveInfinity;
      foreach (var funEntity in FunEntity.entities)
      {
        if (mainTarget && funEntity.gameObject == mainTarget.gameObject)
          continue;
        var distance = (transform.position - funEntity.transform.position).magnitude;
        if (distance < maxDist && distance < funEntity.attentionCatchingDistance)
        {
          funTarget = funEntity;
          maxDist = distance;
        }
      }

      if (funTarget)
        SwitchTarget(funTarget.gameObject);
    }
  }

  public void SwitchTarget(GameObject newTarget)
  {
    isSwitchingTarget = true;
    if (mainTarget && newTarget == mainTarget.gameObject)
    {
      // Debug.LogWarning("Trying to switch target to main target - abort");
      isSwitchingTarget = false;
      return;
    }

    if (newTarget.TryGetComponent(out FollowableByDucklingEntity newFollowableEntity))
    {
      if (mainTarget)
        mainTarget.Unfollow(this);
      mainTarget = newFollowableEntity;
      target = newFollowableEntity.Follow(this);
      if (newTarget.TryGetComponent<Player>(out _))
        currentFocusOnCharacterTimer = defaultFocusOnCharacterTimer;
    }
    isSwitchingTarget = false;
    return;
  }

  IEnumerator FocusManager()
  {
    while (true)
    {
      while (currentFocusOnCharacterTimer >= 0)
      {
        currentFocusOnCharacterTimer -= Time.deltaTime;
        yield return null;
      }
      yield return null;
    }
  }
}


