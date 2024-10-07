using System.Collections.Generic;
using UnityEngine;

public class PlayerStateComponent : MonoBehaviour
{
  private Player Player;

  public PlayerStandingState standingState { get; private set; }

  private Dictionary<State, PlayerState> states;

  public PlayerState currentState { get; private set; }
  public PlayerState previousState { get; private set; }
  public float lastTransitionTime { get; private set; } = 0f;

  private void Awake()
  {
    Player = GetComponent<Player>();
    standingState = new PlayerStandingState(Player);

    states = new Dictionary<State, PlayerState>() {
      {State.STANDING, standingState},
    };

    currentState = standingState;
    previousState = standingState;
  }

  void Update()
  {
    State? newState = currentState.CustomUpdate();
    if (newState.HasValue)
      TransitionToState(newState.Value);
  }

  void FixedUpdate()
  {
    State? newState = currentState.CustomFixedUpdate();
    if (newState.HasValue)
      TransitionToState(newState.Value);
  }

  public void TransitionToState(State newState)
  {
    if (newState == currentState.state)
    {
      Debug.LogWarning($"Attempt to transition to current state prevented - {currentState.state}");
      return;
    }
    currentState.Exit();
    previousState = currentState;
    lastTransitionTime = Time.time;
    currentState = states[newState];
    currentState.Enter();
    Debug.Log($"Transition from {previousState.state} to {currentState.state}");
  }
}

