using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum AttackInput
{
    None = -1,
    LAttack = 0,
    HAttack = 1
}

public enum AttackInputType
{
    None = 0,
    Press = 1,
    Hold = 2,
    Repeat = 3
}

public struct HitboxData
{
    public int Damage;

    //References Type of enemy to prevent collisions with same types of enemies or different factions??
    public int EntityID;
}

public class CombatController : MonoBehaviour
{
    //Player Input
    float AttackTimerDelay = 0.15f;
    float AttackTimer = 0f;
    bool StartTimer = false;
    int MaxAttackBuffer = 5;

    //Combo Count
    public static int ComboCount = 0;

    [Header("PLAYER WEAPONS")]
    [Space(3)]
    public WeaponData[] Weapons;

    public WeaponData CurrentPlayerWeapon;

    static HitBox[] PlayerHitboxes;
    static int HitboxCounter = 0;

    //Attack Queue
    public static Queue<AttackInput> AttackBuffer = new Queue<AttackInput>();

    public static CombatController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<CombatController>();
        }

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
        AttackBuffer.Clear();
        Debug.Log($"Attack Buffer Link: {AttackBuffer.Count}");

        CurrentPlayerWeapon = Weapons[0];
    }

    void Start()
    {
        PlayerHitboxes = GameManager.Player.WeaponHitboxes;
    }

    void ProcessAttack(AttackInput input)
    {
        //Look up move in combo. Play animation, sfx, instantiate hitboxes

        if (AttackBuffer.Count >= MaxAttackBuffer)
        {
            Debug.Log("Attack Buffer Full");
            return;
        }
        AttackBuffer.Enqueue(input);
    }

    public static void EnableNextHitbox()
    {
        if(HitboxCounter >= PlayerHitboxes.Length - 1)
        {
            Debug.LogWarning("Max Hitboxes Met");
            AttackBuffer.Clear();
            return;
        }
        
        PlayerHitboxes[HitboxCounter].gameObject.SetActive(true);
        HitboxCounter++;
    }

    public static void FinishAttack()
    {
        Debug.Log("Attack Finished");
        //Release attack state
        GameManager.Player.CombatStateMachine.CurrentAttack = null;
        GameManager.Player.CombatStateMachine.isAttacking = false;
        AttackBuffer.Dequeue();
        HitboxCounter = 0;
    }

    void ProcessPlayerInput()
    {
        if(!GameManager.PlayerInputEnabled)
        {
            StartTimer = false;
            return;
        }

        if (StartTimer)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= AttackTimerDelay)
            {
                AttackTimer = 0f;
                StartTimer = false;
                //Debug.Log("Timer Reset");
            }
            else return;
        }

        GameManager.CombatInput.lAttack = Input.GetMouseButtonDown(0);
        GameManager.CombatInput.hAttack = Input.GetMouseButtonDown(1);
        AttackInput input;
        if (GameManager.CombatInput.lAttack || GameManager.CombatInput.hAttack)
        {
            StartTimer = true;
            //Switch-case statement
            if (GameManager.CombatInput.lAttack)
                input = AttackInput.LAttack;
            else if (GameManager.CombatInput.hAttack)
                input = AttackInput.HAttack;
            else input = AttackInput.None;

            ProcessAttack(input);
        }

    }

    public void Update()
    {
        ProcessPlayerInput();
    }

}
