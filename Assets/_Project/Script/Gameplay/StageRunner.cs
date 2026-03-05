using UnityEngine;
using UnityEngine.SceneManagement;

public class StageRunner : MonoBehaviour
{
    [SerializeField] private StageDefinition defaultStage;

    private StageDefinition stage;
    private float timer;

    void Start()
    {
        stage = (StageSelector.I != null && StageSelector.I.SelectedStage != null)
            ? StageSelector.I.SelectedStage
            : defaultStage;

        if (stage == null)
        {
            Debug.LogError("Stage not selected! (Select in Title or set defaultStage on StageRunner)");
            return;
        }

        if (GameManager.I != null) GameManager.I.SetState(GameState.Playing);

        timer = stage.timeLimit;
        SpawnEnemies();
    }

    void Update()
    {
        if (GameManager.I != null && GameManager.I.State != GameState.Playing) return;

        timer -= Time.deltaTime;
        if (timer <= 0f) EndStage();
    }

    void SpawnEnemies()
    {
        if (stage.enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is null in StageDefinition");
            return;
        }

        for (int i = 0; i < stage.enemyCount; i++)
            Instantiate(stage.enemyPrefab, Random.insideUnitCircle * 4f, Quaternion.identity);
    }

    void EndStage()
    {
        if (GameManager.I != null) GameManager.I.SetState(GameState.Result);

        if (SceneLoader.I != null) SceneLoader.I.Load("Result");
        else SceneManager.LoadScene("Result");
    }
}