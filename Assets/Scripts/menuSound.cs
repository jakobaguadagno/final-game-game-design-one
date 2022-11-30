using UnityEngine;

public class menuSound : MonoBehaviour
{
    
    public audioScript mainMenu;

    void Start()
    {
        mainMenu = GameObject.Find("Game Manager/Sound Manager").GetComponent<audioScript>();
    }

    public void PlayBC()
    {
        mainMenu.PlayButtonClick();
    }
    
}
