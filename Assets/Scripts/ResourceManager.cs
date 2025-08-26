using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    Dictionary<int, ItemData> itemInfoDIctionary=new Dictionary<int, ItemData>();

    public static ResourceManager Instance;

    private void Awake()
    {
        Instance = this;

        TextAsset itemCSVData = Resources.Load<TextAsset>("ItemTestCSV");
        string[] lines = itemCSVData.text.Split('\n');
        int length=lines.Length;
        //foreach (string line in lines)
        for (int i = 1; i < length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }
            string[] columns = lines[i].Split(',');
            for(int j=0; j < columns.Length; j++)
            {
                columns[j] = columns[j].Trim();
            }  
            int key = int.Parse(columns[0]);
            ItemData itemData = new ItemData(key, columns[1], columns[2], columns[3]); //인스턴스 만들어주고
            itemInfoDIctionary.Add(key, itemData);
        }
    }
    void Start()
    {
        
       
        
    }

    public ItemData GetItemData(int key)
    {
       if(itemInfoDIctionary.TryGetValue(key, out ItemData itemData))
       {
            string pathString = $"ItemIcons/{itemData.itemPath}";
            itemData.itemIcon = Resources.Load<Sprite>(pathString);
            return itemData;
       }
       else
       {
            Debug.Log("키에 해당하는 아이템이 없어 null이 반환됩니다");
            return null;
       }
            
    }
}
