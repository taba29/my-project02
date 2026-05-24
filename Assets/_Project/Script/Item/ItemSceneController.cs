using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private GameObject targetSelectPanel;
    [SerializeField] private RectTransform targetPanelRect;
    [SerializeField] private GameObject potionButton;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text hpText;

    private void Start()
    {   
        messageText.gameObject.SetActive(false);

        if (targetSelectPanel != null)
        {
            targetSelectPanel.SetActive(false);
        }

        targetPanelRect.anchoredPosition = new Vector2(0, -800);

        UpdateItemText();
        UpdateItemText();
        UpdateHPText();
    }

    public void OpenTargetSelect()
    {
        Debug.Log("OpenTargetSelect called");

        if (InventoryManager.Instance.GetItemCount("きずぐすり") <= 0)
            return;

        targetSelectPanel.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(SlideUpPanel());
    }

    public void CloseTargetSelect()
    {
        StopAllCoroutines();
        StartCoroutine(SlideDownPanel());
    }

    public void UsePotionToMonster()
    {
        if (InventoryManager.Instance.GetItemCount("きずぐすり") <= 0)
            return;

        if (PartyState.currentHP >= PartyState.maxHP)
            {ShowMessage("つかっても こうかが なかった！");
            return;}

        PartyState.currentHP += 20;

        if (PartyState.currentHP > PartyState.maxHP)
        {
            PartyState.currentHP = PartyState.maxHP;
        }

        InventoryManager.Instance.RemoveItem("きずぐすり", 1);

        UpdateItemText();
        UpdateHPText();

        CloseTargetSelect();
    }

    public void BackToParty()
    {
        SceneManager.LoadScene("PartyScene");
    }

    private void UpdateItemText()
{
    int count = InventoryManager.Instance.GetItemCount("きずぐすり");

    if (count <= 0)
    {
        itemText.gameObject.SetActive(false);

        if (potionButton != null)
            potionButton.SetActive(false);

        return;
    }

    itemText.gameObject.SetActive(true);

    if (potionButton != null)
        potionButton.SetActive(true);

    itemText.text = "きずぐすり x " + count;
}
    private IEnumerator SlideUpPanel()
{
    float time = 0f;
    float duration = 0.2f;

    targetPanelRect.anchoredPosition = new Vector2(0, 0);

    while (time < duration)
    {
        time += Time.unscaledDeltaTime;

        float y = Mathf.Lerp(0f, 960f, time / duration);
        targetPanelRect.anchoredPosition = new Vector2(0, y);

        Debug.Log("y = " + y);

        yield return null;
    }

    targetPanelRect.anchoredPosition = new Vector2(0, 960);
}

    private IEnumerator SlideDownPanel()
    {
        Vector2 start = targetPanelRect.anchoredPosition;
        Vector2 end = new Vector2(0, -800);

        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            targetPanelRect.anchoredPosition =
                Vector2.Lerp(start, end, time / duration);

            yield return null;
        }

        targetPanelRect.anchoredPosition = end;

        targetSelectPanel.SetActive(false);
    }

    private void ShowMessage(string message)
{
    StopCoroutine(nameof(HideMessage));

    messageText.gameObject.SetActive(true);
    messageText.text = message;

    StartCoroutine(HideMessage());
}

private IEnumerator HideMessage()
{
    yield return new WaitForSecondsRealtime(2f);

    messageText.gameObject.SetActive(false);
}

private void UpdateHPText()
{
    hpText.text = "HP " + PartyState.currentHP + " / " + PartyState.maxHP;
}
}