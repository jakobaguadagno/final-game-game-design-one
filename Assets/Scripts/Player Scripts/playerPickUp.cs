using UnityEngine;

public class playerPickUp : interactionScript
{

    public itemScriptCreator itemPicked;

    public override void Interact()
    {
        base.Interact();
        picked();
    }

    void picked()
    {
        Debug.Log("Picking up: " + itemPicked.name + " / Item type: " + itemPicked.itemType + " / Item description: " + itemPicked.itemDescription + " / Sell for: " + itemPicked.sellValue);
        playerInventory.instance.Add(itemPicked);
        Destroy(gameObject);
    }
}
