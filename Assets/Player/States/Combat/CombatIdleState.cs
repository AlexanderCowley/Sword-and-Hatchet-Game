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
        if(CombatController.AttackBuffer.Count > 0)
        {
            //Transition to Attack state here
            StateMachine.ChangeState<PlayerAttackState>();
            return;
        }
    }

    public void OnStateExit()
    {
        
    }
}
