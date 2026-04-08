using UnityEngine;

public class PlayerCombatStateMachine : AbstractStateMachine
{
    public MonoBehaviour PlayerObject = null;
    public PlayerSystem Player = null;
    public Animator PlayerAnimator = null;
    WeaponData CurrentWeapon;
    public bool isAttacking = false;
    public AttackData CurrentAttack;
    public PlayerCombatStateMachine(MonoBehaviour playerObject)
    {
        PlayerObject = playerObject;
        Player = playerObject.GetComponent<PlayerSystem>();
        PlayerAnimator = Player.gameObject.transform.GetChild(5).GetComponent<Animator>();
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
    
    public void ResetCombatState()
    {
        ChangeState<CombatIdleState>();
    }

}
