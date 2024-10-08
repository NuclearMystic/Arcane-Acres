using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TMP_Text quantityText;
    private InventorySlot slot;
    private InventoryUI inventoryUI;

    private bool isMouseOver = false;

    void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void SetSlot(InventorySlot newSlot)
    {
        slot = newSlot;
        icon.sprite = slot.item.icon;
        icon.enabled = true;
        quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
    }

    public void ClearSlot()
    {
        slot = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot != null)
        {
            inventoryUI.ShowPopup(this, slot);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    void Update()
    {
        // Check if the mouse is clicked outside of the slot and popup
        if (Input.GetMouseButtonDown(0) && !isMouseOver && !IsPointerOverUIElement())
        {
            inventoryUI.HidePopup();
        }
    }

    private bool IsPointerOverUIElement()
    {
        // Check if the pointer is over any UI element in the popup
        return EventSystem.current.IsPointerOverGameObject();
    }
}
