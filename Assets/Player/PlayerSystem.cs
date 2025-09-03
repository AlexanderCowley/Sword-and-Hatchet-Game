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
    public void Awake()
    {
        PlayerTransform = transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        PlayerMovementData = new PlayerData(15f, rigidbody.linearVelocity, transform.position);
    }

    void UpdateCamera()
    {
        //
    }

    public void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) *
         PlayerMovementData.PlayerSpeed * Time.deltaTime;

        
        UpdateCamera();
    }
}
