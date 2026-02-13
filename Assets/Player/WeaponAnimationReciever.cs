using UnityEngine;

public class WeaponAnimationReciever : MonoBehaviour
{
    //Move Hitbox to CombatController.
    [SerializeField] HitBox hitBox;
    [SerializeField] Transform hitboxSpawnpoint;
    int HitboxCounter = 0;
    HitBox currentHitbox;
    public void SpawnHitbox()
    {
        //The problem is here!
        //Should be something like currentHitbox = CombatController.Instance.GetNextHitbox
        if(GameManager.PlayerHitboxes.Length - 1 <= HitboxCounter)
        {
            Debug.LogWarning("Max Hitboxes Met");
            return;
        }
        currentHitbox = GameManager.PlayerHitboxes[HitboxCounter];
        currentHitbox.gameObject.SetActive(true);
        HitboxCounter++;
    }

    public void AttackFinished()
    {
        CombatController.Instance.FinishAttack();
        HitboxCounter = 0;
    }
}
