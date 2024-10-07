using UnityEngine;


public class PlayerStandingState : PlayerState
{
  public bool wasRecentlyStanding = true;

  public PlayerStandingState(Player player) : base(player)
  {
    this.state = State.STANDING;
  }

  public override State? CustomUpdate()
  {
    return base.CustomUpdate();
  }
}

