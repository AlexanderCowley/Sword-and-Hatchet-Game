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
        DontDestroyOnLoad(this);
    }

    public static void GoToNextScene()
    {
        sceneIndex = (sceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(sceneIndex);
    }
}
