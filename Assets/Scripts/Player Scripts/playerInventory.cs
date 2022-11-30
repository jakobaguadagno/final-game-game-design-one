using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    
    public static playerInventory instance;

    public static int playerHealth;
    public static int playerMoney;
    public static int playerLevel;
    public static int playerDamage;
    public static int playerWeapon;
    public static float audioVolume;
    public static bool level1Done = false;
    public static bool level2Done = false;
    public static bool level3Done = false;
    public static bool levelComplete = false;
    public static bool dead = false;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } 
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerHealth = 100;
            playerMoney = 10000;
            playerDamage = 10;
            audioVolume = .25f;
            playerLevel = 1;
            playerWeapon = 1;
            Debug.Log("Health: " + playerHealth + " / Money: " + playerMoney);
        }
    }

    public delegate void itemChange();
    public itemChange itemChangeCB;

    public List<itemScriptCreator> pItems = new List<itemScriptCreator>();

    void Start()
    {
        Debug.Log("Health: " + playerHealth + " / Money: " + playerMoney);
    }

    void Update()
    {
        if(playerHealth<=0&&dead==false)
        {
            dead = true;
            playerHealth=100;
        }
        if(dead&&!(SceneManager.GetActiveScene().name=="MenuScene"))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

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
            //itemChangeCB.Invoke();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
