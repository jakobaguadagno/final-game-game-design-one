using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerUI : MonoBehaviour
{

    private float timer = 0;
    private int hours = 0;
    private int minutes = 0;
    private int seconds = 0;
    public Text textC;

    void Start()
    {
        textC = GetComponent<Text>();
        timer = Time.timeSinceLevelLoad;
        textC.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void Update()
    {
        timer = Time.timeSinceLevelLoad;
        seconds = (int)(timer % 60);
        minutes = (int)((timer / 60)%60);
        hours = (int)((timer / 3600)%24);
        textC.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
