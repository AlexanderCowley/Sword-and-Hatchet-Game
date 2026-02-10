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
        //Debug.Log("Combat Idle State Entered");
    }

    public void OnStateExecute()
    {
        if(CombatController.Instance.AttackBuffer.Count > 0)
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
