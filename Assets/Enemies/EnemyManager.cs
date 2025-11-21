using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public int EnemyCounter = 0;
    public Enemy[] Enemies = new Enemy[20];

    public void AddEnemy(Enemy newEnemy)
    {
        Enemies[EnemyCounter] = newEnemy;
        newEnemy.EntityID = GameManager.Manager.GenerateEntityID(EnemyCounter);
        EnemyCounter++;
    }

    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = GetComponent<EnemyManager>();
        }
        DontDestroyOnLoad(this);
    }

    void SpawnEnemies()
    {
        
    }
}
