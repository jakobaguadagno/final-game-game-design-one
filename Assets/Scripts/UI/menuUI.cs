using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUI : MonoBehaviour
{
    
    public GameObject MainMenu;
    public GameObject Directions;
    public GameObject Credits;
    public GameObject Settings;
    public GameObject Shop;
    public GameObject Overworld;
    public GameObject Win;
    public GameObject Lose;
    public GameObject itemParentBox;

    void Start()
    {
        if(!(playerInventory.dead))
        {
            MainMenu.SetActive(true);
            HideUI(Directions, Credits, Settings, Shop, Overworld, Win, Lose);
            
        }
        if(playerInventory.dead)
        {
            Lose.SetActive(true);
            HideUI(Directions, Credits, Settings, Shop, Overworld, Win, MainMenu);
            playerInventory.dead = false;
        }
        if(playerInventory.playerLevel==4)
        {
            Win.SetActive(true);
            HideUI(Directions, Credits, Settings, Shop, Overworld, Lose, MainMenu);
        }
        playerInventory.levelComplete = false;
        itemParentBox = GameObject.Find("Game Manager/Canvas/InventoryUI/Inventory/ScrollGroup/ScrollBoxUI");
    }

    private void HideUI(GameObject ui1,GameObject ui2, GameObject ui3, GameObject ui4, GameObject ui5, GameObject ui6, GameObject ui7)
    {
        ui1.SetActive(false);
        ui2.SetActive(false);
        ui3.SetActive(false);
        ui4.SetActive(false);
        ui5.SetActive(false);
        ui6.SetActive(false);
        ui7.SetActive(false);
    }


    public void Level1Button()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Level 1");
    }

    public void Level2Button()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Level 2");
    }

    public void Level3Button()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Level 3");
    }

    public void MenuButton()
    {
        HideUI(Directions, Credits, Settings, Shop, Overworld, Lose, Win);
        MainMenu.SetActive(true);
    }

    public void DirectionsButton()
    {
        HideUI(MainMenu, Credits, Settings, Shop, Overworld, Win, Lose);
        Directions.SetActive(true);
    }

    public void CreditsButton()
    {
        HideUI(MainMenu, Directions, Settings, Shop, Overworld, Win, Lose);
        Credits.SetActive(true);
    }

    public void SettingsButton()
    {
        HideUI(MainMenu, Directions, Credits, Shop, Overworld, Win, Lose);
        Settings.SetActive(true);
    }

    public void OverworldButton()
    {
        if(itemParentBox.transform.childCount >= 1)
        {
            for (int i = itemParentBox.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(itemParentBox.transform.GetChild(i).gameObject);
            }
        }
        HideUI(MainMenu, Directions, Credits, Shop, Settings, Win, Lose);
        Overworld.SetActive(true);
    }

    public void ShopButton()
    {
        HideUI(MainMenu, Directions, Credits, Settings, Overworld, Win, Lose);
        Shop.SetActive(true);
    }
}
