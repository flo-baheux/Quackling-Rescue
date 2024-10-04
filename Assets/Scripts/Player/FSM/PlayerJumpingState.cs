using UnityEngine;

namespace Player
{
  public class PlayerJumpingState : PlayerState
  {
    public PlayerJumpingState(Player player) : base(player)
    {
      this.state = State.JUMPING;
    }

    public override void Enter()
    {
      Player.playerVelocity.y += Mathf.Sqrt(Player.jumpHeight * -3f * Player.gravityValue);

      base.Enter();
    }

    public override State? CustomUpdate()
    {
      if (Player.IsGrounded3D())
        return State.STANDING;

      if (Player.playerVelocity.y <= 0)
        return State.FALLING;

      // Player.input.controlsEnabled = false;
      return base.CustomUpdate();
    }
  }
}
