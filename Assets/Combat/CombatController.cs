using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum AttackInput
{
    None = 0,
    LAttack = 1,
    HAttack = 2,
    LHAttack = 3
}

public enum AttackInputType
{
    None = 0,
    Press = 1,
    Hold = 2,
    Repeat = 3
}

public struct WeaponData
{
    public string Name;
    public int WeaponID;
    public HitboxData[] Hitboxes;
    public Vector3[] CurrentHitboxPositions;
    public int ComboCount;

    public ComboData[] Combos;
    //Would need to search up based on available combo input
}

public struct ComboData
{
    public AttackData[] Attacks;
    public AttackInput[] AttackInputs;
    public AttackInputType[] InputTypes;
    public bool NextInput()
    {
        return false;
    }
}

public struct AttackData
{
    public Vector3[] HitboxPositions;
    public string AnimationName;
    //SFX
}

//An attack would have to be looked up based on a set of bools (lAttack, hAttack)

public struct HitboxData
{
    public int Damage;
    public float Speed;
    public int WeaponID;
}

public class CombatController : MonoBehaviour
{
    float AttackTimerDelay = 0.25f;
    float AttackTimer = 0f;
    bool StartTimer = false;
    int MaxAttackBuffer = 5;
    public Queue<AttackInput> AttackBuffer = new Queue<AttackInput>();

    public static CombatController Instance;

    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = GetComponent<CombatController>();
        }
        DontDestroyOnLoad(this);
    }

    void ProcessAttack(AttackInput input)
    {
        //Look up move in combo. Play animation, sfx, instantiate hitboxes
        //Have a function that takes in two bools so that it will look like ComboLookup(bool lAttack, bool hAttack)

        //For the queue have an attack state that can be measured.
        if (AttackBuffer.Count >= MaxAttackBuffer)
        {
            Debug.Log("Attack Buffer Full");
            return;
        }
        AttackBuffer.Enqueue(input);
        WeaponController.WeaponControllerInstance.LookupCombo(WeaponController.CurrentWeapon, input);
    }

    public void FinishAttack()
    {
        Debug.Log("Attack finsihed");
        AttackBuffer.Dequeue();
        Debug.Log(AttackBuffer.Count);
    }

    void ProcessPlayerInput()
    {
        if (StartTimer)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= AttackTimerDelay)
            {
                AttackTimer = 0f;
                StartTimer = false;
                Debug.Log("Timer Reset");
            }
            else return;
        }

        GameManager.PlayerInputStatic.lAttack = Input.GetMouseButtonDown(0);
        GameManager.PlayerInputStatic.hAttack = Input.GetMouseButtonDown(1);
        AttackInput input;
        if (GameManager.PlayerInputStatic.lAttack || GameManager.PlayerInputStatic.hAttack)
        {
            StartTimer = true;
            //Switch-case statement
            if (GameManager.PlayerInputStatic.lAttack)
                input = AttackInput.LAttack;
            else if (GameManager.PlayerInputStatic.hAttack)
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
