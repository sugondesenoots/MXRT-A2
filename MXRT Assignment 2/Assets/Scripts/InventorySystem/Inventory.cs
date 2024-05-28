using System.Collections;
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

    public void AddItem(Item itemToAdd)
    {
        Item existingItem = items.Find(i => i.name == itemToAdd.name);

        if (existingItem != null)
        {
            existingItem.count += itemToAdd.count;
        }
        else
        {
            Item newItem = new Item(itemToAdd.name, itemToAdd.count);
            items.Add(newItem);
        }

        Debug.Log(itemToAdd.count + " " + itemToAdd.name + " added to inventory");
    }

    public void RemoveItem(Item itemToRemove)
    {
        Item existingItem = items.Find(i => i.name == itemToRemove.name);

        if (existingItem != null)
        {
            existingItem.count -= itemToRemove.count;

            if (existingItem.count <= 0)
            {
                items.Remove(existingItem);
            }

            Debug.Log(itemToRemove.count + " " + itemToRemove.name + " removed from inventory");
        }
    }
}
