using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    public static Animator PlayerAnimator;
    public static int ComboCount;

    //Player
    //public static PlayerData PlayerMovement = new PlayerData(15f);
    public static PlayerInput PlayerInputStatic = new();
    public void AnimResetToIdle()
    {
        if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            PlayerAnimator.Play("Idle");
        }
    }
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
        PlayerAnimator = FindAnyObjectByType<PlayerSystem>().transform.GetChild(5).GetComponent<Animator>();
    }

    void Update()
    {
        //AnimResetToIdle();
    }


}
