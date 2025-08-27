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

    //�̷��� ���� ���� �� ����Ʈ�� private���� �ٲ۴���

    public List<RuntimeItemData> runtimeDataInventory = new List<RuntimeItemData>();




    public CharacterData()
    {

    }

    public void ChangeHealth()
    {

    }
    //����Ʈ�� �ߺ��� ����ϴϱ� �׳� �ϴ� �־�ΰ�
    //���� �װ� �������� �������̶�� UI�ʿ��� ����� �����ұ�?
    public void AddItem(ItemData item)
    {
        characterInventory.Add(item);
        UIManager.Instance.UiInventory.SetItemData(characterInventory);
    }

}
