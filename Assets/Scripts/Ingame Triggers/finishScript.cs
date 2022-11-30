using UnityEngine;
using UnityEngine.SceneManagement;

public class finishScript : MonoBehaviour
{
    public MeshRenderer finish;

    void Start()
    {
        finish = GetComponent<MeshRenderer>();
        finish.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Scene Name: " + SceneManager.GetActiveScene().name + "\n"+other);
            if(SceneManager.GetActiveScene().name == "Level 1" && !playerInventory.level1Done)
            {
                playerInventory.levelComplete = true;
                playerInventory.playerLevel += 1;
                SceneManager.LoadScene("MenuScene");
                Debug.Log("Player Level Unlock: " + playerInventory.playerLevel);
                playerInventory.level1Done = true;
            }
            else if(SceneManager.GetActiveScene().name == "Level 2" && !playerInventory.level2Done)
            {
                playerInventory.levelComplete = true;
                playerInventory.playerLevel += 1;
                SceneManager.LoadScene("MenuScene");
                Debug.Log("Player Level Unlock: " + playerInventory.playerLevel);
                playerInventory.level2Done = true;
            }
            else if(SceneManager.GetActiveScene().name == "Level 3" && !playerInventory.level3Done)
            {
                playerInventory.levelComplete = true;
                playerInventory.playerLevel += 1;
                SceneManager.LoadScene("MenuScene");
                Debug.Log("Player Level Unlock: " + playerInventory.playerLevel);
                playerInventory.level3Done = true;
                
            }
            else
            {
                playerInventory.levelComplete = true;
                SceneManager.LoadScene("MenuScene");
            }
        }
    }
}
