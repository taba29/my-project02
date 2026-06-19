using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
{
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
}

    public void AddItem(string itemName, int amount)
    {
        if (!items.ContainsKey(itemName))
        {
            items[itemName] = 0;
        }

        items[itemName] += amount;
    }

    public int GetItemCount(string itemName)
    {
        if (!items.ContainsKey(itemName))
        {
            return 0;
        }

        return items[itemName];
    }

    public void RemoveItem(string itemName, int amount)
    {
        if (!items.ContainsKey(itemName)) return;

        items[itemName] -= amount;

        if (items[itemName] <= 0)
        {
            items.Remove(itemName);
        }
    }

    public Dictionary<string, int> GetAllItems()
{
    return items;
}

public void ClearItems()
{
    items.Clear();
}
}