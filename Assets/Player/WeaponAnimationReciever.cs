using UnityEngine;

public class WeaponAnimationReciever : MonoBehaviour
{
    public void SpawnHitbox()
    {
        CombatController.EnableNextHitbox();
    }

    public void AttackFinished()
    {
        Debug.Log("Attack Finished");
        CombatController.FinishAttack();
    }
}
