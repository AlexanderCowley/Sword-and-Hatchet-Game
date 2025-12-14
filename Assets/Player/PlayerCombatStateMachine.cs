using UnityEngine;

public class PlayerCombatStateMachine : AbstractStateMachine
{
    MonoBehaviour PlayerObject = null;
    public PlayerCombatStateMachine(MonoBehaviour playerObject)
    {
        PlayerObject = playerObject;
        CreateStates();
    }

    public override void CreateStates()
    {
        _states.Add(new CombatIdleState(this));
        _states.Add(new PlayerAttackState(this));
        //AttackState -> LAttack1 -> HAttack2 -> etc.
        //Stunned
    }
}
