using UnityEngine;
using UnityEngine.Animations;
public class WeaponController : MonoBehaviour
{
    public static WeaponController WeaponControllerInstance;
    public static WeaponData CurrentWeapon;
    ComboData[] TestCombos = new ComboData[3];
    Animation[] WeaponAnimations;

    void Awake()
    {

        if (WeaponControllerInstance == null)
        {
            WeaponControllerInstance = GetComponent<WeaponController>();
        }
        DontDestroyOnLoad(this);
    }

    public void LookupCombo(WeaponData weapon, AttackInput attackInput)
    {
        for (int i = 0; i < weapon.Combos.Length; i++)
        {
            if (weapon.Combos[i].AttackInputs.Length <= GameManager.ComboCount)
                break;
            if (weapon.Combos[i].AttackInputs[GameManager.ComboCount] == attackInput)
            {
                GameManager.ComboCount++;
                //Reference AttackData run Animator to play clip based on ID/Name
                GameManager.PlayerAnimator.Play("LBaseAttack");
                Debug.Log($"Combo Sequence: {GameManager.ComboCount}");
                break;
            }
            else
            {
                GameManager.ComboCount = 0;
                Debug.Log("Combo Broken");
            } 
        }
    }

    void OnEnable()
    {
        TestCombos[0] = new ComboData();
        TestCombos[1] = new ComboData();
        TestCombos[2] = new ComboData();
        TestCombos[0].AttackInputs = new AttackInput[3];
        TestCombos[1].AttackInputs = new AttackInput[3];
        TestCombos[2].AttackInputs = new AttackInput[3];

        TestCombos[0].AttackInputs[0] = AttackInput.LAttack;
        TestCombos[0].AttackInputs[1] = AttackInput.LAttack;
        TestCombos[0].AttackInputs[2] = AttackInput.LAttack;

        TestCombos[1].AttackInputs[0] = AttackInput.LAttack;
        TestCombos[1].AttackInputs[1] = AttackInput.HAttack;
        TestCombos[1].AttackInputs[2] = AttackInput.LAttack;

        TestCombos[2].AttackInputs[0] = AttackInput.HAttack;
        TestCombos[2].AttackInputs[1] = AttackInput.LAttack;
        TestCombos[2].AttackInputs[2] = AttackInput.LAttack;

        CurrentWeapon = new();
        CurrentWeapon.Name = "Sword";
        CurrentWeapon.ComboCount = 0;
        CurrentWeapon.WeaponID = 0;
        CurrentWeapon.Combos = TestCombos;
        GameManager.ComboCount = 0;
    }
}
