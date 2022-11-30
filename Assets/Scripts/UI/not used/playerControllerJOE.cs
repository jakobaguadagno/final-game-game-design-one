using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControllerJOE : MonoBehaviour
{

    [Header("Player Input Settings")]
    private PlayerInput MyInput;
    public Slider s;
    private Rigidbody rb;
    private Vector3 dir = new Vector3(0,0,0);
    [SerializeField] private int pSpeed = 500;
    [SerializeField] private int pRSpeed = 50;
    [SerializeField] private int pHealth = 10;
    [SerializeField] private int pScore = 0;
    private bool IsRotateRight = false;
    private bool IsRotateLeft = false;
    private int[] inventory;
    public GameObject pauseMenu;
    public GameObject ingameMenu;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public GameObject slot6;
    public GameObject slot7;
    public GameObject slot8;
    public GameObject slot9;
    private bool pause = false;
    public Text textS;
    public Text textSP;

    void Start()
    {
        inventory = new int[9];
        for (int i = 0; i < 9; i++)
        {
            inventory[i] = 0;
        }
        ingameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);
        slot4.SetActive(false);
        slot5.SetActive(false);
        slot6.SetActive(false);
        slot7.SetActive(false);
        slot8.SetActive(false);
        slot9.SetActive(false);
        MyInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SlowUpdate());
        sHealth(pHealth);
    }

    void Update()
    {
        if (pHealth == 0)
        {
            SceneManager.LoadScene("MenuScene");
        }
        sHealth(pHealth);
        textS.text = pScore.ToString();
        textSP.text = pScore.ToString();
    }

    private void sHealth(int h)
    {
        s.value = h;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            pScore += 1;
        }
        if (collision.gameObject.tag == "PickUp")
        {
            for (int i = 0; i < 9; i++)
            {
                if(inventory[i]==0)
                {
                    switch(i)
                    {
                        case 0:
                            slot1.SetActive(true);
                            break;
                        case 1:
                            slot2.SetActive(true);
                            break;
                        case 2:
                            slot3.SetActive(true);
                            break;
                        case 3:
                            slot4.SetActive(true);
                            break;
                        case 4:
                            slot5.SetActive(true);
                            break;
                        case 5:
                            slot6.SetActive(true);
                            break;
                        case 6:
                            slot7.SetActive(true);
                            break;
                        case 7:
                            slot8.SetActive(true);
                            break;
                        case 8:
                            slot9.SetActive(true);
                            break;
                        default:
                            break;

                    }
                    inventory[i]=1;
                    i=9;
                }
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            pHealth -= 1;
        }
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            IsRotateRight = true;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            IsRotateRight = false;
        }
    }

    public void RotateLeft(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            IsRotateLeft = true;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            IsRotateLeft = false;
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Pause()
    {
        if(!pause)
        {
            pauseMenu.SetActive(true);
            ingameMenu.SetActive(false);
            pause = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            ingameMenu.SetActive(true);
            pause = false;
        }
        
    }

    public void GreenButton1()
    {
        inventory[0] = 0;
        slot1.SetActive(false);
        pScore += 1;
    }
    public void GreenButton2()
    {
        inventory[1] = 0;
        slot2.SetActive(false);
        pScore += 1;
    }
    public void GreenButton3()
    {
        inventory[2] = 0;
        slot3.SetActive(false);
        pScore += 1;
    }
    public void GreenButton4()
    {
        inventory[3] = 0;
        slot4.SetActive(false);
        pScore += 1;
    }
    public void GreenButton5()
    {
        inventory[4] = 0;
        slot5.SetActive(false);
        pScore += 1;
    }
    public void GreenButton6()
    {
        inventory[5] = 0;
        slot6.SetActive(false);
        pScore += 1;
    }
    public void GreenButton7()
    {
        inventory[6] = 0;
        slot7.SetActive(false);
        pScore += 1;
    }
    public void GreenButton8()
    {
        inventory[7] = 0;
        slot8.SetActive(false);
        pScore += 1;
    }
    public void GreenButton9()
    {
        inventory[8] = 0;
        slot9.SetActive(false);
        pScore += 1;
    }

    public IEnumerator SlowUpdate()
    {
        while(true)
        {
            if(dir == Vector3.forward)
            {
                rb.AddForce(transform.forward * pSpeed);
            }
            if(dir == -Vector3.forward)
            {
                rb.AddForce(-transform.forward * pSpeed);
            }
            if(IsRotateRight)
            {
                rb.AddTorque(Vector3.up*pRSpeed);
            }
            if(IsRotateLeft)
            {
                rb.AddTorque(Vector3.up*-pRSpeed);
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            yield return new WaitForSeconds(.016f);
        }
    }
}
