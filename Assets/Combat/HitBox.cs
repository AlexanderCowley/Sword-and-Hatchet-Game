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
        //Potentially what can happen is that AttackData can be null when it is enabled.
        if(HitboxData == null)
        {
            Debug.LogWarning("Hitbox Data is Null");
            gameObject.SetActive(false);
            return;
        }
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
            if(HitboxData == null)
            {
                Debug.Log("No Attack Data!");
            }
            Debug.Log("Made contact");
        }
    }

    void Update()
    {
        HitboxTimer();
    }
}
