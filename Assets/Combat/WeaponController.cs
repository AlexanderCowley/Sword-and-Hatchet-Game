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
}
