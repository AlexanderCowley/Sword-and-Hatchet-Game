using UnityEngine;

public struct PlayerInput
{
    public bool lAttack;
    public bool hAttack;
    public bool jumpInput;
}

public struct PlayerData
{
    public float PlayerSpeed;
    public Vector3 PlayerVelocity;
    public Vector3 PlayerPosition;

    public PlayerData(float Speed, Vector3 Velocity, Vector3 Position)
    {
        PlayerSpeed = Speed;
        PlayerPosition = Position;
        PlayerVelocity = Velocity;
    }

}

public class PlayerSystem : MonoBehaviour
{

    public PlayerData PlayerMovementData;
    Transform PlayerTransform;
    Rigidbody playerRigidbody;
    Quaternion rotation;

    float RotSpeedX = 220f;
    float RotSpeedY = 220f;

    Transform CameraObject;

    PlayerStateMachine MovementStateMachine = null;
    PlayerCombatStateMachine CombatStateMachine = null;

    public void Awake()
    {
        PlayerTransform = transform;
        playerRigidbody = GetComponent<Rigidbody>();
        CameraObject = transform.GetChild(3);

        PlayerMovementData = new PlayerData(15f, playerRigidbody.linearVelocity, transform.position);
        
        MovementStateMachine = new PlayerStateMachine(this);
        CombatStateMachine = new PlayerCombatStateMachine(this);
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateCamera()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            PlayerTransform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * RotSpeedX * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            CameraObject.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * RotSpeedY * Time.deltaTime);
        }
    }

    public void Update()
    {
        /*float xAxis = Input.GetAxis("Vertical");
        float yAxis = Input.GetAxis("Horizontal");
        PlayerTransform.position += (PlayerTransform.forward * xAxis +
         PlayerTransform.right * yAxis) *
         PlayerMovementData.PlayerSpeed * Time.deltaTime;*/
        
        //Process Input
        GameManager.CombatInput.lAttack = Input.GetMouseButtonDown(0);
        GameManager.CombatInput.hAttack = Input.GetMouseButtonDown(1);

        MovementStateMachine.UpdateState();
        CombatStateMachine.UpdateState();

        
        UpdateCamera();
    }
}
