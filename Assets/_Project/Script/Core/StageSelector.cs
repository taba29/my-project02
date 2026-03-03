using UnityEngine;

public class StageSelector : MonoBehaviour
{
    public static StageSelector I { get; private set; }

    public StageDefinition SelectedStage { get; private set; }

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectStage(StageDefinition stage)
    {
        SelectedStage = stage;
    }
}