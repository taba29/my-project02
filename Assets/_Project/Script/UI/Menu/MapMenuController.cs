using UnityEngine;

public class MapMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private GameObject itemsPanel;

    [Header("Player")]
    [SerializeField] private PlayerMover playerMover;

    private bool isMenuOpen = false;

    void Start()
    {
        CloseAllPanels();
    }

    public void OnSetButton()
    {
        if (isMenuOpen)
        {
            CloseAllPanels();
        }
        else
        {
            OpenMainMenu();
        }
    }

    public void OnAButton()
    {
        if (!isMenuOpen)
        {
            Debug.Log("A: マップ中の決定・調べる");
            return;
        }

        Debug.Log("A: メニュー決定");
    }

    public void OnBButton()
    {
        if (!isMenuOpen)
        {
            Debug.Log("B: マップ中キャンセル");
            return;
        }

        CloseAllPanels();
    }

    public void OpenMainMenu()
    {
        isMenuOpen = true;

        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (statusPanel != null) statusPanel.SetActive(false);
        if (itemsPanel != null) itemsPanel.SetActive(false);

        if (playerMover != null) playerMover.enabled = false;
    }

    public void OpenStatus()
    {
        isMenuOpen = true;

        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(true);
        if (itemsPanel != null) itemsPanel.SetActive(false);

        if (playerMover != null) playerMover.enabled = false;
    }

    public void OpenItems()
    {
        isMenuOpen = true;

        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(false);
        if (itemsPanel != null) itemsPanel.SetActive(true);

        if (playerMover != null) playerMover.enabled = false;
    }

    public void CloseAllPanels()
    {
        isMenuOpen = false;

        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(false);
        if (itemsPanel != null) itemsPanel.SetActive(false);

        if (playerMover != null) playerMover.enabled = true;
    }
}