using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleSceneController : MonoBehaviour
{

    [SerializeField] private GameObject continueButton;


    private void Start()
{
    continueButton.SetActive(SaveManager.HasSave());
}

    public void ContinueGame()
{
    if (SaveManager.Load())
    {
        SceneManager.LoadScene(SaveManager.LoadedSceneName);
    }
    else
    {
        Debug.Log("セーブデータがありません");
    }
}

    

    public void NewGame()
{
    GameInitializer.NewGame();
    SceneManager.LoadScene("Map01");
}

public void DebugResetSave()
{
    SaveManager.Delete();
    GameInitializer.NewGame();

    Debug.Log("開発用：セーブ削除＋初期化しました");
}


}