using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

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
    Rigidbody rigidbody;
    Quaternion rotation;

    float RotSpeedX = 220f;
    float RotSpeedY = 220f;

    Transform CameraObject;
    public void Awake()
    {
        PlayerTransform = transform;
        rigidbody = GetComponent<Rigidbody>();
        CameraObject = transform.GetChild(3);
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerMovementData = new PlayerData(15f, rigidbody.linearVelocity, transform.position);
    }

    void UpdateCamera()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * RotSpeedX * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            CameraObject.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * RotSpeedY * Time.deltaTime);
        }
    }

    public void Update()
    {
        float xAxis = Input.GetAxis("Vertical");
        float yAxis = Input.GetAxis("Horizontal");
        transform.position += (transform.forward * xAxis +
         transform.right * yAxis) *
         PlayerMovementData.PlayerSpeed * Time.deltaTime;

        UpdateCamera();
    }
}
