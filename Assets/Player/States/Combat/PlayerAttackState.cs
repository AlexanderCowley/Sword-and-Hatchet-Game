using UnityEngine;

public class PlayerAttackState : IState
{
    PlayerCombatStateMachine StateMachine;
    public PlayerAttackState(PlayerCombatStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void OnStateEntered()
    {
        //Check input to determine which attack.
        //In this case it would the first attack available using either lAttack, hAttack, hold, press, whatever with the current weapon
        //PlayerSystem -> Weapon -> AttackData[]
    }

    public void OnStateExecute()
    {
        //Create conditions for moving from attack state to idle
        //Check for stun state??
    }

    public void OnStateExit()
    {
        
    }
}
