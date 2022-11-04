using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionScript : MonoBehaviour
{

    public Transform interactionSpot;
    public float radiusOfInteraction = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        if(interactionSpot == null)
        {
            interactionSpot = gameObject.transform;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionSpot.position, radiusOfInteraction);
    }

}
