using UnityEngine;

namespace Player
{
  public class PlayerStandingState : PlayerState
  {
    public bool wasRecentlyStanding = true;

    public PlayerStandingState(Player player) : base(player)
    {
      this.state = State.STANDING;
    }

    public override State? CustomUpdate()
    {
      if (!Player.IsGrounded3D())
        return State.FALLING;

      if (Player.input.JumpBuffered || Player.input.JumpPressed)
        return State.JUMPING;

      return base.CustomUpdate();
    }
  }
}
