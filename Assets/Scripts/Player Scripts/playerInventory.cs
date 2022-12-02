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
    public static bool keepInventory;
    public static bool level1Done;
    public static bool level2Done;
    public static bool level3Done;
    public static bool levelComplete;
    public static bool dead;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } 
        else
        {
            playerHealth = 100;
            playerMoney = 0;
            playerDamage = 10;
            audioVolume = .25f;
            playerLevel = 1;
            playerWeapon = 1;
            keepInventory = true;
            level1Done = false;
            level2Done = false;
            level3Done = false;
            levelComplete = false;
            dead = false;
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        if(playerInventory.dead && !playerInventory.keepInventory)
        {
            Debug.Log("Items Before Death: " +pItems.Count);
            while (pItems.Count>0)
            {
                pItems.RemoveAt(pItems.Count-1);
            }
            Debug.Log("Items After Death: " +pItems.Count);
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
