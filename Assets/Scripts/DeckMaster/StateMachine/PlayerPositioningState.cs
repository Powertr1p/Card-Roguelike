namespace DeckMaster.StateMachine
{
    public class PlayerPositioningState : State
    {
        public PlayerPositioningState()
        {
            Name = TurnState.PlayerPositioningTurn;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            NextState = new PlayerTurnState();
            Stage = Event.Exit;
            
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}