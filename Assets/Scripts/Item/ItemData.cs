using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Itemtype
{
    Equipment,
    Others,
    Potion
}



[System.Serializable]
public class ItemData
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public string itemPath;
    public int maxStack;

    public int attackValue;
    public int defenceValue;
    public int potionValue;



    public Itemtype itemType;
    public bool isStackable => maxStack > 1 ? true:false;
    //public GameObject itemPrefab;
    public Sprite itemIcon;
    public ItemData(int itemID, string itemName, string itemDescription, string itemPath,string itemType,int maxStack,int attackValue,int defenceValue, int potionValue)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemPath = itemPath;
        this.maxStack = maxStack;

        this.attackValue = attackValue;
        this.defenceValue = defenceValue;
        this.potionValue = potionValue;

        if(itemType=="Equipment")
        {
            this.itemType = Itemtype.Equipment;
        }
        else if(itemType=="Potion")
        {
            this.itemType = Itemtype.Potion;
        }
        else if(itemType=="Others")
        {
            this.itemType = Itemtype.Others;
        }

        //���⿡�ٰ� ���� ���� ���� "Equipment"��� �� �������� ������ Ÿ���� Equipment�� ��� ���������͵� �ִ°� ������
    }

    //���� �����̶��?
    //���� �����?
}

[System.Serializable]
public class RuntimeItemData //�̰ſ� ���� �ν��Ͻ��� ���� ������������
{
    public ItemData itemData;
    public int quantity = 1;
    public bool isEquipped;

    public RuntimeItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public RuntimeItemData(ItemData itemData, int quantity, bool isEquipped)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        this.isEquipped = isEquipped;
    }

    public RuntimeItemData DeepCopy() => new RuntimeItemData(this.itemData,this.quantity,this.isEquipped);
   
}




