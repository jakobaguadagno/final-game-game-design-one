using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inventorySlot : MonoBehaviour
{
    itemScriptCreator item;

    public Image setImage;

    public void AddItem(itemScriptCreator newItem)
    {
        item = newItem;
        setImage.sprite = item.itemIcon;
    }

    public void RemoveItem()
    {
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
