using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private string itemName = "きずぐすり";
    [SerializeField] private int amount = 1;
    [SerializeField] private string openedMessage = "からっぽだ。";

    private bool isOpened = false;

    public string GetMessage()
    {
        if (isOpened)
        {
            return openedMessage;
        }

        isOpened = true;

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemName, amount);
        }

        return itemName + "を " + amount + "こ 手に入れた！";
    }
}