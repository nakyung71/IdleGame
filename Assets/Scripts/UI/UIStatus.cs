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

    CharacterData characterData;
    public override UIKey GetUIKey()
    {
        return Key;
    }

    public override void Init()
    {
        backButton.onClick.AddListener(CloseStatus);
    }

    public void GetPlayerCharacter(CharacterData characterData)
    {
        this.characterData = characterData;
    }
    private void OnEnable()
    {
        SetStatusData();
    }

    private void SetStatusData()
    {
        attackText.SetText($"Attack:\t{characterData.Attack}");
        defenceText.SetText($"Defence:\t{characterData.Defence}");
        healthText.SetText($"Health:\t{characterData.Health}");
        criticalRateText.SetText($"CriticalRate:\t{characterData.CriticalRate}");
    }

    private void CloseStatus()
    {
        UIManager.Instance.CloseUI();
    }
    
}
