using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMapButton : MonoBehaviour
{
    public void BackToMap()
    {
        SceneManager.LoadScene("Map01");
    }
}