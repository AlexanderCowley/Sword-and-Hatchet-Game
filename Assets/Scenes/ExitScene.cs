using UnityEngine;

public class ExitScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerSystem>())
        {
            SceneController.GoToNextScene();
        }
    }
}
