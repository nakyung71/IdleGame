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

    public int maxStack = 3; //나중에 파일 조정 통해 생성자에서 관리해줄예정
    public bool isStackable => maxStack > 1 ? true:false;
    //public GameObject itemPrefab;

    public ItemData(int itemID, string itemName, string itemDescription, string itemPath)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemPath = itemPath;
    }

    //만약 포션이라면?
    //만약 장비라면?
}


public class RuntimeItemData //이거에 대한 인스턴스는 언제 만들어줘야하지
{
    public ItemData itemData;
    public int quantity = 1;

    public RuntimeItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public RuntimeItemData(ItemData itemData,int addedQuantity)
    {
        this.itemData = itemData;
        this.quantity += addedQuantity;
    }
   
}




