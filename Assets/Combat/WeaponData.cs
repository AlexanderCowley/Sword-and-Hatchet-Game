using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public int WeaponID;

    //Really I just need the starting attacks for each possible input
    public AttackData[] StartingAttacks;
}
