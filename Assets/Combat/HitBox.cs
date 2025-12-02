using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int Damage = 0;
    int Stun = 0;
    bool isParry = false;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Hurtbox>() != null)
        {
            Debug.Log("Made contact");
        }
    }
}
