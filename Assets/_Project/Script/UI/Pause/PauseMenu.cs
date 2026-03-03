using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public MenuStickNavigator navigator; // ★追加
    public GameObject pausePanel;

    [Header("Tab Panels")]
    public GameObject panelParty;
    public GameObject panelGear;
    public GameObject panelQuest;
    public GameObject panelAchieve;
    public GameObject panelReport;

    [Header("First Selected")]
    public GameObject firstSelected;

    [Header("B Button (optional)")]
    //public PressButton btnB;

    bool paused;

    void Start()
    {
        SetPaused(false);
    }

    void Update()
    {
        if (!paused) return;

        bool bDown = Input.GetKeyDown(KeyCode.Escape);
        //if (btnB != null) bDown |= btnB.Down;

        if (bDown)
        {
            if (IsAnySubPanelOpen())
                CloseSubPanels();
            else
                SetPaused(false);
        }
    }

    public void Toggle() => SetPaused(!paused);

    public void SetPaused(bool on)
    {
        paused = on;

        if (pausePanel != null)
            pausePanel.SetActive(on);

        Time.timeScale = on ? 0f : 1f;

        if (on)
        {
        ShowParty();

        // ★初回だけfirstSelected、それ以降はnavigatorに任せる
        if (navigator != null)
        navigator.RestoreSelectionOrFirst(firstSelected);
        else if (firstSelected != null && EventSystem.current != null)
        EventSystem.current.SetSelectedGameObject(firstSelected);
        }
    }

    public void OnClickResume() => SetPaused(false);

    // ---- Tabs ----
    void HideAll()
    {
        if (panelParty) panelParty.SetActive(false);
        if (panelGear) panelGear.SetActive(false);
        if (panelQuest) panelQuest.SetActive(false);
        if (panelAchieve) panelAchieve.SetActive(false);
        if (panelReport) panelReport.SetActive(false);
    }

    bool IsAnySubPanelOpen()
    {
        return (panelParty && panelParty.activeSelf) ||
               (panelGear && panelGear.activeSelf) ||
               (panelQuest && panelQuest.activeSelf) ||
               (panelAchieve && panelAchieve.activeSelf) ||
               (panelReport && panelReport.activeSelf);
    }

    void CloseSubPanels() => HideAll();

    public void ShowParty()   { HideAll(); if (panelParty) panelParty.SetActive(true); }
    public void ShowGear()    { HideAll(); if (panelGear) panelGear.SetActive(true); }
    public void ShowQuest()   { HideAll(); if (panelQuest) panelQuest.SetActive(true); }
    public void ShowAchieve() { HideAll(); if (panelAchieve) panelAchieve.SetActive(true); }
    public void ShowReport()  { HideAll(); if (panelReport) panelReport.SetActive(true); }

public void PreviewBySelected(GameObject selected)
{
    if (selected == null) return;

    // 名前で判定（最短）。慣れたらDictionaryでもOK
    if (selected.name.Contains("Party")) ShowParty();
    else if (selected.name.Contains("Gear")) ShowGear();
    else if (selected.name.Contains("Quest")) ShowQuest();
    else if (selected.name.Contains("Achieve")) ShowAchieve();
    else if (selected.name.Contains("Report")) ShowReport();
}

}
