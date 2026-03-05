using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public void Retry()
    {
        Debug.Log("Retry clicked");
        if (SceneLoader.I != null) SceneLoader.I.Load("Game");
        else SceneManager.LoadScene("Game");
    }

    public void Title()
    {
        Debug.Log("Title clicked");
        if (SceneLoader.I != null) SceneLoader.I.Load("Title");
        else SceneManager.LoadScene("Title");
    }
}