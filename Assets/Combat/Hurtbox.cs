using UnityEngine;
using System.Collections.Generic;
public class Hurtbox : MonoBehaviour
{
    public int EntityID = -1;

    [SerializeField] EnemyStats enemyStats;
    Transform transInstance;
    int Health = 0;
    int Stun = 0;
    bool hasArmor = false;
    int Weakness = 0;

    bool StartTimer = false;

    float AttackTimer = 0f;

    float MaxTimer = 0.15f;

    Queue<int> ActiveAttackIDs = new Queue<int>();

    void Awake()
    {
        Health = enemyStats.MaxHealth;
        Stun = enemyStats.MaxStunHealth;
        Weakness = enemyStats.DefaultWeakness;
    }

    void Start()
    {
        transInstance = transform;
    }

    void OnTriggerEnter(Collider other)
    {
        //Take Damage
        if(other.TryGetComponent<HitBox>(out HitBox hitBox))
        {
            if(ActiveAttackIDs.Contains(hitBox.AttackID))
            {
                StartTimer = true;
                return;
            }
            //Set up timer and queue
            ActiveAttackIDs.Enqueue(hitBox.AttackID);
            StartTimer = true;
            Health -= hitBox.Damage;
            Debug.Log($"{transInstance.name} Hitbox -> Health: {Health}", gameObject);

            //Enemy Death
            if(Health <= 0)
            {
                Debug.Log($"{transInstance.name} Hitbox -> Death", gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    void HitboxTimer()
    {
        if(StartTimer)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= MaxTimer)
            {
                AttackTimer = 0f;
                //Seems redundant but I plan to factor this function out.
                //Setting StartTimer twice might be the end result of that anyway
                StartTimer = false;
                //Keeps timer going afterwards.
                ActiveAttackIDs.Dequeue();
                if(ActiveAttackIDs.Count > 0)
                {
                    StartTimer = true;
                }
            }
            else return;
        }
    }

    void Update()
    {
        HitboxTimer();
    }
}
