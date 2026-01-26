using System.Collections;
using UnityEngine;

public class PlayerAttackState : IState
{
    PlayerCombatStateMachine StateMachine;
    PlayerSystem Player;
    AttackData CurrentAttack;
    WeaponData CurrentWeapon;

    public PlayerAttackState(PlayerCombatStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        //Player should be a static object in GM
        Player = StateMachine.PlayerObject.GetComponent<PlayerSystem>();
    }

    void ProcessAttack()
    {
        //Actual attack implementation
        //Used to reduce calls.
        //Find way to get earliest transition during an animation or see what are common factors amongst animations
        //0.6 feels the most responsive at them moment. We will see.
        if(GameManager.PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6)
        {
            return;
        }

        SetupHitboxes();
        Debug.Log("Plays attack animation");
        GameManager.PlayerAnimator.Play(CurrentAttack.AnimationName, 0);
    }

    void SetupHitboxes()
    {
        HitBox[] hitBoxes = Player.WeaponHitboxes;
        int hitboxCount = CurrentAttack.HitboxPositions.Length;
        //Maybe use a set amount to remove for loop?
        //This is going to happen almost instantly. Might need a coroutine?
        for(int i = 0; i < hitboxCount; i++)
        {
            hitBoxes[i].AssignHitboxInfo(CurrentAttack);
            hitBoxes[i].gameObject.transform.position = Player.transform.position + CurrentAttack.HitboxPositions[i];
        }
    }

    public void OnStateEntered()
    {
        AttackInput combatInput = CombatController.Instance.AttackBuffer.Peek();
        if(combatInput == AttackInput.None)
        {
            Debug.LogWarning("Incorrect Type of input");
            return;
        }
        //Check weapon since it is going from Combat Idle to Attack?
        CurrentWeapon = CombatController.Instance.CurrentPlayerWeapon;
        CurrentAttack = CurrentWeapon.StartingAttacks[(int)combatInput];
        ProcessAttack();
    }

    public void OnStateExecute()
    {
        //Create conditions for moving from attack state to idle
        //Check for stun state
        //Checks if the Queue is empty
        if(CombatController.Instance.AttackBuffer.Count == 0)
        {
            return;
        }
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
        //Reset to look for other combos to go from
        if(CurrentAttack.nextAttacks.Length == 0)
        {
            CurrentAttack = CurrentWeapon.StartingAttacks[(int)combatInput];
        }
        
        ProcessAttack();
    }

    public void OnStateExit()
    {
        
    }
}
