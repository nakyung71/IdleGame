using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image image;
    


    // Start is called before the first frame update
    void Start()
    {
        ItemData data= ResourceManager.Instance.GetItemData(1);
        image.sprite=data.itemIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
