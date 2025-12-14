using UnityEngine;

public class CombatIdleState : IState
{
    public PlayerCombatStateMachine StateMachine = null;

    public CombatIdleState(PlayerCombatStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void OnStateEntered()
    {
        
    }

    public void OnStateExecute()
    {
        if(GameManager.CombatInput.lAttack || GameManager.CombatInput.hAttack)
        {
            //Transition to Attack state here
            StateMachine.ChangeState<MoveState>();
            return;
        }
    }

    public void OnStateExit()
    {
        
    }
}
