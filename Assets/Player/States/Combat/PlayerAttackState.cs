using UnityEngine;

public class PlayerAttackState : IState
{
    PlayerCombatStateMachine StateMachine;
    PlayerSystem Player;
    int AttackBufferLength = 0;
    AttackData CurrentAttack;
    WeaponData CurrentWeapon;

    HitBox[] HitBoxes;

    public PlayerAttackState(PlayerCombatStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        //Player should be a static object in GM
        Player = StateMachine.PlayerObject.GetComponent<PlayerSystem>();
    }

    void ProcessAttack()
    {
        //Actual attack implementation
        SetupHitboxes();
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
        AttackBufferLength = CombatController.Instance.AttackBuffer.Count;
        AttackInput combatInput = CombatController.Instance.AttackBuffer.Peek();
        if(combatInput == AttackInput.None)
        {
            Debug.LogWarning("Incorrect Type of input");
            return;
        }
        //Check weapon since it is going from Combat Idle to Attack
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
