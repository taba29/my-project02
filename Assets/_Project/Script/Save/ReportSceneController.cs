using UnityEngine;

public class ReportSceneController : MonoBehaviour
{
    public void SaveGame()
    {
        Debug.Log("SaveGame button pressed");

        // ローカル保存
        SaveManager.Save();

        Debug.Log("FirestoreManager.Instance = " + FirestoreManager.Instance);

        if (FirestoreManager.Instance != null)
        {
            Debug.Log("Firestore保存開始");
            FirestoreManager.Instance.SaveCurrentPlayer();
        }
        else
        {
            Debug.LogError("FirestoreManager.Instance が NULL");
        }
    }
}