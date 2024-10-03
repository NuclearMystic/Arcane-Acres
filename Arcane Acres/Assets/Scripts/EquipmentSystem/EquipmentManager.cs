using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Item[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Item[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnEquipmentChanged(Item newItem, Item oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        if (!targetMesh)
        {
            targetMesh = GameObject.FindWithTag("PlayerBody").GetComponent<SkinnedMeshRenderer>();
        }

        EquipDefaultItems();
    }

    public void Equip(Item newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Item oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Item Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {

            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Item oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    void EquipDefaultItems()
    {
        foreach (Item item in defaultItems)
        {
            Equip(item);
        }
    }

    public void Update()
    {
        if (targetMesh == null)
        {
            targetMesh = GameObject.FindWithTag("PlayerBody").GetComponent<SkinnedMeshRenderer>();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
