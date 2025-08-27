using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : BaseUI
{

    CharacterData character;
    
    public void GetPlayerCharacter(CharacterData character)
    {
        this.character = character;
        
    }
    //�̷��� �ϴ°� �ƴ϶� ĳ���� �Ŵ����� �÷��̾� ������ �ϳ� �����صΰ� �װſ� �����ϴ°� ������ ������?

    public override UIKey Key => UIKey.Inventory;

    public override UIKey GetUIKey()
    {
        return Key;
    }

    [SerializeField] Transform contentBackground;
    [SerializeField] UISlot slotPrefab;
    [SerializeField] Button backButton;
    [SerializeField] Image selectedItemImage;
    [SerializeField] TextMeshProUGUI selectedItemName;
    [SerializeField] TextMeshProUGUI selectedItemDescription;
    [SerializeField] Button useButton;
    [SerializeField] Button discardButton;
    [SerializeField] RectTransform dragLayerTransform;
    int slotNumber = 10;
    List<UISlot> slots = new List<UISlot>();
    UISlot selectedSlot;

    public override void Init()
    {
        for (int i = 0; i < slotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
            slot.GetDragLayer(dragLayerTransform);
        }
        backButton.onClick.AddListener(CloseInventory);
        
    }


    public void SetItemData(List<RuntimeItemData> runtimeItemDatas)
    {

        foreach (UISlot uISlot in slots)
        {
            uISlot.DiscardSlotItem();
        }

        foreach (RuntimeItemData runtimeItem in character.runtimeDataInventory) 
        {
            
            FindEmptySlot().SetItem(runtimeItem);
        }
    }

    UISlot FindEmptySlot()
    {
        foreach (UISlot uISlot in slots)
        {
            if(uISlot.SlotItemData == null)
            {
                return uISlot;
            }
            
        }
        Debug.Log("�󽽷��� �����ϴ�");
        return null;
    }

    public void SelectSlot(UISlot slot)
    {
        
        if(selectedSlot!=null)
        {
            selectedSlot.outline.enabled = false;
        }
        selectedSlot = slot;
        selectedSlot.outline.enabled = true;
        SetDescriptionPanel();




    }
    
    void SetDescriptionPanel()
    {
        ItemData data = selectedSlot.SlotItemData.itemData;
        selectedItemName.SetText(data.itemName);
        selectedItemDescription.SetText(data.itemDescription);
        selectedItemImage.sprite=data.itemIcon;
    }

    private void CloseInventory()
    {
        UIManager.Instance.CloseUI();
    }
}
