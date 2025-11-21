using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public int EntityID = -1;
    int Health = 0;
    int Stun = 0;
    bool hasArmor = false;
    int Weakness = 0;

    void OnTriggerEnter(Collider other)
    {
        
    }

}
