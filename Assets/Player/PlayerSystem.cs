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

    public PlayerStateMachine MovementStateMachine = null;
    public PlayerCombatStateMachine CombatStateMachine = null;

    public HitBox[] WeaponHitboxes;

    public void Awake()
    {
        PlayerTransform = transform;
        playerRigidbody = GetComponent<Rigidbody>();
        CameraObject = PlayerTransform.GetChild(3);
        WeaponHitboxes = PlayerTransform.GetChild(6).GetComponentsInChildren<HitBox>(true);
        PlayerMovementData = new PlayerData(15f, playerRigidbody.linearVelocity, transform.position);
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        MovementStateMachine = new PlayerStateMachine(this);
        CombatStateMachine = new PlayerCombatStateMachine(this);
    }

    public void ResetPlayerStates()
    {
        MovementStateMachine.ResetCombatState();
        CombatStateMachine.ResetCombatState();
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
        //Process Input
        if(!GameManager.PlayerInputEnabled) return;
        GameManager.CombatInput.lAttack = Input.GetMouseButtonDown(0);
        GameManager.CombatInput.hAttack = Input.GetMouseButtonDown(1);

        MovementStateMachine.UpdateState();
        CombatStateMachine.UpdateState();

        
        UpdateCamera();
    }
}
