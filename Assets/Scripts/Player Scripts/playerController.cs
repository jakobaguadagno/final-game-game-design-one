using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private PlayerInput MyInput;
    private CharacterController controller;
    private LineRenderer aimLine;
    public GameObject bullet;
    private GameObject tempB;
    private Vector3 playerVelocity, playerPos;
    private bool groundedPlayer;
    private bool fired = false;
    private bool isDragging = false;
    private Vector2 dir, cDir, sDir, mLinePos, pLinePos, mousePos2D;
    private Ray rayLine, shotLine;
    private float gravityValue = -9.81f;
    private Vector3 move, facingDirection, mousePos3D, clickPosStart, clickPosEnd, forwardCamera, rightCamera, fmov, rmov, mousePosPoint, camRelMov, shotDirection;
    private Rigidbody tempR;
    private Quaternion shotQ ,shotGunQ2,shotGunQ3,shotGunQ4,shotGunQ5;
    private GameObject tempSGB;
    private GameObject tempSGB2;
    private GameObject tempSGB3;
    private GameObject tempSGB4;
    private GameObject tempSGB5;
    private Rigidbody tempRSG,tempRSG2,tempRSG3,tempRSG4,tempRSG5;
    playerInventory inv;
    public GameObject playerGO, playerCenter;
    public float shootCooldown = 0;
    private int playerWeapon;
    public Animator ani;
    public audioScript sounds;
    public GameObject inventoryUI;
    public Slider s;

    [Header("Player Input Settings")]
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float smoothSpeed = 0.2f;
    
    [Header("Camera Controller Settings")]
    public Camera cameraPlayer;
    public float cameraP = 2f;
    public float ySpeed = 200f;
    private Vector2 cameraYV2temp, cameraYRef, cameraYV2;
    [SerializeField] private float zSpeed = 0.001f;
    [SerializeField] private float maxZ = 5f;
    [SerializeField] private float minZ = 2f;
    [SerializeField] private float cameraZ = 10f;
    [SerializeField] private float cameraY = 0f;
    private float scroll, mouseMovementX;

    public MeshRenderer pistol, shotgun, assualt;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Start()
    {
        inventoryUI = GameObject.Find("Game Manager/Canvas/InventoryUI");
        sounds = GameObject.Find("Game Manager/Sound Manager").GetComponent<audioScript>();
        aimLine = GetComponent<LineRenderer>();
        aimLine.positionCount = 2;
        MyInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        playerPos = gameObject.transform.position;
        switch (playerInventory.playerWeapon)
        {
            case 1:
                pistol.enabled = true;
                shotgun.enabled = false;
                assualt.enabled = false;
                break;
            case 2:
                pistol.enabled = false;
                shotgun.enabled = true;
                assualt.enabled = false;
                break;
            case 3:
                pistol.enabled = false;
                shotgun.enabled = false;
                assualt.enabled = true;
                break;
        }
    }

    void Update()
    {
        if(!playerInventory.dead)
        {
            sHealth(playerInventory.playerHealth);
        }
        playerMovement();
        playerPos = gameObject.transform.position;
        cameraPosFunc();
        aimLineFunc();
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if(fired==true && shootCooldown <= 0)
        {
            Shoot();
            if(playerInventory.playerWeapon != 1 && playerInventory.playerWeapon != 2)
            {
                shootCooldown += .1f;
            }
            else
            {
                shootCooldown += 1f;
            }
            
        }
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
        if(context.action.phase == InputActionPhase.Started)
        {
            fired = true;
            
        }
        if(context.action.phase == InputActionPhase.Canceled)
        {
            fired = false;
        }
    }


    private void playerMovement()
    {
        shotDirection = Mouse.current.position.ReadValue();
        shotLine = cameraPlayer.ScreenPointToRay(shotDirection);
        if(Physics.Raycast(shotLine, out RaycastHit hit))
        {
            mousePos3D = hit.point;
        }
        mousePos3D.y = 0;
        playerPos.y = 0;
        shotDirection = (mousePos3D - playerPos).normalized;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        cDir = Vector2.SmoothDamp(cDir, dir, ref sDir, smoothSpeed);
        move = new Vector3(cDir.x, 0, cDir.y);
        move = movePlayerfromCam(move);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if(isDragging)
        {
            move = Vector3.zero;
            controller.Move(Vector3.zero);
        }
        gameObject.transform.forward = shotDirection;
        ani.SetFloat("speed", controller.velocity.z);
        ani.SetFloat("lrspeed", controller.velocity.x);
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

    public void Shoot()
    {
        if(!inventoryUI.activeSelf)
        {
            switch (playerInventory.playerWeapon)
        {
            case 1:
                sounds.PlayPistol();
                shotDirection = Mouse.current.position.ReadValue();
                shotLine = cameraPlayer.ScreenPointToRay(shotDirection);
                if(Physics.Raycast(shotLine, out RaycastHit hit))
                {
                    mousePos3D = hit.point;
                }
                shotDirection = (mousePos3D - playerPos).normalized;
                shotQ = Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.z));
                tempB = Instantiate(bullet, playerCenter.transform.position, shotQ);
                tempR = tempB.GetComponent<Rigidbody>();
                tempR.AddForce(tempR.transform.forward * 4000f);
                Destroy(tempB, .8f);
                break;
            case 2:
                sounds.PlayShotgun();
                shotDirection = Mouse.current.position.ReadValue();
                shotLine = cameraPlayer.ScreenPointToRay(shotDirection);
                if(Physics.Raycast(shotLine, out RaycastHit hit1))
                {
                    mousePos3D = hit1.point;
                }
                shotDirection = (mousePos3D - playerPos).normalized;
                shotQ = Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.z));
                shotGunQ2 = shotQ * Quaternion.Euler(Vector3.up * 5);
                shotGunQ3 = shotQ * Quaternion.Euler(Vector3.up * 10);
                shotGunQ4 = shotQ * Quaternion.Euler(Vector3.up * -10);
                shotGunQ5 = shotQ * Quaternion.Euler(Vector3.up * -5);
                tempSGB = Instantiate(bullet, playerCenter.transform.position, shotQ);
                tempSGB2 = Instantiate(bullet, playerCenter.transform.position, shotGunQ2);
                tempSGB3 = Instantiate(bullet, playerCenter.transform.position, shotGunQ3);
                tempSGB4 = Instantiate(bullet, playerCenter.transform.position, shotGunQ4);
                tempSGB5 = Instantiate(bullet, playerCenter.transform.position, shotGunQ5);
                tempRSG = tempSGB.GetComponent<Rigidbody>();
                tempRSG2 = tempSGB2.GetComponent<Rigidbody>();
                tempRSG3 = tempSGB3.GetComponent<Rigidbody>();
                tempRSG4 = tempSGB4.GetComponent<Rigidbody>();
                tempRSG5 = tempSGB5.GetComponent<Rigidbody>();
                tempRSG.AddForce(tempRSG.transform.forward * 4000f);
                tempRSG2.AddForce(tempRSG2.transform.forward * 4000f);
                tempRSG3.AddForce(tempRSG3.transform.forward * 4000f);
                tempRSG4.AddForce(tempRSG4.transform.forward * 4000f);
                tempRSG5.AddForce(tempRSG5.transform.forward * 4000f);
                Destroy(tempSGB, .8f);
                Destroy(tempSGB2, .8f);
                Destroy(tempSGB3, .8f);
                Destroy(tempSGB4, .8f);
                Destroy(tempSGB5, .8f);
                Debug.Log("Yo");
                break;
            case 3:
                sounds.PlayAssault();
                shotDirection = Mouse.current.position.ReadValue();
                shotLine = cameraPlayer.ScreenPointToRay(shotDirection);
                if(Physics.Raycast(shotLine, out RaycastHit hit3))
                {
                    mousePos3D = hit3.point;
                }
                shotDirection = (mousePos3D - playerPos).normalized;
                shotQ = Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.z));
                tempB = Instantiate(bullet, playerCenter.transform.position, shotQ);
                tempR = tempB.GetComponent<Rigidbody>();
                tempR.AddForce(tempR.transform.forward * 4000f);
                Destroy(tempB, .8f);
                break;
        }
        }
        
    }
    private void sHealth(int h)
    {
        s.value = h;
    }
}

/*
groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        cDir = Vector2.SmoothDamp(cDir, dir, ref sDir, smoothSpeed);
        move = new Vector3(cDir.x, 0, cDir.y);
        move = movePlayerfromCam(move);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if(isDragging)
        {
            move = Vector3.zero;
            controller.Move(Vector3.zero);
        }
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
*/