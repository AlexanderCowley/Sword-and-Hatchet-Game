using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public string Name = null;
    public int Damage = 0;
    public int StunDamage = 0;
    
    public string AnimationName = null;
    
    public Vector3[] HitboxPositions;

    //Will not be larger than the amount of inputs there are. Mapped to either
    public AttackData[] nextAttacks;
    //Effects like knockback, etc.
    //VFX, SFX
    
}
