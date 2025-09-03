using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    //Player
    //PlayerData PlayerMovement = new PlayerData(15f);
    void OnEnable()
    {
        if (Manager == null)
        {
            Manager = GetComponent<GameManager>();
        }
        DontDestroyOnLoad(this);
    }
}
