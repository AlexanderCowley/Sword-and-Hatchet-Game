using UnityEngine;

public class HitBox : MonoBehaviour
{
    public AttackData HitboxData;
    public int Damage = 0;
    public int StunDamage = 0;
    bool isParry = false;

    bool StartTimer = false;
    float AttackTimer = 0;
    float MaxTimer = 0.5f;

    void OnEnable()
    {
        if(HitboxData == null)
        {
            return;
        }
        Damage = HitboxData.Damage;
        StunDamage = HitboxData.StunDamage;
        StartTimer = true;
    }

    void HitboxTimer()
    {
        if(StartTimer)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= MaxTimer)
            {
                AttackTimer = 0f;
                StartTimer = false;
                Debug.Log("Hit Box -> Released");
                gameObject.SetActive(false);
            }
            else return;
        }
    }

    public void AssignHitboxInfo(AttackData attackData)
    {
        HitboxData = attackData;
        Damage = HitboxData.Damage;
        StunDamage = HitboxData.StunDamage;
    }

    void OnDisable()
    {
        HitboxData = null;
        Damage = 0;
        StunDamage = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Hurtbox>() != null)
        {
            Debug.Log("Made contact");
        }
    }

    void Update()
    {
        HitboxTimer();
    }
}
