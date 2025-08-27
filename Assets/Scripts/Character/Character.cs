using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public float Attack { get; private set; } = 0f;
    public float Defence { get; private set; } = 0f;
    public float Health { get; private set; } = 100f;
    public float CriticalRate { get; private set; } = 0f;

    public List<ItemData> characterInventory = new List<ItemData>();

    //이렇게 하지 말고 저 리스트를 private으로 바꾼다음

    public List<RuntimeItemData> runtimeDataInventory = new List<RuntimeItemData>();




    public CharacterData()
    {

    }

    public void ChangeHealth()
    {

    }
    //리스트는 중복도 허용하니까 그냥 일단 넣어두고
    //만약 그게 겹쳐지는 아이템이라면 UI쪽에서 몇개인지 관리할까?
    public void AddItem(ItemData item)
    {
        characterInventory.Add(item);
        UIManager.Instance.UiInventory.SetItemData(characterInventory);
    }

}
