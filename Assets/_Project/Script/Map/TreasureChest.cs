using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private string itemName = "きずぐすり";
    [SerializeField] private int amount = 1;
    [SerializeField] private string openedMessage = "からっぽだ。";
    [SerializeField] private string chestId;

    private bool isOpened = false;

    private void Start()
    {
        isOpened = OpenedChestState.openedChestIds.Contains(chestId);
    }

    public string GetMessage()
    {
        if (isOpened)
        {
            return openedMessage;
        }

        isOpened = true;
        OpenedChestState.openedChestIds.Add(chestId);

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemName, amount);
        }

        return itemName + "を " + amount + "こ 手に入れた！";
    }
}