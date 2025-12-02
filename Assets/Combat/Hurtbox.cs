using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public int EntityID = -1;

    [SerializeField] EnemyStats enemyStats;
    Transform transInstance;
    int Health = 0;
    int Stun = 0;
    bool hasArmor = false;
    int Weakness = 0;

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
        HitBox hitBox = null;
        if(other.TryGetComponent<HitBox>(out hitBox))
        {
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
}
