using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public float Attack { get; private set; } = 0f;
    public float Defence { get; private set; } = 0f;
    public float Health { get; private set; } = 100f;
    public float CriticalRate { get; private set; } = 0f;



    //이렇게 하지 말고 저 리스트를 private으로 바꾼다음

    public List<RuntimeItemData> runtimeDataInventory = new List<RuntimeItemData>();
    //이걸 통해 내가 무슨 아이템을 몇개 가지고 있는지 파악 가능



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
        
        foreach(RuntimeItemData runtimeItemData in runtimeDataInventory)
        {
            if(item==runtimeItemData.itemData&&item.isStackable==true)//여기다가 이제 Max스택 관련해서 조건같은거 나중에 넣던가 이런식으로
            {
                //이럴경우 이미 있는 아이템이니까 수량만 증가시켜준다.
                runtimeItemData.quantity++; //여기서 최대 수량 관련해서는 좀 고민을 해보자
                UIManager.Instance.UiInventory.SetItemData(runtimeDataInventory);
                return;
                
            }
        }

        //만약 반복문을 돌려도 같은 아이템이 없으면/혹은 있지만 쌓을수 없을때 새로운 런타임 인스턴스를 만들어서 런타임 아이템 리스트에 넣어준다!
        RuntimeItemData runtimeItem=new RuntimeItemData(item);
        runtimeDataInventory.Add(runtimeItem);
        UIManager.Instance.UiInventory.SetItemData(runtimeDataInventory);
    }

}
