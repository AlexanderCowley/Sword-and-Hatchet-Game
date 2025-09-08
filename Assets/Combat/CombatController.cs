using JetBrains.Annotations;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    void OnEnable()
    {
        
    }

    void ProcessAttack()
    {
        //Look up move in combo. Play animation, sfx, instantiate hitboxes
        //Have a function that takes in two bools so that it will look like ComboLookup(bool lAttack, bool hAttack)
    }

    void ProcessPlayerInput()
    {
        GameManager.PlayerInputStatic.lAttack = Input.GetMouseButtonDown(0);
        GameManager.PlayerInputStatic.hAttack = Input.GetMouseButtonDown(1);
        if (GameManager.PlayerInputStatic.lAttack || GameManager.PlayerInputStatic.hAttack)
        {
            ProcessAttack();
        }
        
    }

    public void Update()
    {
        ProcessPlayerInput();
    }
    
}
