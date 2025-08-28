using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public float Attack { get; private set; } = 0f;
    public float Defence { get; private set; } = 0f;
    public float Health { get; private set; } = 100f;
    public float CriticalRate { get; private set; } = 0f;



    //�̷��� ���� ���� �� ����Ʈ�� private���� �ٲ۴���

    public List<RuntimeItemData> runtimeDataInventory = new List<RuntimeItemData>();
    //�̰� ���� ���� ���� �������� � ������ �ִ��� �ľ� ����



    public CharacterData()
    {

    }

    public void ChangeAttack(float amount)
    {
        Attack += amount;
    }

    public void ChangeDefence(float amount)
    {
        Defence += amount;
    }
    public void ChangeHealth(float amount)
    {
        Health += amount;
    }
    //����Ʈ�� �ߺ��� ����ϴϱ� �׳� �ϴ� �־�ΰ�
    //���� �װ� �������� �������̶�� UI�ʿ��� ����� �����ұ�?
    public void AddItem(ItemData item)
    {

        foreach (RuntimeItemData runtimeItemData in runtimeDataInventory)
        {
            if (item == runtimeItemData.itemData && item.isStackable == true&&runtimeItemData.quantity<runtimeItemData.itemData.maxStack)//����ٰ� ���� Max���� �����ؼ� ���ǰ����� ���߿� �ִ��� �̷�������
            {
                //�̷���� �̹� �ִ� �������̴ϱ� ������ ���������ش�.
                runtimeItemData.quantity++;
                UIManager.Instance.UiInventory.SetItemData(runtimeItemData);
                return;

            }
        }

        ////���� �ݺ����� ������ ���� �������� ������/Ȥ�� ������ ������ ������ ���ο� ��Ÿ�� �ν��Ͻ��� ���� ��Ÿ�� ������ ����Ʈ�� �־��ش�!
        RuntimeItemData runtimeItem=new RuntimeItemData(item);
        runtimeDataInventory.Add(runtimeItem);
        UIManager.Instance.UiInventory.SetItemData(runtimeItem);
    }

}
