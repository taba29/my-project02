using UnityEngine;

public class ReportSceneController : MonoBehaviour
{
    public void SaveGame()
{
    Debug.Log("SaveGame button pressed");

    // ローカル保存
    SaveManager.Save();

    // クラウド保存
    if (FirestoreManager.Instance != null)
    {
        FirestoreManager.Instance.SaveCurrentPlayer();
    }
    else
    {
        Debug.LogWarning("FirestoreManager.Instance がありません");
    }
}
}