using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : BaseUI
{
    public override UIKey Key => UIKey.Inventory;

    public override UIKey GetUIKey()
    {
        return Key;
    }

    [SerializeField] Transform contentBackground;
    [SerializeField] UISlot slotPrefab;
    [SerializeField] Button backButton;
    int slotNumber = 10;
    List<UISlot> slots = new List<UISlot>();


    // Start is called before the first frame update
  


    public override void Init()
    {
        for (int i = 0; i < slotNumber; i++)
        {
            UISlot slot = Instantiate(slotPrefab, contentBackground);
            slots.Add(slot);
        }
        backButton.onClick.AddListener(CloseInventory);
        
    }

    private void CloseInventory()
    {
        UIManager.Instance.CloseUI();
    }
}
