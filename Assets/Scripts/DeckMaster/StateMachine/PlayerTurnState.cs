namespace DeckMaster.StateMachine
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState()
        {
            Name = TurnState.PlayerTurn;
        }
        
        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            NextState = new DeckMasterState();
            Stage = Event.Exit;
            
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}