using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : BaseUI
{

    [SerializeField] Button inventoryButton;
    [SerializeField] Button statusButton;
    public override UIKey Key => UIKey.MainMenu;
    
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
    }
}
