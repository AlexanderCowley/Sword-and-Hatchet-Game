using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int MaxHealth = 0;
    public int MaxStunHealth = 0;
    public int DefaultWeakness = 0;
}
