using UnityEngine;

[CreateAssetMenu(menuName = "Game/StageDefinition")]
public class StageDefinition : ScriptableObject
{
    public string stageName;

    [Header("Rules")]
    public float timeLimit = 30f;
    public int enemyCount = 10;

    [Header("Prefabs")]
    public GameObject enemyPrefab;
}