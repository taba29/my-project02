using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLink : MonoBehaviour
{
    public string sceneName;

    private void SaveMapPosition()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");

    if (player == null)
        return;

    MapReturnState.returnPosition = player.transform.position;
    MapReturnState.hasReturnPosition = true;
    MapReturnState.returnSceneName =
    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
}

    public void Load()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning($"SceneLink: sceneName is empty on {name}");
            return;
        }

        SaveMapPosition();
        
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToMap()
{
    MapReturnState.hasReturnPosition = true;

    if (string.IsNullOrEmpty(MapReturnState.returnSceneName))
    {
        SceneManager.LoadScene("Map01");
        return;
    }

    SceneManager.LoadScene(MapReturnState.returnSceneName);
}
}