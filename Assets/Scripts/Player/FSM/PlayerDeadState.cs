namespace Player
{
  public class PlayerDeadState : PlayerState
  {
    public PlayerDeadState(Player player) : base(player)
    {
      this.state = State.DEAD;
    }

    public override State? CustomUpdate()
    {
      // Player.input.controlsEnabled = false;
      return base.CustomUpdate();
    }
  }
}
