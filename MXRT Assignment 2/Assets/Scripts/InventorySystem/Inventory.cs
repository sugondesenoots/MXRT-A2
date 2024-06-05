using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemName)
    {
        Item existingItem = items.Find(i => i.name == itemName);

        if (existingItem != null)
        {
            existingItem.count += 1;
        }
        else
        {
            items.Add(new Item(itemName, 1));
        }

        Debug.Log("1 " + itemName + " added to inventory");
    }

    public void RemoveItem(string itemName)
    {
        Item existingItem = items.Find(i => i.name == itemName);

        if (existingItem != null)
        {
            existingItem.count -= 1;

            if (existingItem.count <= 0)
            {
                items.Remove(existingItem);
            }

            Debug.Log("1 " + itemName + " removed from inventory");
        }
    }
}
