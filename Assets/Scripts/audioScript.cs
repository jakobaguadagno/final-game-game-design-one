using UnityEngine;

public class audioScript : MonoBehaviour
{
    
    public AudioSource pistol;
    public AudioSource shotgun;
    public AudioSource assault;
    public AudioSource playerHit;
    public AudioSource enemyHit;
    public AudioSource bearHit;
    public AudioSource pickUp;
    public AudioSource music;
    public AudioSource buttonClick;
    public AudioSource enemyPunch;
    public AudioSource bearAttack;

    void Start()
    {
        PlayMusic();
    }

    public void PlayPistol()
    {
        pistol.volume = playerInventory.audioVolume;
        pistol.Play();
    }
    public void PlayShotgun()
    {
        shotgun.volume = playerInventory.audioVolume;
        shotgun.Play();
    }
    public void PlayAssault()
    {
        assault.volume = playerInventory.audioVolume;
        assault.Play();
    }
    public void PlayPlayerHit()
    {
        playerHit.volume = playerInventory.audioVolume;
        playerHit.Play();
    }
    public void PlayEnemyHit()
    {
        enemyHit.volume = playerInventory.audioVolume;
        enemyHit.Play();
    }
    public void PlayBearHit()
    {
        bearHit.volume = playerInventory.audioVolume;
        bearHit.Play();
    }
    public void PlayPickUp()
    {
        pickUp.volume = playerInventory.audioVolume;
        pickUp.Play();
    }
    public void PlayMusic()
    {
        music.volume = playerInventory.audioVolume;
        music.Play();
    }
    public void StopMusic()
    {
        music.Stop();
    }
    public void PlayButtonClick()
    {
        buttonClick.volume = playerInventory.audioVolume/2;
        buttonClick.Play();
    }
    public void PlayEnemyPunch()
    {
        enemyPunch.volume = playerInventory.audioVolume;
        enemyPunch.Play();
    }
    public void PlayBearAttack()
    {
        bearAttack.volume = playerInventory.audioVolume;
        bearAttack.Play();
    }
}
