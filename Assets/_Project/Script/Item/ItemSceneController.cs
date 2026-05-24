using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private GameObject targetSelectPanel;

    private void Start()
    {
        if (targetSelectPanel != null)
        {
            targetSelectPanel.SetActive(false);
        }

        UpdateItemText();
    }

    public void OpenTargetSelect()
    {
        if (InventoryManager.Instance.GetItemCount("きずぐすり") <= 0)
            return;

        targetSelectPanel.SetActive(true);
    }

    public void CloseTargetSelect()
    {
        targetSelectPanel.SetActive(false);
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
}