using UnityEngine;

public class awakeScene : MonoBehaviour
{
    playerInventory inv;

    public GameObject itemParent;
    public inventorySlot prefabSlot;

    void Start()
    {
        inv = playerInventory.instance;
        itemParent = GameObject.Find("Game Manager/Canvas/InventoryUI/Inventory/ScrollGroup/ScrollBoxUI");
        for(int i = 0; i < inv.pItems.Count; i++)
        {
            Instantiate(prefabSlot, itemParent.transform).AddItem(inv.pItems[i]);
        }
    }
}

