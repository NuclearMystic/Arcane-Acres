using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public EquipmentSlot equipSlot;

    public int damageModifier;

    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    public SkinnedMeshRenderer mesh;
    public bool isStackable;

    public ItemType itemType;
    ItemType weapon = ItemType.Weapon;
    ItemType clothes = ItemType.Clothes;
    ItemType consumable = ItemType.Consumable;
    ItemType resource = ItemType.Resource;

    public void Use()
    {
        if (itemType == weapon)
        {
            UseWeapon();
        }
        else if (itemType == consumable)
        {
            UseConsumable();
        }
        else if (itemType == resource)
        {
            UseResource();
        }
        else if (itemType == clothes)
        {
            UseClothes();
        }

    }

    void UseWeapon()
    {
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
        Debug.Log("This is a weapon.");
    }

    void UseConsumable()
    {

        Debug.Log("This is a consumable");
    }

    void UseResource()
    {
        Debug.Log("This is a resource.");
    }

    void UseClothes()
    {
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
        Debug.Log("This is armor.");
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }

}

public enum ItemType
{
    Weapon,
    Clothes,
    Consumable,
    Resource,
}

public enum EquipmentSlot
{
    Headwear,
    Outfit,
    Footwear,
    Belt,
    Necklace,
    Bracklet,
    Robes,
    Weapon,
    NotEquipable,
}
