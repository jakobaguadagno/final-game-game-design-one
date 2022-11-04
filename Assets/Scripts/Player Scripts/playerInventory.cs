using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    
    public static playerInventory instance;

    void Awake()
    {
        instance = this;
    }

    public delegate void itemChange();
    public itemChange itemChangeCB;

    public int inventorySpace = 20;

    public List<itemScriptCreator> pItems = new List<itemScriptCreator>();

    public void Add(itemScriptCreator itemName)
    {
        pItems.Add(itemName);
        if(itemChangeCB != null)
        {
            itemChangeCB.Invoke();
        }
    }

    public void Remove(itemScriptCreator itemName)
    {
        pItems.Remove(itemName);
        if(itemChangeCB != null)
        {
            itemChangeCB.Invoke();
        }
    }
}
