using UnityEngine;

public class WeaponAnimationReciever : MonoBehaviour
{
    public void AttackFinished()
    {
        CombatController.Instance.FinishAttack();
    }
}
