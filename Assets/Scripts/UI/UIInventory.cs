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
    [SerializeField] Button equipButton;
    [SerializeField] Button discardButton;
    [SerializeField] RectTransform dragLayerTransform;
    [SerializeField] TextMeshProUGUI equipText;

    int slotNumber = 12;
    int extraSlotNumber = 6;
    List<UISlot> slots = new List<UISlot>();
    UISlot selectedSlot;

    public override void Init()
    {
        Debug.Log("Init��");
        for (int i = 0; i < slotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
            slot.GetDragLayer(dragLayerTransform);
        }
        backButton.onClick.AddListener(CloseInventory);
        useButton.onClick.AddListener(UseItem);
        equipButton.onClick.AddListener(EquipOrUnEquipItem);

    }


    public void SetItemData(RuntimeItemData runtimeItemData)
    {
        if(runtimeItemData.itemData.isStackable)
        {
            UISlot foundSlot = FindStack(runtimeItemData);
            
            if(foundSlot.SlotItemData != null)
            {

                foundSlot.RefreshUI();
                return;

            }
            else
            {
                foundSlot.SetItem(runtimeItemData);
                foundSlot.RefreshUI();
            }
        }
        else
        {
            FindEmptySlot().SetItem(runtimeItemData);
        }
        

        
    }

    UISlot FindEmptySlot()
    {
        Debug.Log(slots.Count);
        foreach (UISlot uISlot in slots)
        {
            if(uISlot.SlotItemData == null)
            {
                return uISlot;
            }
            
        }
        CreateExtraSlot();
        FindEmptySlot();
        Debug.Log("�󽽷��� �����ϴ�");
        return null;
    }

    UISlot FindStack(RuntimeItemData runtimeItemData)
    {
        foreach(UISlot uISlot in slots)
        {
            if(uISlot.SlotItemData == runtimeItemData )
            {
                return uISlot;
                
            }
           
        }
        
        return FindEmptySlot();
    }

    public void SelectSlot(UISlot slot)
    {
        
        if(selectedSlot!=null)
        {
            //selectedSlot.outline.enabled = false;
        }
        selectedSlot = slot;
        //selectedSlot.outline.enabled = true;
        SetDescriptionPanel();

        //���� ��ư ����
        switch (selectedSlot.SlotItemData.itemData.itemType)
        {
            case Itemtype.Potion:
                useButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false); break;
            case Itemtype.Equipment:
                useButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true); break;
            case Itemtype.Others:
                useButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false); break;

        }
        RefreshDescriptionPanel();
    }
    void CreateExtraSlot()
    {
        for(int i=0; i<extraSlotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
            slot.GetDragLayer(dragLayerTransform);
        }
        
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

    private void UseItem()
    {

    }

    private void EquipOrUnEquipItem()
    {
        RuntimeItemData runtimeItemData = selectedSlot.SlotItemData;
        if (runtimeItemData.isEquipped)
        {
            //�̹� ����� �����ε�
            character.ChangeAttack(-runtimeItemData.itemData.attackValue);
            character.ChangeDefence(-runtimeItemData.itemData.defenceValue);
            runtimeItemData.isEquipped = false;
            RefreshDescriptionPanel();
        }
        else
        {
            character.ChangeAttack(runtimeItemData.itemData.attackValue);
            character.ChangeDefence(runtimeItemData.itemData.defenceValue);
            runtimeItemData.isEquipped=true;
            RefreshDescriptionPanel() ;
        }

    }

    private void RefreshDescriptionPanel()
    {
        if(selectedSlot==null)
        {
            return;
        }
        RuntimeItemData runtimeItemData = selectedSlot.SlotItemData;

        if(runtimeItemData.isEquipped)
        {

            equipText.enabled = true;
        }
        else
        {

            equipText.enabled=false;
        }
    }
}
