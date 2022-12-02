using UnityEngine;
using UnityEngine.UI;

public class settingsUI : MonoBehaviour
{

    public Toggle inventoryOn;

    void Start()
    {
        inventoryOn.isOn =  playerInventory.keepInventory;
    }


    void Update()
    {
        if(!inventoryOn.isOn)
        {
            playerInventory.keepInventory = false;
        }
        else if(inventoryOn.isOn)
        {
            playerInventory.keepInventory = true;
        }
        
    }
}
