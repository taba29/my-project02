using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

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

    public void AddItem(string itemName, int amount = 1)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += amount;
        }
        else
        {
            items[itemName] = amount;
        }

        Debug.Log(itemName + " x" + items[itemName]);
    }

    public int GetItemCount(string itemName)
    {
        if (items.ContainsKey(itemName))
            return items[itemName];

        return 0;
    }

    public Dictionary<string, int> GetAllItems()
    {
        return items;
    }
}