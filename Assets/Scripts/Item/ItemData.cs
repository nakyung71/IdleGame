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
    public Sprite itemIcon;

    public int maxStack;//���߿� ���� ���� ���� �����ڿ��� �������ٿ���
    public bool isStackable => maxStack > 1 ? true:false;
    //public GameObject itemPrefab;

    public ItemData(int itemID, string itemName, string itemDescription, string itemPath,int maxStack)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemPath = itemPath;
        this.maxStack = maxStack;

        //���⿡�ٰ� ���� ���� ���� "Equipment"��� �� �������� ������ Ÿ���� Equipment�� ��� ���������͵� �ִ°� ������
    }

    //���� �����̶��?
    //���� �����?
}


public class RuntimeItemData //�̰ſ� ���� �ν��Ͻ��� ���� ������������
{
    public ItemData itemData;
    public int quantity = 1;
    public bool isEquipped;

    public RuntimeItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public RuntimeItemData(ItemData itemData,int addedQuantity)
    {
        this.itemData = itemData;
        this.quantity += addedQuantity;
    }

    public RuntimeItemData DeepCopy() => new RuntimeItemData(this.itemData);
   
}




