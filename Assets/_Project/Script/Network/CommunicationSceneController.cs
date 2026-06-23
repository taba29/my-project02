using UnityEngine;
using UnityEngine.SceneManagement;

public class CommunicationSceneController : MonoBehaviour
{
    public void CloudLoad()
    {
        FirestoreManager.Instance.LoadPlayer("Player");
    }

    public void Back()
    {
        SceneManager.LoadScene("Map01");
    }
}