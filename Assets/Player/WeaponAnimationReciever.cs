using UnityEngine;

public class WeaponAnimationReciever : MonoBehaviour
{
    //Move Hitbox to CombatController.
    [SerializeField] HitBox hitBox;
    [SerializeField] Transform hitboxSpawnpoint;
    float AttackTimer = 0f;

    float HitboxTimer = 0.5f;

    bool StartTimer = false;
    HitBox currentHitbox;
    public void SpawnHitbox()
    {
        //Might need to make each attack be a state with a separate hitbox pos, Rotation should use Quaternion.identity
        //Should be something like currentHitbox = CombatController.Instance.GetNextHitbox -> Align position maybe?
        currentHitbox = (HitBox)Instantiate(hitBox, transform.parent.position + hitboxSpawnpoint.localPosition, 
         Quaternion.identity, transform);
        StartTimer = true;
    }

    public void AttackFinished()
    {
        CombatController.Instance.FinishAttack();
    }

    void TempTimerForHitboxes()
    {
        if (StartTimer)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= HitboxTimer)
            {
                AttackTimer = 0f;
                StartTimer = false;
                currentHitbox.gameObject.SetActive(false);
                Debug.Log("Hit Box -> Released");
            }
            else return;
        }
    }

    void Update()
    {
        TempTimerForHitboxes();
    }
}
