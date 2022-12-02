using UnityEngine.UI;
using UnityEngine;

public class soundControl : MonoBehaviour
{

    public Slider s;

    void Start()
    {
        s.value = playerInventory.audioVolume;
        Debug.Log("Sound: " +  s.value);
    }

    void Update()
    {
        playerInventory.audioVolume = s.value;
    }
}
