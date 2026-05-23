using TMPro;
using UnityEngine;

public class ItemSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text itemText;

    private void Start()
    {
        UpdateItemText();
    }

    public void UsePotion()
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
    }

    private void UpdateItemText()
{
    itemText.text =
        "きずぐすり x " +
        InventoryManager.Instance.GetItemCount("きずぐすり");
}
}