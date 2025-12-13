using UnityEngine;

public class MoveState : IState
{
    PlayerStateMachine StateMachine = null;
    Transform PlayerTransform = null;
    PlayerData PlayerStats;
    int MoveSpeed = 20;

    public MoveState(PlayerStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        PlayerTransform = StateMachine.PlayerObject.transform;
        PlayerStats = StateMachine.PlayerObject.GetComponent<PlayerSystem>().PlayerMovementData;
    }

    public void OnStateEntered()
    {
        StateMachine.Speed = MoveSpeed;
    }

    public void OnStateExecute()
    {
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            StateMachine.ChangeState<IdleState>();
            return;
        }

        float xAxis = Input.GetAxis("Vertical");
        float yAxis = Input.GetAxis("Horizontal");
        PlayerTransform.position += (PlayerTransform.forward * xAxis +
         PlayerTransform.right * yAxis) *
         PlayerStats.PlayerSpeed * Time.deltaTime;
    }

    public void OnStateExit()
    {
        
    }
}
