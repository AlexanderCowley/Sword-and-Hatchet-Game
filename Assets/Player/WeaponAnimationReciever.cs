using UnityEngine;

public class WeaponAnimationReciever : MonoBehaviour
{
    public void SpawnHitbox()
    {
        CombatController.EnableNextHitbox();
    }

    public void AttackFinished()
    {
        CombatController.FinishAttack();
    }
}
