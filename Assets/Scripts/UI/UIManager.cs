using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] List<BaseUI> UIList=new List<BaseUI>();

    //[SerializeField] UIMainMenu _uiMainMenu;
    //public UIMainMenu UiMainMenu
    //{
    //    get { return _uiMainMenu; }

    //}
    //[SerializeField] UIInventory _uiInventory;
    //public UIInventory UiInventory
    //{
    //    get { return _uiInventory; }

    //}
    //[SerializeField] UIStatus _uiStatus;
    //public UIStatus UiStatus
    //{
    //    get { return _uiStatus; }

    //}
    Dictionary<UIKey,GameObject> uiKeyDictionary = new Dictionary<UIKey,GameObject>();

    Stack<GameObject> openedUIStack = new Stack<GameObject>();
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var ui in UIList)
        {
            uiKeyDictionary.Add(ui.GetUIKey(),ui.gameObject);
            ui.Init();
        }
    }

    public void ShowUI(UIKey uIKey)
    {
        uiKeyDictionary.TryGetValue(uIKey, out var ui);
        if (ui != null)
        {
            ui.SetActive(true);
            openedUIStack.Push(ui);
        }
        else
        {
            Debug.Log("등록되지 않은UI");
        }
    }

    public void CloseUI()
    {
        if(openedUIStack.Count > 0)
        {
            GameObject go= openedUIStack.Pop();
            go.SetActive(false);
        }
        
    }
}
