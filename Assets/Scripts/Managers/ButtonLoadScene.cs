using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public string sceneToLoad;
    public void Click()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
