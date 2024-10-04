namespace Player
{
  public class PlayerFallingState : PlayerState
  {
    public PlayerFallingState(Player player) : base(player)
    {
      this.state = State.FALLING;
    }

    public override State? CustomUpdate()
    {
      if (Player.IsGrounded3D())
        return State.STANDING;

      // Player.input.controlsEnabled = false;
      return base.CustomUpdate();
    }
  }
}
