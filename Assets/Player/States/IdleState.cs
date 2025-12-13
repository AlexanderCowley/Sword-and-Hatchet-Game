using UnityEngine;

public class IdleState : IState
{
    public PlayerStateMachine StateMachine = null;

    public IdleState(PlayerStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void OnStateEntered()
    {
        StateMachine.Speed = 0;
    }

    public void OnStateExecute()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Transition to move state here
            StateMachine.ChangeState<MoveState>();
            return;
        }
    }

    public void OnStateExit()
    {
        //Nothing
    }
}
