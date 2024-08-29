using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item; // The item to be picked up
    public int quantity = 1;

    public override void Interact()
    {
        Inventory inventory = FindObjectOfType<Inventory>(); // Find the player's inventory (assuming there is only one)
        if (inventory != null)
        {
            inventory.AddItem(item, quantity);
            Debug.Log("Picked up " + item.itemName);
            Destroy(gameObject); // Remove item from the world
        }
        else
        {
            Debug.LogError("No Inventory found on the player");
        }
    }
}
