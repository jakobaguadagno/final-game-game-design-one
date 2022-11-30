using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventoryUIScript : MonoBehaviour
{

    playerInventory inv;

    public GameObject itemParent;
    public inventorySlot prefabSlot;

    void Start()
    {
        itemParent = GameObject.Find("Game Manager/Canvas/InventoryUI/Inventory/ScrollGroup/ScrollBoxUI");
        inv = playerInventory.instance;
        inv.itemChangeCB += UpdateUI;
    }

    void Update()
    {
       
    }

    void UpdateUI()
    {
        Debug.Log(inv.pItems.Count);
        if(itemParent.transform.childCount >= 1)
        {
            for (int i = itemParent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(itemParent.transform.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < inv.pItems.Count; i++)
        {
            Instantiate(prefabSlot, itemParent.transform).AddItem(inv.pItems[i]);
        }
        
    }
}
