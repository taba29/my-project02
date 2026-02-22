using TMPro;
using UnityEngine;

public class MenuStateUI : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject statusPanel;
    public GameObject itemsPanel;

    public TMP_Text statusText;
    public TMP_Text itemsText;

    enum State { Main, Status, Items }
    State state = State.Main;

    void Start()
    {
        // 初期データ例（後でセーブにもできる）
        if (statusText) statusText.text = "Lv: 3\nHP: 12/12\nATK: 5\nDEF: 4";
        if (itemsText) itemsText.text = "Potion x2\nWater x1\nSnack x3";

        GoMain();
    }

    public void GoMain()
    {   Debug.Log("GoMain");     state = State.Main;  SetPanels(true,false,false);
        state = State.Main;
        SetPanels(main: true, status: false, items: false);
    }

    public void OpenStatus()
    {   Debug.Log("OpenStatus"); state = State.Status;SetPanels(false,true,false);
        state = State.Status;
        SetPanels(main: false, status: true, items: false);
    }

    public void OpenItems()
    {   Debug.Log("OpenItems");  state = State.Items; SetPanels(false,false,true);
        state = State.Items;
        SetPanels(main: false, status: false, items: true);
    }

    public void Back()
    {   Debug.Log("Back");       GoMain();
        // どの画面からでもメインに戻る
        GoMain();
    }

    void Update()
    {
        // Androidの戻る（Escape）対応の最小
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == State.Main) Application.Quit();
            else Back();
        }
    }

    void SetPanels(bool main, bool status, bool items)
    {
        if (mainMenuPanel) mainMenuPanel.SetActive(main);
        if (statusPanel) statusPanel.SetActive(status);
        if (itemsPanel) itemsPanel.SetActive(items);
    }
}
