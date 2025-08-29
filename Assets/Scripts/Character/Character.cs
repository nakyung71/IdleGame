using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public float Attack { get; private set; } = 0f;
    public float Defence { get; private set; } = 0f;
    private float _health = 100f;
    public float Health 
    { 
        get { return _health; } 
        private set 
        {
            _health = Mathf.Clamp(value,0, 100f);
            return;
        } 
    } 
    public float CriticalRate { get; private set; } = 0f;

    public int Level { get; private set; } = 1;

    public float CurrentExp { get; private set; } = 0;

    public float MaxExp
    {
        get { return Level * 100; }
    }

    private Action OnExpChange;

    //�̷��� ���� ���� �� ����Ʈ�� private���� �ٲ۴���

    public List<RuntimeItemData> runtimeDataInventory = new List<RuntimeItemData>();
    //�̰� ���� ���� ���� �������� � ������ �ִ��� �ľ� ����



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

    private void LevelUp()
    {
        Level++;
        
    }

    public void GainExp(float exp)
    {
        CurrentExp += exp;
        LevelExpCalculator();
    }

    private void LevelExpCalculator()
    {
        var neededExp = Level * 100;
        while(CurrentExp >= neededExp)
        {
            CurrentExp = CurrentExp - neededExp;
            LevelUp();
            neededExp = Level * 100;
        }
        OnExpChange?.Invoke();
       
    }
    
    public void SubscribeOnExpChangeEvent(Action action)
    {
        OnExpChange += action;
    }


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
