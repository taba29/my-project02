using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommunicationSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    public void CloudLoad()
    {
        messageText.text = "読込中...";
        FirestoreManager.Instance.LoadPlayer("Player");
    }

    public void PlayerList()
    {
        messageText.text = "プレイヤー一覧取得中...";
        FirestoreManager.Instance.LoadAllPlayers();
    }

    public void Back()
    {
        SceneManager.LoadScene("Map01");
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }
}