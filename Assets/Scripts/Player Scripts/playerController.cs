using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class playerController : MonoBehaviour
{
    private PlayerInput MyInput;
    private CharacterController controller;
    private LineRenderer aimLine;
    private Vector3 playerVelocity, playerPos;
    private bool groundedPlayer;
    private bool isDragging = false;
    private Vector2 dir, cDir, sDir, mLinePos, pLinePos, mousePos2D;
    private Ray rayLine;
    private float gravityValue = -9.81f;
    private Vector3 move, mousePos3D, clickPosStart, clickPosEnd, forwardCamera, rightCamera, fmov, rmov, camRelMov;

    [Header("Player Input Settings")]
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float smoothSpeed = 0.2f;
   // [SerializeField] private int pHealth = 10;
   // [SerializeField] private int pScore = 0;
    
    [Header("Camera Controller Settings")]
    public Camera cameraPlayer;
    public float cameraP = 2f;
    public float ySpeed = 200f;
    private Vector2 cameraYV2temp, cameraYRef, cameraYV2;
    [SerializeField] private float zSpeed = 0.001f;
    [SerializeField] private float maxZ = 5f;
    [SerializeField] private float minZ = 1f;
    [SerializeField] private float cameraZ = 10f;
    [SerializeField] private float cameraY = 0f;
    private float scroll, mouseMovementX;

    //[SerializeField] private float lineMaxDistance = 5;
    //private float lineDistance;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
    }

    void Start()
    {
        aimLine = GetComponent<LineRenderer>();
        aimLine.positionCount = 2;
        MyInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        playerPos = gameObject.transform.position;
    }

    void Update()
    {
        //Debug.Log(EditorWindow.mouseOverWindow);
        playerMovement();
        playerPos = gameObject.transform.position;
        cameraPosFunc();
        aimLineFunc();
    }

    void FixedUpdate()
    {
        dragCameraFunc();
    }
    
    void OnTriggerEnter(Collider collision)
    {
        
    }

    public void Mover(InputAction.CallbackContext context)
    {
        dir = context.action.ReadValue<Vector2>();
    }

    public void Scroller(InputAction.CallbackContext context)
    {
        scroll = context.action.ReadValue<float>();
    }

    public void Dragger(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            if(dir==Vector2.zero)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isDragging = true;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            Cursor.lockState = CursorLockMode.None;
            isDragging = false;
        }
    }

    public void Firer(InputAction.CallbackContext context)
    {
        Ray storageBoxInteraction = cameraPlayer.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(storageBoxInteraction, out RaycastHit storageBoxHit, 10))
        {
            Debug.Log("yo");
        }

    }


    private void playerMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        cDir = Vector2.SmoothDamp(cDir, dir, ref sDir, smoothSpeed);
        move = new Vector3(cDir.x, 0, cDir.y);
        move = movePlayerfromCam(move);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void cameraPosFunc()
    {
        
        cameraZ -= scroll * zSpeed;
        cameraZ = Mathf.Clamp(cameraZ, minZ, maxZ);
        cameraPlayer.transform.position = playerPos - new Vector3(0, -4, 3) * cameraZ;
        cameraPlayer.transform.LookAt(playerPos + Vector3.up * cameraP);
        cameraPlayer.transform.RotateAround(playerPos, Vector3.up, cameraY);
    }

    private void dragCameraFunc()
    {
        if(isDragging&&dir==Vector2.zero)
        {
            mouseMovementX = Mathf.Clamp(Mouse.current.delta.x.ReadValue(), -1, 1);
            cameraYV2temp.x = cameraYV2.x;
        }
        cameraYV2.x -= mouseMovementX * ySpeed;
        cameraYV2 = Vector2.SmoothDamp(cameraYV2temp, cameraYV2, ref cameraYRef, 1f);
        cameraY = cameraYV2.x;
    }

    private void aimLineFunc()
    {
        mousePos3D = Mouse.current.position.ReadValue();
        rayLine = cameraPlayer.ScreenPointToRay(mousePos3D);
        if(Physics.Raycast(rayLine, out RaycastHit hit))
        {
                mousePos3D = hit.point;
        }
        
        //lineDistance = Vector3.Distance(playerPos, mousePos3D);
       
        aimLine.SetPosition(0, new Vector3(playerPos.x, 1, playerPos.z));
        aimLine.SetPosition(1, mousePos3D);
        
    }

    private Vector3 movePlayerfromCam(Vector3 move)
    {
        forwardCamera = cameraPlayer.transform.forward;
        rightCamera = cameraPlayer.transform.right;
        forwardCamera.y = 0;
        rightCamera.y = 0;
        forwardCamera = forwardCamera.normalized;
        rightCamera = rightCamera.normalized;

        fmov = move.z * forwardCamera;
        rmov = move.x * rightCamera;

        camRelMov = fmov + rmov;
        return camRelMov;
    }
}

/*

*/