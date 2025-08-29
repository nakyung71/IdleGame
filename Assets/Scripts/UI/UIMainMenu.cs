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
    }

    private void SetGodModeButtons()
    {
        for (int i = 0; i < godmodeButtonsList.Count-1; i++)
        {
            var buttonTextComponent = godmodeButtonsList[i].GetComponentInChildren<TextMeshProUGUI>();
            var buttonItemData = ResourceManager.Instance.GetItemData(i + 1);
            buttonTextComponent.SetText($"{buttonItemData.itemName} Ãß°¡");
            godmodeButtonsList[i].onClick.AddListener(() => character.AddItem(buttonItemData));
        }
    }
    private void OpenGodModePanel(bool open)
    {
        
    }
}
