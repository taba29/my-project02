using UnityEngine;

public class StageRunner : MonoBehaviour
{
    StageDefinition stage;
    float timer;

    void Start()
    {
        stage = StageSelector.I ? StageSelector.I.SelectedStage : null;

        if (stage == null)
        {
            Debug.LogError("Stage not selected!");
            return;
        }

        GameManager.I.SetState(GameState.Playing);

        timer = stage.timeLimit;

        SpawnEnemies();
    }

    void Update()
    {
        if (GameManager.I.State != GameState.Playing) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            EndStage();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < stage.enemyCount; i++)
        {
            Instantiate(stage.enemyPrefab,
                        Random.insideUnitCircle * 4f,
                        Quaternion.identity);
        }
    }

    void EndStage()
    {
        GameManager.I.SetState(GameState.Result);
        SceneLoader.I.Load("Result");
    }
}