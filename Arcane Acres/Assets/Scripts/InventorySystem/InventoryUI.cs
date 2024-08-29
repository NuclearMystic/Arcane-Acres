using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject inventorySlotPrefab;
    public Transform inventorySlotParent;

    public GameObject itemPopup;
    public TMP_Text popupItemName;
    public Button useButton;
    public Button removeButton;

    private List<GameObject> pool = new List<GameObject>();
    private InventorySlotUI currentSlotUI;

    void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned.");
            return;
        }

        // Deactivate existing UI elements instead of destroying them
        foreach (Transform child in inventorySlotParent)
        {
            child.gameObject.SetActive(false);
            pool.Add(child.gameObject);
        }

        // Add inventory slots to the UI
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            GameObject newSlot = GetPooledObject();
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();
            if (i < inventory.inventorySlots.Count)
            {
                slotUI.SetSlot(inventory.inventorySlots[i]);
            }
            else
            {
                slotUI.ClearSlot(); // Clear unused slots
            }
            newSlot.SetActive(true);
        }
    }

    private GameObject GetPooledObject()
    {
        // Reuse an inactive object from the pool if available
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                pool.Remove(obj);
                return obj;
            }
        }

        // If no inactive objects are available, create a new one
        GameObject newObj = Instantiate(inventorySlotPrefab, inventorySlotParent);
        return newObj;
    }

    public void ShowPopup(InventorySlotUI slotUI, InventorySlot slot)
    {
        currentSlotUI = slotUI;
        popupItemName.text = slot.item.itemName;
        itemPopup.SetActive(true);
        itemPopup.transform.position = Input.mousePosition;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => UseItem(slot));

        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(() => RemoveItem(slot));
    }

    public void HidePopup()
    {
        itemPopup.SetActive(false);
    }

    private void UseItem(InventorySlot slot)
    {
        Debug.Log("Used item: " + slot.item.itemName);
        // Implement item usage logic here
        HidePopup();
    }

    private void RemoveItem(InventorySlot slot)
    {
        inventory.RemoveItem(slot.item, 1);
        RefreshUI();
        HidePopup();
    }

    public bool IsMouseOverPopup()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            itemPopup.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main);
    }
}
