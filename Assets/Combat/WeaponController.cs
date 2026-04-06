using UnityEngine;
using UnityEngine.Animations;
public class WeaponController : MonoBehaviour
{
    public static WeaponController WeaponControllerInstance;
    public static WeaponData CurrentWeapon;
    Animation[] WeaponAnimations;

    void Awake()
    {

        if (WeaponControllerInstance == null)
        {
            WeaponControllerInstance = GetComponent<WeaponController>();
        }

        if(WeaponControllerInstance != null && WeaponControllerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void LookupCombo(WeaponData weapon, AttackInput attackInput)
    {
        CombatController.ComboCount++;
        //Reference AttackData run Animator to play clip based on ID/Name
        //GameManager.PlayerAnimator.Play("LBaseAttack");
    }

    void OnEnable()
    {
        CombatController.ComboCount = 0;
    }
}
