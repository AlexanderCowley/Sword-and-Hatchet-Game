using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    public static PlayerSystem Player;

    public static Animator PlayerAnimator;
    public static int ComboCount;

    //Player
    //public static PlayerData PlayerMovement = new PlayerData(15f);
    public static PlayerInput CombatInput = new();
    void OnEnable()
    {
        if (Manager == null)
        {
            Manager = GetComponent<GameManager>();
        }
        DontDestroyOnLoad(this);

        //Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player = FindAnyObjectByType<PlayerSystem>();
        PlayerAnimator = Player.transform.GetChild(5).GetComponent<Animator>();
    }

    void Update()
    {
        //AnimResetToIdle();
    }

    public int GenerateEntityID(int lastID = 0)
    {
        return lastID++;
    }


}
