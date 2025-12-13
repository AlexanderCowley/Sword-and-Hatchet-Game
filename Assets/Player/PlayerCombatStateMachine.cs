using UnityEngine;

public class PlayerCombatStateMachine : AbstractStateMachine
{
    MonoBehaviour PlayerObject = null;
    public PlayerCombatStateMachine(MonoBehaviour playerObject)
    {
        PlayerObject = playerObject;
    }

    public override void CreateStates()
    {
        //Idle
        //Attacking
        // -> LAttack1 -> HAttack2 -> etc
        //Stunned
    }
}
