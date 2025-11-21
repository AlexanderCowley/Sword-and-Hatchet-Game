using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EntityID = -1;
    public Hurtbox EnemyHurtbox;

    void Awake()
    {
        EnemyManager.Instance.AddEnemy(this);
        EnemyHurtbox = GetComponent<Hurtbox>();
    }
}
