using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private string itemName = "きずぐすり";
    [SerializeField] private string openedMessage = "からっぽだ。";

    private bool isOpened = false;

    public string GetMessage()
    {
        if (isOpened)
        {
            return openedMessage;
        }

        isOpened = true;
        return itemName + "を 手に入れた！";
    }
}