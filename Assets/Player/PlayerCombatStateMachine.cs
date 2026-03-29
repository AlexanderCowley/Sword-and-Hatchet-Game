using UnityEngine;

public class PlayerCombatStateMachine : AbstractStateMachine
{
    public MonoBehaviour PlayerObject = null;
    WeaponData CurrentWeapon;
    public bool isAttacking = false;
    public AttackData CurrentAttack;
    public PlayerCombatStateMachine(MonoBehaviour playerObject)
    {
        PlayerObject = playerObject;
        CreateStates();
        CurrentWeapon = CombatController.Instance.CurrentPlayerWeapon;
    }

    public override void CreateStates()
    {
        _states.Add(new CombatIdleState(this));
        _states.Add(new PlayerAttackState(this));
        //Stunned
        ChangeState<CombatIdleState>();
    }
}
