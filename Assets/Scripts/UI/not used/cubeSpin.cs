using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpin : MonoBehaviour
{

    private Rigidbody rb;
    private Transform pos;
    private int randomN1 = 0;
    private int randomN2 = 0;
    private int randomN3 = 0;
    private Vector3 spin;
    private Vector3 height;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        height = pos.position;
        height.y = 1;
        pos.position = height;
        randomN1 = Random.Range(1, 10);
        randomN2 = Random.Range(1, 10);
        randomN3 = Random.Range(1, 10);
        spin = new Vector3(randomN1, randomN2, randomN3);
        rb.angularVelocity = spin;
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
