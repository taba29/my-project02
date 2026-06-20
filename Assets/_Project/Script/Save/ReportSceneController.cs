using UnityEngine;

public class ReportSceneController : MonoBehaviour
{
    public void SaveGame()
    {
        Debug.Log("SaveGame button pressed");
        SaveManager.Save();
    }
}