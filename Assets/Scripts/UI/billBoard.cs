using UnityEngine;

public class billBoard : MonoBehaviour
{
    
    public Camera cameraPlayer;
    public RectTransform rect;

    void Start()
    {
        cameraPlayer = GameObject.Find("Player/Main Camera").GetComponent<Camera>();
    }
   
    void Update()
    {
        rect.transform.LookAt(transform.position + cameraPlayer.transform.rotation * Vector3.forward, cameraPlayer.transform.rotation * Vector3.up);
    }
}
