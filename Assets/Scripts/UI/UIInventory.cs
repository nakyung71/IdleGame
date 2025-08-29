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
    //이렇게 하는게 아니라 캐릭터 매니저에 플레이어 변수를 하나 저장해두고 그거에 접근하는게 빠르지 않을까?

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

        for (int i = 0; i < slotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
            slot.GetDragLayer(dragLayerTransform);
        }
        backButton.onClick.AddListener(CloseInventory);

        useButton.onClick.AddListener(UseItem);
        equipButton.onClick.AddListener(EquipOrUnEquipItem);
        discardButton.onClick.AddListener(DiscardItem);

        useButton.gameObject.SetActive(false);     
        equipButton.gameObject.SetActive(false);
        discardButton.gameObject.SetActive(false);
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

        foreach (UISlot uISlot in slots)
        {
            if(uISlot.SlotItemData == null)
            {
                return uISlot;
            }
            
        }
        CreateExtraSlot();
        return FindEmptySlot();

    }

    void CreateExtraSlot()
    {
        for (int i = 0; i < extraSlotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
            slot.GetDragLayer(dragLayerTransform);
        }

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

        //여기 버튼 띄우기
        switch (selectedSlot.SlotItemData.itemData.itemType)
        {
            case Itemtype.Potion:
                useButton.gameObject.SetActive(true);
                discardButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false); break;
            case Itemtype.Equipment:
                useButton.gameObject.SetActive(false);
                discardButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(true); break;
            case Itemtype.Others:
                useButton.gameObject.SetActive(false);
                discardButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false); break;

        }
        RefreshDescriptionPanel();
    }

    public void SelectSlot(UISlot slot,bool isEmpty)
    {
        //빈 슬롯을 선택했을 경우 오버로드
        selectedSlot = slot;
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        discardButton.gameObject.SetActive(false);
        selectedItemName.SetText(string.Empty);
        selectedItemDescription.SetText(string.Empty);
        selectedItemImage.sprite = null;
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
        character.ChangeHealth(selectedSlot.SlotItemData.itemData.potionValue);
        selectedSlot.SlotItemData.quantity--;
        selectedSlot.RefreshUI();
    }

    private void EquipOrUnEquipItem()
    {
        RuntimeItemData runtimeItemData = selectedSlot.SlotItemData;
        if (runtimeItemData.isEquipped)
        {
            //이미 착용된 상태인데
            character.ChangeAttack(-runtimeItemData.itemData.attackValue);
            character.ChangeDefence(-runtimeItemData.itemData.defenceValue);
            runtimeItemData.isEquipped = false;
            selectedSlot.RefreshUI();
            RefreshDescriptionPanel();
        }
        else
        {
            character.ChangeAttack(runtimeItemData.itemData.attackValue);
            character.ChangeDefence(runtimeItemData.itemData.defenceValue);
            runtimeItemData.isEquipped=true;
            selectedSlot.RefreshUI();
            RefreshDescriptionPanel() ;
        }

    }

    private void DiscardItem()
    {
        selectedSlot.DiscardSlotItem();
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        discardButton.gameObject.SetActive(false);
        selectedItemName.SetText(string.Empty);
        selectedItemDescription.SetText(string.Empty);
        selectedItemImage.sprite = null;
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
