using UnityEngine;

public class musicScript : MonoBehaviour
{
    public AudioSource music;

    void Update()
    {
        music.volume = playerInventory.audioVolume/4;
    }
}
