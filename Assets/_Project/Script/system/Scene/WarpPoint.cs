using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpPoint : MonoBehaviour
{
    [Header("移動先シーン名")]
    public string targetSceneName;

    [Header("移動先で使うSpawnPoint名")]
    public string targetSpawnPointName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        MapTransitionManager.NextSpawnPointName = targetSpawnPointName;
        SceneManager.LoadScene(targetSceneName);
    }
}