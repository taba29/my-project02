using UnityEngine;

public class StageButton : MonoBehaviour
{
    [SerializeField] StageDefinition stage;

    public void OnClick()
    {
        StageSelector.I.SelectStage(stage);
        SceneLoader.I.Load("Game");
    }
}