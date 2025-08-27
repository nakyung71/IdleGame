using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ItemData
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public string itemPath;
    public Sprite itemIcon;
    //public GameObject itemPrefab;

    public ItemData(int itemID, string itemName, string itemDescription, string itemPath)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemPath = itemPath;
    }

    //���� �����̶��?
    //���� �����?
}


public class RuntimeItemData //�̰ſ� ���� �ν��Ͻ��� ���� ������������
{
    public ItemData itemData;
    public int quantity;

    public RuntimeItemData(ItemData itemData,int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}




