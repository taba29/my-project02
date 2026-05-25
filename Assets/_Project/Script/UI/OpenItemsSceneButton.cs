using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenItemsSceneButton : MonoBehaviour
{
    private void SaveMapPosition()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");

    if (player == null)
        return;

    MapReturnState.returnPosition = player.transform.position;
    MapReturnState.hasReturnPosition = true;
}

    public void OpenItemsScene()
{
    Debug.Log("OPEN ITEM SCENE");

    SaveMapPosition();

    SceneManager.LoadScene("ItemScene");
}
}