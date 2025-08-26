using System.Collections;
using System.Collections.Generic;
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
    int slotNumber = 10;
    List<UISlot> slots = new List<UISlot>();

    public override void Init()
    {
        for (int i = 0; i < slotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
        }
        backButton.onClick.AddListener(CloseInventory);
        
    }


    public void SetItemData(List<ItemData> itemDatas)
    {
        Debug.Log(itemDatas.Count);
        foreach (UISlot uISlot in slots)
        {
            uISlot.DiscardSlotItem();
        }

        foreach(ItemData item in itemDatas)
        {
            
            FindEmptySlot().SetItem(item);
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

    
    


    private void CloseInventory()
    {
        UIManager.Instance.CloseUI();
    }
}
