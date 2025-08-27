using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UISlot : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] GameObject equipTextPrefab;
    [SerializeField] Image itemImage;
    public Outline outline;
    UIInventory inventory;
    public int Quantity { get; private set; }

    public ItemData SlotItemData { get; private set; }

    private void Start()
    {
        outline = GetComponent<Outline>();
        inventory = GetComponentInParent<UIInventory>();
    }
    public void SetItem(ItemData item)
    {
        SlotItemData = item;
        Quantity++;
        itemImage.sprite=item.itemIcon;
        quantityText.SetText(Quantity.ToString());
        
    }

    public void RefreshUI()
    {
        
    }




    public void DiscardSlotItem()
    {
        SlotItemData = null;
        Quantity = 0;
        itemImage.sprite = null;
        quantityText.SetText(string.Empty);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.SlotItemData != null)
        {
            inventory.SelectSlot(this);
        }
        
    }
}
