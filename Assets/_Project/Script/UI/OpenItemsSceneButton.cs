using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenItemsSceneButton : MonoBehaviour
{
    public void OpenItemsScene()
    {
        Debug.Log("OPEN ITEM SCENE");

        SceneManager.LoadScene("ItemScene");
    }
}