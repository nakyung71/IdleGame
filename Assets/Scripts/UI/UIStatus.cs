using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : BaseUI
{
    public override UIKey Key => UIKey.Status;
    [SerializeField] Button backButton;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI defenceText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI criticalRateText;
    public override UIKey GetUIKey()
    {
        return Key;
    }

    public override void Init()
    {
        backButton.onClick.AddListener(CloseStatus);
    }
    private void CloseStatus()
    {
        UIManager.Instance.CloseUI();
    }
    
}
