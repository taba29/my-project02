using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryDebugViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;

    void Start()
    {
    Refresh();
    }

    public void Refresh()
    {
        if (itemText == null) return;
        if (InventoryManager.Instance == null)
        {
            itemText.text = "";
            return;
        }

        Dictionary<string, int> items = InventoryManager.Instance.GetAllItems();

        if (items.Count == 0)
        {
            itemText.text = "どうぐは ありません";
            return;
        }

        itemText.text = "";

        foreach (var item in items)
        {
            itemText.text += item.Key + " x" + item.Value + "\n";
        }
    }
}