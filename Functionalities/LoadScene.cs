using UnityEngine;
using UnityEngine.SceneManagement;

/**
This script uses the SceneManager to load scenes.
*/
public class LoadScene : MonoBehaviour
{
    public void Load(string sceneToBeLoaded)
    {
        SceneManager.LoadScene(sceneToBeLoaded);
    }
}



