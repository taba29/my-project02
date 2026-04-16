using UnityEngine;

public class MapMenuSwitcher : MonoBehaviour
{
    [Header("マップ操作用")]
    [SerializeField] private GameObject mapTouchUI;     // 下の Canvas/TouchUI

    [Header("メニュー操作用")]
    [SerializeField] private GameObject menuTouchUI;    // 上の Canvas(1) 側の TouchUI
    [SerializeField] private GameObject menuPanelRoot;  // pausePanel か MainMenuPanel

    [Header("PauseMenu")]
    [SerializeField] private PauseMenu pauseMenu;       // PauseUIRoot の PauseMenu

    private bool isMenuOpen = false;

    void Awake()
{
    CloseMenuImmediate();
}

    public void ToggleMenu()
    {
        if (isMenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    public void OpenMenu()
    {
        isMenuOpen = true;

        if (mapTouchUI != null)
            mapTouchUI.SetActive(false);

        if (menuTouchUI != null)
            menuTouchUI.SetActive(true);

       
       

        // ここが大事
        if (pauseMenu != null)
        {
            pauseMenu.SetPaused(true);
        }
        else if (menuPanelRoot != null)
        {
            menuPanelRoot.SetActive(true);
        }

        Debug.Log("Menu Open");
    }

    public void CloseMenu()
    {
        isMenuOpen = false;

        if (mapTouchUI != null)
            mapTouchUI.SetActive(true);

        if (menuTouchUI != null)
            menuTouchUI.SetActive(false);

        // ここが大事
        if (pauseMenu != null)
        {
            pauseMenu.SetPaused(false);
        }
        else if (menuPanelRoot != null)
        {
            menuPanelRoot.SetActive(false);
        }

        Debug.Log("Menu Close");
    }

    public void CloseMenuFromPauseMenu()
    {
        CloseMenu();
    }

    private void CloseMenuImmediate()
    {
        isMenuOpen = false;

        if (mapTouchUI != null)
            mapTouchUI.SetActive(true);

        if (menuTouchUI != null)
            menuTouchUI.SetActive(false);

        if (pauseMenu != null)
            pauseMenu.SetPaused(false);
        else if (menuPanelRoot != null)
            menuPanelRoot.SetActive(false);
    }
}