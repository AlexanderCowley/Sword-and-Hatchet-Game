using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine
{
    IState[] States = new IState[5];
    public MonoBehaviour PlayerObject = null;

    public int Speed = 0;
    public bool isStunned = false;
    public PlayerStateMachine(MonoBehaviour playerObject)
    {
        PlayerObject = playerObject;
        CreateStates();
        ChangeState<IdleState>();
    }

    public override void CreateStates()
    {
        _states.Add(new IdleState(this));
        _states.Add(new MoveState(this));
        //Stunned
    }

    public void ResetCombatState()
    {
        ChangeState<IdleState>();
    }
}
