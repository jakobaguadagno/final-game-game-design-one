using UnityEngine;
using UnityEngine.InputSystem;

public class mouseAim : MonoBehaviour
{
    private Vector3 mousePos3D;
    private Ray rayLine;
    public Camera cameraPlayer;
    
    void Start()
    {
        
    }

    void Update()
    {
        mousePos3D = Mouse.current.position.ReadValue();
        rayLine = cameraPlayer.ScreenPointToRay(mousePos3D);
        if(Physics.Raycast(rayLine, out RaycastHit hit))
        {
                mousePos3D = hit.point;
        }
        mousePos3D.y = 2;
        gameObject.transform.position = mousePos3D;
    }
}
