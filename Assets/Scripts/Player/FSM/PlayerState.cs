using System;


public enum State
{
  STANDING
};

public abstract class PlayerState
{
  public State state;
  protected Player Player;

  public PlayerState(Player player) => Player = player;

  public event Action<Player> OnEnter;
  public event Action<Player> OnExit;

  public virtual State? CustomUpdate() => null;
  public virtual State? CustomFixedUpdate() => null;

  public virtual void Enter() => OnEnter?.Invoke(Player);
  public virtual void Exit() => OnExit?.Invoke(Player);
}

