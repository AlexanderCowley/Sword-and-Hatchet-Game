using System;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EntityID = -1;
    [NonSerialized] public Hurtbox EnemyHurtbox;

    void Awake()
    {
        EnemyHurtbox = GetComponent<Hurtbox>();
    }

    void Start()
    {
        //Called multiple times
        EnemyManager.Instance.AddEnemy(this);
    }
}
