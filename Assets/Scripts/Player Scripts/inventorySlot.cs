using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class inventorySlot : MonoBehaviour
{
    itemScriptCreator item;

    public TextMeshProUGUI itemName;

    public void AddItem(itemScriptCreator newItem)
    {
        item = newItem;
        itemName.SetText(item.itemName);
    }

    public void RemoveItem()
    {
        Debug.Log("Removing... " + item);
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            if(item.itemName == "Wood")
            {
                playerInventory.playerMoney += 10;
                playerInventory.instance.Remove(item);
            }
            if(item.itemName == "Metal")
            {
                playerInventory.playerMoney += 20;
                playerInventory.instance.Remove(item);
            }
            if(item.itemName == "Food")
            {
                playerInventory.playerMoney += 30;
                playerInventory.instance.Remove(item);
            }
        }
        else
        {
            playerInventory.instance.Remove(item);
        }
    }

}
