using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackState : IState
{
    PlayerCombatStateMachine StateMachine;
    PlayerSystem Player;
    public AttackData CurrentAttack;
    AttackData NextAttack;
    WeaponData CurrentWeapon;

    int inputIndex = -1;

    public PlayerAttackState(PlayerCombatStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        Player = GameManager.Player;
    }

    void ProcessAttack()
    {
        if(!CurrentAttack)
        {
            Debug.Log("No current attack assigned");
            return;
        }
        SetupHitboxes();
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
            hitBoxes[i].gameObject.transform.localPosition = CurrentAttack.HitboxPositions[i];
        }
    }

    public void OnStateEntered()
    {
        AttackInput combatInput = CombatController.AttackBuffer.Peek();
        inputIndex = (int)combatInput;
        if(combatInput == AttackInput.None)
        {
            Debug.LogWarning("Incorrect Type of input");
            return;
        }
        CurrentWeapon = CombatController.Instance.CurrentPlayerWeapon;
        if(inputIndex <= CurrentWeapon.StartingAttacks.Length - 1)
        {
            CurrentAttack = CurrentWeapon.StartingAttacks[inputIndex];
            StateMachine.CurrentAttack = CurrentAttack;
        }

        GetAttack();
        ProcessAttack();
    }

    public void OnStateExecute()
    {
        //Create conditions for moving from attack state to idle
        //Check for stun state
        //Checks if the Queue is empty
        if(CombatController.AttackBuffer.Count == 0)
        {
            //ChangeState to Idle, don't feel like testing this right now
            return;
        }

        AttackInput combatInput = CombatController.AttackBuffer.Peek();
        inputIndex = (int)combatInput;

        //Checks if the weapon has changed
        if(CurrentWeapon != CombatController.Instance.CurrentPlayerWeapon)
        {
            CurrentWeapon = CombatController.Instance.CurrentPlayerWeapon;
        }

        //Enable this after CurrentAttack runs its functions in ProcessAttack
        GetAttack();
        ProcessAttack();
    }

    void GetAttack()
    {
        if(NextAttack == null || NextAttack.nextAttacks.Length == 0)
        {
            //Reset attacks
            if(inputIndex > CurrentWeapon.StartingAttacks.Length - 1)
            {
                Debug.Log("No combo uses this input");
                CombatController.AttackBuffer.Dequeue();
                NextAttack = null;
                return;
            }
            Debug.Log("Starting attack from weapon");
            NextAttack = CurrentWeapon.StartingAttacks[inputIndex];
        }
        else if(inputIndex <= CurrentAttack.nextAttacks.Length - 1)
        {
            Debug.Log("Valid combo input");
            NextAttack = CurrentAttack.nextAttacks[inputIndex];
        }
        else
        {
            Debug.Log("Not a valid combo input.");
            NextAttack = null;
            CombatController.AttackBuffer.Dequeue();
        }

        //If CurrentAttack is still null
        if(!CurrentAttack)
        {
            CurrentAttack = NextAttack;
            StateMachine.CurrentAttack = CurrentAttack;
            NextAttack = null;
        }
    }

    public void OnStateExit()
    {
        CurrentAttack = null;
        StateMachine.CurrentAttack = null;
        NextAttack = null;
        inputIndex = -1;
    }
}
