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

    private RuntimeItemData previousSlotItemData;

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
        itemImage.sprite = SlotItemData?.itemData.itemIcon;
        if (Quantity > 0)
        {
            quantityText.SetText(Quantity.ToString());
        }
        else
        {
            quantityText.SetText(string.Empty);
        }
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


        Image dragIconImage = dragIcon.GetComponent<Image>();
        dragIconImage.sprite= SlotItemData.itemData.itemIcon; 

        dragIcon.transform.SetParent(dragLayerTransform,false); 
        RectTransform rectTransform = (RectTransform)dragIcon.transform;
        SetIconPosition(rectTransform,eventData.position);
        previousSlotItemData = this.SlotItemData.DeepCopy(); //이거 넣어줘서 기좀 아이템 데이터 보존( 타 슬롯에 드래그 드롭 실패시) 근데 이렇게 하고 비울시 결국 참조형이라 다 비는 대참사
        //그래서 깊은 복사로 복사해두기(새 인스턴스 만들어서)
        dragIconImage.raycastTarget = false;
        DiscardSlotItem();
        
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
        var result = eventData.pointerCurrentRaycast.gameObject;
        var uiSlot = result.GetComponent<UISlot>();
        if( uiSlot != null )
        {
            
            Destroy(dragIcon);
            Debug.Log("슬롯에 넣음");
            
            return;
        }

        SlotItemData = previousSlotItemData;
        Destroy(dragIcon);
        SetItem(SlotItemData);



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
            if(originalSlot != null)
            {
                originalSlot.SlotItemData = originalSlot.previousSlotItemData;
                //기존에 있는 아이템 데이터(복사본)의 참조를 기존 슬롯에 다시 넣어줌
                RuntimeItemData originalSlotItem = originalSlot.SlotItemData;

                originalSlot.SetItem(this.SlotItemData);  //드랍이 된 그 슬롯의 데이터로 오리지넣 슬롯은 아이템이 생김 //여기가 문제일 가능성이 제일 큼

                //드래그 해서 넣었을떄 복사되는 문제가 간혹생김
                SetItem(originalSlotItem); //그리고 이 오리지넣 슬롯 아이템을 가지고 지금 이 드롭된 슬롯은 아이템을 저장


            }

 





        }



    }
}
