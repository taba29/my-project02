using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenItemsSceneButton : MonoBehaviour
{
    public void OpenItemsScene()
    {
        SceneManager.LoadScene("ItemScene");
    }
}