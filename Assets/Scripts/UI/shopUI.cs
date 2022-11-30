using UnityEngine;
using UnityEngine.UI;


public class shopUI : MonoBehaviour
{

    playerInventory inv;

    public Transform itemParent;
    private int i = 0;
    public inventorySlot prefabSlot;

    public Text money;
    public Text health;
    public Text weaponCost;

    void Start()
    {
        inv = playerInventory.instance;
        Debug.Log(inv.pItems.Count);
        UpdateUI();
    }

    void Update()
    {
        money.text = "Money: " + playerInventory.playerMoney;
        health.text = "Health: " + playerInventory.playerHealth;
        switch(playerInventory.playerWeapon)
        {
            case 1:
                weaponCost.text = "Upgrade Weapon\nCost: 200";
                break;
            case 2:
                weaponCost.text = "Upgrade Weapon\nCost: 300";
                break;
            case 3:
                weaponCost.text = "No More Upgrades";
                break;
        }
    }

    void UpdateUI()
    {
        for(i = 0; i < inv.pItems.Count; i++)
        {
            Instantiate(prefabSlot, itemParent).AddItem(inv.pItems[i]);
        }
    }

    public void FullHealth()
    {
        if (playerInventory.playerMoney >= 50 && playerInventory.playerHealth != 100)
        {
            playerInventory.playerMoney -= 50;
            playerInventory.playerHealth = 100;
            Debug.Log("Full Health \n Player HP: " + playerInventory.playerHealth + "\n Player Money: " + playerInventory.playerMoney);
        }
        else
        {
            Debug.Log("Player Money: " + playerInventory.playerMoney);
        }
    }
    public void Upgrade()
    {
        switch(playerInventory.playerWeapon)
        {
            case 1:
                if (playerInventory.playerMoney >= 100)
                {
                    playerInventory.playerMoney -= 100;
                    playerInventory.playerWeapon = 2;
                    
                    Debug.Log("New Weapon: " + playerInventory.playerWeapon);
                }
                else
                {
                    Debug.Log("Not Enough \n Player Money: " + playerInventory.playerMoney);
                }
                break;
            case 2:
                if (playerInventory.playerMoney >= 200)
                {
                    playerInventory.playerMoney -= 200;
                    playerInventory.playerWeapon = 3;
                    
                    Debug.Log("New Weapon: " + playerInventory.playerWeapon);
                }
                else
                {
                    Debug.Log("Not Enough \n Player Money: " + playerInventory.playerMoney);
                }
                break;
            case 3:
                break;
        }
    }
    
}