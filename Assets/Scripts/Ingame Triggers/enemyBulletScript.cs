using UnityEngine;

public class enemyBulletScript : MonoBehaviour
{

    public audioScript sounds;

    void Start()
    {
        sounds = GameObject.Find("Game Manager/Sound Manager").GetComponent<audioScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag != "Enemy") && (other.tag != "Bullet") && (other.tag != "Enemy Bullet"))
        {
            if(other.tag == "Player")
            {
                sounds.PlayPlayerHit();
                playerInventory.playerHealth -= 10;
                Debug.Log(playerInventory.playerHealth);
            }
            Destroy(this.gameObject);
        }
    }
}
