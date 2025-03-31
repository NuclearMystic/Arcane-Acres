using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    private InventorySlot slot;
    private EquipmentUI equipmentUI;

    private bool isMouseOver = false;

    void Start()
    {
        equipmentUI = GetComponentInParent<EquipmentUI>();
    }

    public void SetSlot(InventorySlot newSlot)
    {
        slot = newSlot;
        icon.sprite = slot.item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        slot = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot != null)
        {
            //equipmentUI.ShowPopup(this, slot);
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
            //equipmentUI.HidePopup();
        }
    }

    private bool IsPointerOverUIElement()
    {
        // Check if the pointer is over any UI element in the popup
        return EventSystem.current.IsPointerOverGameObject();
    }
}
