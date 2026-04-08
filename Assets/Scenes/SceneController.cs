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
        //GameManager.SceneInit();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneInit;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneInit;
    }

    public static void GoToNextScene()
    {
        GameManager.PlayerInputEnabled = false;
        CombatController.SceneCleanup();
        sceneIndex = (sceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(sceneIndex);
    }

    void SceneInit(Scene scene, LoadSceneMode loadSceneMode)
    {
        CombatController.SceneInit();
        GameManager.ResetPlayer();
        GameManager.SceneInit();
        GameManager.PlayerInputEnabled = true;
    }
}
