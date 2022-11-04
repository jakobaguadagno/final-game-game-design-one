using UnityEngine;

public class inventoryUIScript : MonoBehaviour
{

    playerInventory inv;

    void Start()
    {
        inv = playerInventory.instance;
        inv.itemChangeCB += UpdateUI;
    }

    void Update()
    {
        
    }

    void UpdateUI()
    {

    }
}
