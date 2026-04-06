using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    static int sceneIndex = 0;

    public static SceneController Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<SceneController>();
        }

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        GameManager.PlayerInputEnabled = true;
    }

    public static void GoToNextScene()
    {
        SceneCleanUp();
        sceneIndex = (sceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(sceneIndex);
    }

    static void SceneCleanUp()
    {
        GameManager.PlayerInputEnabled = false;
        CombatController.AttackBuffer.Clear();
        GameManager.ResetPlayer();
    }
}
