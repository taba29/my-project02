using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private GameObject targetSelectPanel;
    [SerializeField] private RectTransform targetPanelRect;

    private void Start()
    {
        if (targetSelectPanel != null)
        {
            targetSelectPanel.SetActive(false);
        }

        targetPanelRect.anchoredPosition = new Vector2(0, -800);

        UpdateItemText();
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
            return;

        PartyState.currentHP += 20;

        if (PartyState.currentHP > PartyState.maxHP)
        {
            PartyState.currentHP = PartyState.maxHP;
        }

        InventoryManager.Instance.RemoveItem("きずぐすり", 1);

        UpdateItemText();

        CloseTargetSelect();
    }

    public void BackToParty()
    {
        SceneManager.LoadScene("PartyScene");
    }

    private void UpdateItemText()
    {
        itemText.text =
            "きずぐすり x " +
            InventoryManager.Instance.GetItemCount("きずぐすり");
    }

    private IEnumerator SlideUpPanel()
{
    float time = 0f;
    float duration = 2f;

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
            time += Time.deltaTime;

            targetPanelRect.anchoredPosition =
                Vector2.Lerp(start, end, time / duration);

            yield return null;
        }

        targetPanelRect.anchoredPosition = end;

        targetSelectPanel.SetActive(false);
    }
}