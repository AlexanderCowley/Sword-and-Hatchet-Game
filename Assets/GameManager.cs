using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    public static PlayerSystem Player;
    public static HitBox[] PlayerHitboxes;

    public static Animator PlayerAnimator;
    public static int ComboCount;
    static Stack<int> ActiveAttackIDs = new Stack<int>();
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

    void Start()
    {
        PlayerHitboxes = Player.WeaponHitboxes;
    }

    public static int GenerateAttackIDs(int modifier = 1, bool isPlayer = false)
    {
        int result;
        if(isPlayer)
        {
            result = 0;
        }
        else
        {
            result = 100 * modifier;
        }

        while(ActiveAttackIDs.Contains(result))
        {
            result++;
        }

        ActiveAttackIDs.Push(result);
        return result;
    }

    public int GenerateEntityID(int lastID = 0)
    {
        return lastID++;
    }


}
