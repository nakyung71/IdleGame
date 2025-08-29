using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CharacterData character; //이거에 대한 정보는? 인스펙터 드래그?



    private void Awake()
    {
        Instance = this;
        CharacterData characterData = new CharacterData();
        this.character = characterData;
    }
    private void Start()
    {
        SetData();







    }
    public void SetData()
    {
        //정보를 세팅하는 메서드에 여기 있는 플레이어를 인자로 해서 전달을 해라
        //예를 들어
        UIManager.Instance.UiInventory.GetPlayerCharacter(character);
        UIManager.Instance.UiStatus.GetPlayerCharacter(character);
        UIManager.Instance.UiMainMenu.GetPlayerCharacter(character);
        //이렇게 하려면 결국 이 게임매니저가 관련 메서드를 가진 모든 클래스를 알아야하는거 아닌가?

        //인터페이스로 뺼까? 이 부분을?
        //IPlayerData이런식
    }
}
