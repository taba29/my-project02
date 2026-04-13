using UnityEngine;

public class PlayerSpawnSetter : MonoBehaviour
{
    void Start()
    {
        if (string.IsNullOrEmpty(MapTransitionManager.NextSpawnPointName)) return;

        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

        foreach (SpawnPoint sp in spawnPoints)
        {
            if (sp.spawnPointName == MapTransitionManager.NextSpawnPointName)
            {
                transform.position = sp.transform.position;
                MapTransitionManager.NextSpawnPointName = "";
                return;
            }
        }
    }
}