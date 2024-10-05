using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duckling : MonoBehaviour
{

  [NonSerialized] public static readonly HashSet<Duckling> entities = new HashSet<Duckling>();

  private bool following = false;
  FollowableByDucklingEntity currentlyFollowing = null;
  private GameObject target = null;

  public float speed = 1f;

  public float funEntityDetectionRadius = 30f;
  [SerializeField] private float defaultFocusOnCharacterTimer = 5f;
  private float currentFocusOnCharacterTimer = 0f;

  private CharacterController characterController;

  private float initialY = 0;

  private GameObject playerGameObject;
  private Animator animator;
  private SpriteRenderer spriteRenderer;
  private bool isMoving = false;

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
    if (!playerGameObject)
      playerGameObject = GameObject.FindWithTag("Player");

    if (!following)
    {
      AcquirePlayerTarget();
      following = true;
    }
    else
      MoveTowardsTarget();

    transform.position = new Vector3(transform.position.x, initialY, transform.position.z);

    spriteRenderer.flipX = characterController.velocity.x < 0;
    animator.SetBool("IsMoving", isMoving);
  }

  public void AcquirePlayerTarget()
  {
    if (playerGameObject)
    {
      Player.Player p = playerGameObject.GetComponent<Player.Player>();
      SwitchTarget(p);
      currentFocusOnCharacterTimer = defaultFocusOnCharacterTimer;
    }
  }

  void MoveTowardsTarget()
  {
    Vector3 offset = target.transform.position - transform.position;
    if (offset.magnitude >= 2f)
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
      if (currentFocusOnCharacterTimer > 0)
        continue;
      // Find closest fun stuff within range

      FunEntity funTarget = null;
      float maxDist = funEntityDetectionRadius;
      foreach (var funEntity in FunEntity.entities)
      {
        var distance = (transform.position - funEntity.transform.position).magnitude;
        if (distance < maxDist)
        {
          funTarget = funEntity;
          maxDist = distance;
        }
        yield return null;
      }

      if (funTarget)
      {
        Debug.Log($"I am {gameObject.name} and I'm switching to fun target {funTarget}.");
        SwitchTarget(funTarget);
      }
    }
  }

  private void SwitchTarget(FollowableByDucklingEntity followableEntity)
  {
    if (currentlyFollowing)
      currentlyFollowing.Unfollow(this);
    currentlyFollowing = followableEntity;
    target = followableEntity.Follow(this);
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

/*
Okay so the ducklings are chasing but when following as duck, if the trigger
happens again they end up following the last one chasing, ending up in a loop.

Can it be solved by "if already currently chasing fun stuff and being first, do nothing"?
*/

