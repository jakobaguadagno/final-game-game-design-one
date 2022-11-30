using UnityEngine;
using UnityEngine.UI;

public class playerHealthUI : MonoBehaviour
{

    public Text health;
    public GameObject inventoryUI;
    public GameObject healthCanvas;

    void Start()
    {
        inventoryUI = GameObject.Find("Game Manager/Canvas/InventoryUI");
    }

    void Update()
    {
        if(!playerInventory.dead)
        {
            health.text = "Health: " + playerInventory.playerHealth.ToString() + "/100";
        }
        if(inventoryUI.activeSelf)
        {
            healthCanvas.SetActive(false);
        }
        else
        {
            healthCanvas.SetActive(true);
        }
    }
}
