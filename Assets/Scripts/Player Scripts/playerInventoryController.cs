using UnityEngine;
using UnityEngine.InputSystem;


public class playerInventoryController : MonoBehaviour
{

    private PlayerInput MyInput;
    private bool inventoryButton = false;
    public GameObject inventory;

    void Start()
    {
        MyInput = GetComponent<PlayerInput>();
        inventory = GameObject.Find("Game Manager/Canvas/InventoryUI");
        inventory.SetActive(false);
    }

    public void InventoryControl()
    {
        if(!inventoryButton)
        {
            AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            {
                foreach(AudioSource sound in audios)
                {
                    sound.volume = 0;
                    if(sound.ToString()=="Button Click")
                    {
                        sound.volume = playerInventory.audioVolume/2;
                    }
                }
            }
            inventory.SetActive(true);
            Time.timeScale = 0f;
            inventoryButton = true;
        }
        else
        {
            AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            {
                foreach(AudioSource sound in audios)
                {
                    sound.volume = playerInventory.audioVolume;
                    if(sound.ToString()=="Button Click")
                    {
                        sound.volume = playerInventory.audioVolume/2;
                    }
                }
            }
            inventory.SetActive(false);
            Time.timeScale = 1f;
            inventoryButton = false;
        }
    }

    

}
