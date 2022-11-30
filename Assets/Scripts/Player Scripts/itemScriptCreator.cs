using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]

public class itemScriptCreator : ScriptableObject
{
    public string itemName = "Item";
    public string itemType = "None";
    public string itemDescription = "Yo!";

    public bool isUsableItem = false;

    public Sprite itemIcon = null;

    public int amountOfItem = 1;
    public int sellValue = 10;
}
