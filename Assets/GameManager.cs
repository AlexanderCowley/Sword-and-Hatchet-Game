using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    //Player
    //public static PlayerData PlayerMovement = new PlayerData(15f);
    public static PlayerInput PlayerInputStatic = new();
    void OnEnable()
    {
        if (Manager == null)
        {
            Manager = GetComponent<GameManager>();
        }
        DontDestroyOnLoad(this);
    }
}
