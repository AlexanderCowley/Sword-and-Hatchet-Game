using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    int index = 0;

    void GoToNextScene()
    {
        index++;
        if (index % SceneManager.sceneCount != 0)
            return;
        SceneManager.LoadScene(index++);
    }
}
