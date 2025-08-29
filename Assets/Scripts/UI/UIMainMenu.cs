using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : BaseUI
{

    [SerializeField] Button inventoryButton;
    [SerializeField] Button statusButton;
    [SerializeField] GameObject godmodePanel;
    [SerializeField] RectTransform friendshipRect;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI friendshipBarText;
    public override UIKey Key => UIKey.MainMenu;

    CharacterData character;

    private List<Button> godmodeButtonsList=new List<Button>();
    public override UIKey GetUIKey()
    {
        return Key;
    }

    
    void OpenMainMenu()
    {
        
    }

    void OpenStatus()
    {
        UIManager.Instance.ShowUI(UIKey.Status);
    }

    void OpenInventory()
    {
        UIManager.Instance.ShowUI(UIKey.Inventory);
    }

    public override void Init()
    {
        inventoryButton.onClick.AddListener(OpenInventory);
        statusButton.onClick.AddListener(OpenStatus);
        godmodeButtonsList=godmodePanel.GetComponentsInChildren<Button>().ToList();
        SetGodModeButtons();
        
        
    }
    public void GetPlayerCharacter(CharacterData character)
    {
        this.character = character;
        character.SubscribeOnExpChangeEvent(UpdateFriendshipBar);
    }

    private void SetGodModeButtons()
    {
        for (int i = 0; i < godmodeButtonsList.Count-1; i++)
        {
            var buttonTextComponent = godmodeButtonsList[i].GetComponentInChildren<TextMeshProUGUI>();
            var buttonItemData = ResourceManager.Instance.GetItemData(i + 1);
            buttonTextComponent.SetText($"{buttonItemData.itemName} 추가");
            godmodeButtonsList[i].onClick.AddListener(() => character.AddItem(buttonItemData));
        }
        godmodeButtonsList[godmodeButtonsList.Count - 1].onClick.AddListener(() => character.GainExp(100));
    }
    
    private void UpdateFriendshipBar()
    {
        var friendShipBar = friendshipRect.sizeDelta;
        friendShipBar.x = 760 * (character.CurrentExp/character.MaxExp);
        friendshipRect.sizeDelta = friendShipBar;
        friendshipBarText.SetText($"친밀도 게이지 \t{character.CurrentExp} / {character.MaxExp}");
        levelText.SetText($"LV {character.Level}");
    }
}
