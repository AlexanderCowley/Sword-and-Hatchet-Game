using UnityEngine;

public class PlayerAttackState : IState
{
    PlayerCombatStateMachine StateMachine;
    PlayerSystem Player;
    int AttackBufferLength = 0;
    AttackData CurrentAttack;
    WeaponData CurrentWeapon;

    public PlayerAttackState(PlayerCombatStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        Player = StateMachine.PlayerObject.GetComponent<PlayerSystem>();
    }

    void ProcessAttack()
    {
        //Actual attack implementation
    }

    public void OnStateEntered()
    {
        AttackBufferLength = CombatController.Instance.AttackBuffer.Count;
        AttackInput combatInput = CombatController.Instance.AttackBuffer.Peek();
        if(combatInput == AttackInput.None)
        {
            Debug.LogWarning("Incorrect Type of input");
            return;
        }
        //Check weapon
        CurrentWeapon = CombatController.Instance.CurrentPlayerWeapon;
        CurrentAttack = CurrentWeapon.StartingAttacks[(int)combatInput];
        ProcessAttack();
    }

    public void OnStateExecute()
    {
        //Create conditions for moving from attack state to idle
        //Check for stun state
        //Checks if any new attacks are in the queue
        if(AttackBufferLength == CombatController.Instance.AttackBuffer.Count)
        {
            return;
        }

        AttackBufferLength = CombatController.Instance.AttackBuffer.Count;
        AttackInput combatInput = CombatController.Instance.AttackBuffer.Peek();
        
        //Checks if the weapon has changed
        if(CurrentWeapon != CombatController.Instance.CurrentPlayerWeapon)
        {
            CurrentWeapon = CombatController.Instance.CurrentPlayerWeapon;
            CurrentAttack = CurrentWeapon.StartingAttacks[(int)combatInput];
        }
        else
        {
            //Update AttackData based on the current one
            CurrentAttack = CurrentAttack.nextAttacks[(int)combatInput];
        }
        
        ProcessAttack();
    }

    public void OnStateExit()
    {
        
    }
}
