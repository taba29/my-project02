using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLink : MonoBehaviour
{
    public string sceneName;

    public void Load()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning($"SceneLink: sceneName is empty on {name}");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}