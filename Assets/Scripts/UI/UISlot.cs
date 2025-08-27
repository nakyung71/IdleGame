using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UISlot : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] GameObject equipTextPrefab;
    [SerializeField] Image itemImage;
    public Outline outline;
    UIInventory inventory;
    Canvas canvas;
    bool isDragging;

    RectTransform dragLayerTransform;

    GameObject dragIcon;
    public int Quantity { get; private set; }

    public RuntimeItemData SlotItemData { get; private set; }

    private void Start()
    {
        outline = GetComponent<Outline>();
        inventory = GetComponentInParent<UIInventory>();
        canvas=UIManager.Instance.canvas;

    }
    public void SetItem(RuntimeItemData runtimeItemData)
    {
        SlotItemData = runtimeItemData;
        Quantity = runtimeItemData?.quantity ?? 0;
        itemImage.sprite = runtimeItemData?.itemData.itemIcon;
        if(Quantity > 0)
        {
            quantityText.SetText(Quantity.ToString());
        }
        else
        {
            quantityText.SetText(string.Empty);
        }





    }

    public void RefreshUI()
    {
        
    }

    public void GetDragLayer(RectTransform rectTransform)
    {
        dragLayerTransform = rectTransform;
    }


    public void DiscardSlotItem()
    {
        SlotItemData = null;
        Quantity = 0;
        itemImage.sprite = null;
        quantityText.SetText(string.Empty);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.SlotItemData != null&&isDragging==false)
        {
            inventory.SelectSlot(this);
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(SlotItemData==null)
        {
            return;
        }
        isDragging = true;
        dragIcon = new GameObject("Icon", typeof(Image));


        dragIcon.GetComponent<Image>().sprite = SlotItemData.itemData.itemIcon;  
        dragIcon.transform.SetParent(dragLayerTransform,false); 
        RectTransform rectTransform = (RectTransform)dragIcon.transform;
        SetIconPosition(rectTransform,eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDragging)
        {
            return;
        }
        SetIconPosition((RectTransform)dragIcon.transform, eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!isDragging)
        {
            return;
        }
        isDragging=false;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(var result in results)
        {
            var slot=result.gameObject.GetComponent<UISlot>();
            if(slot!= null)
            {
                slot.OnDrop(eventData);
                Debug.Log("ΩΩ∑‘ø° ≥÷¿Ω");
                Destroy(dragIcon);
            }
            
        }
        Destroy(dragIcon);



    }

    private void SetIconPosition(RectTransform rectTransform, Vector2 screenPosition)
    {
        Vector2 local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragLayerTransform, screenPosition,canvas.worldCamera, out local);
        rectTransform.anchoredPosition = local;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData!=null)
        {
            UISlot originalSlot = eventData.pointerDrag.GetComponent<UISlot>();
            RuntimeItemData originalSlotItem=originalSlot.SlotItemData;
            originalSlot.SetItem(this.SlotItemData);

            SetItem(originalSlotItem);


        }
        

        
    }
}
