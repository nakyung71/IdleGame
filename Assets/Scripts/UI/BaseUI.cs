using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIKey
{
    MainMenu,
    Inventory,
    Status

}
public abstract class BaseUI : MonoBehaviour
{
    public abstract UIKey Key { get; }

    public abstract UIKey GetUIKey();

    public abstract void Init();

}
