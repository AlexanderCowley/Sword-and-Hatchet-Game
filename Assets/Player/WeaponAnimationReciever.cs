using UnityEngine;

public class WeaponAnimationReciever : MonoBehaviour
{
    //Move Hitbox to CombatController.
    [SerializeField] HitBox hitBox;
    [SerializeField] Transform hitboxSpawnpoint;
    float AttackTimer = 0f;

    float HitboxTimer = 0.5f;
    int HitboxCounter = 0;
    HitBox currentHitbox;
    public void SpawnHitbox()
    {
        Debug.Log("Spawn hitboxes");
        //Should be something like currentHitbox = CombatController.Instance.GetNextHitbox
        if(GameManager.PlayerHitboxes.Length - 1 > HitboxCounter)
        {
            Debug.LogWarning("Max Hitboxes Exceeded");
            return;
        }
        currentHitbox = GameManager.PlayerHitboxes[HitboxCounter];
        currentHitbox.gameObject.SetActive(true);
        Debug.Log(currentHitbox);
        HitboxCounter++;
    }

    public void AttackFinished()
    {
        CombatController.Instance.FinishAttack();
    }
}
