using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommunicationSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    [SerializeField] private Transform content;

[SerializeField] private GameObject playerRowPrefab;

    public void CloudLoad()
    {
        messageText.text = "読込中...";
        FirestoreManager.Instance.LoadPlayer("Player");
    }

    public void PlayerList()
{
    Debug.Log("① PlayerList開始");

    Debug.Log("② Instance = " + FirestoreManager.Instance);

    messageText.text = "Loading player list...";

   

ClearPlayerRows();



    Debug.Log("③ LoadAllPlayers呼ぶ");

    FirestoreManager.Instance.LoadAllPlayers();

    Debug.Log("④ LoadAllPlayers呼び終わり");
}

private IEnumerator PlayerListDelay()
{
    yield return new WaitForSeconds(1f);

    Debug.Log("IsInitialized = " + FirebaseInitializer.IsInitialized);

    FirestoreManager.Instance.LoadAllPlayers();
}

    public void Back()
    {
        SceneManager.LoadScene("Map01");
    }

    public void ShowMessage(string message)
{
    Debug.Log("ShowMessage = " + message);

    messageText.text = message;

    Debug.Log("TMP = " + messageText.text);
}

public void ClearPlayerRows()
{
    foreach (Transform child in content)
    {
        Destroy(child.gameObject);
    }
}

public GameObject CreatePlayerRow(PlayerData data)
{
    GameObject row = Instantiate(playerRowPrefab, content);

    PlayerRowButton rowButton =
        row.GetComponent<PlayerRowButton>();

    rowButton.Setup(data);

    return row;
}
}