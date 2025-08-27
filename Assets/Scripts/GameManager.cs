using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CharacterData character; //�̰ſ� ���� ������? �ν����� �巡��?



    private void Awake()
    {
        Instance = this;
        CharacterData characterData = new CharacterData();
        this.character = characterData;
    }
    private void Start()
    {
        SetData();
        character.AddItem(ResourceManager.Instance.GetItemData(1));
        character.AddItem(ResourceManager.Instance.GetItemData(2)); 
        character.AddItem(ResourceManager.Instance.GetItemData(1));
        character.AddItem(ResourceManager.Instance.GetItemData(2));
        character.AddItem(ResourceManager.Instance.GetItemData(1));
        
    }
    public void SetData()
    {
        //������ �����ϴ� �޼��忡 ���� �ִ� �÷��̾ ���ڷ� �ؼ� ������ �ض�
        //���� ���
        UIManager.Instance.UiInventory.GetPlayerCharacter(character);
        UIManager.Instance.UiStatus.GetPlayerCharacter(character);
        //�̷��� �Ϸ��� �ᱹ �� ���ӸŴ����� ���� �޼��带 ���� ��� Ŭ������ �˾ƾ��ϴ°� �ƴѰ�?

        //�������̽��� �E��? �� �κ���?
        //IPlayerData�̷���
    }
}
