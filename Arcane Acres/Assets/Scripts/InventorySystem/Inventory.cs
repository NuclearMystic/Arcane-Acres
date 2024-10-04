using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int maxSlots = 20; // Define the maximum number of slots

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Item item, int quantity = 1)
    {
        // Check if item is stackable and already exists in inventory
        InventorySlot slot = inventorySlots.Find(i => i.item == item && item.isStackable);
        if (slot != null)
        {
            slot.quantity += quantity;
        }
        else
        {
            // Check if there is available space for a new slot
            if (inventorySlots.Count < maxSlots)
            {
                inventorySlots.Add(new InventorySlot(item, quantity));
            }
            else
            {
                Debug.LogWarning("Inventory is full!");
            }
        }
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        InventorySlot slot = inventorySlots.Find(i => i.item == item);
        if (slot != null)
        {
            slot.quantity -= quantity;
            if (slot.quantity <= 0)
            {
                inventorySlots.Remove(slot);
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    public InventorySlot(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
