using UnityEngine;

public class ReportSceneController : MonoBehaviour
{
    public void SaveGame()
    {
        SaveManager.Save();
    }
}