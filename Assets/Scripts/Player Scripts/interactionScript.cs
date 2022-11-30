using UnityEngine;

public class interactionScript : MonoBehaviour
{

    public Transform interactionSpot;
    public float radiusOfInteraction = 3f;

    public Transform player;
    bool hasInteracted = false;

    private float deltaDistance;
    public audioScript pickUpSound;

    public virtual void Interact()
    {
        pickUpSound.PlayPickUp();
    }

    void Start()
    {
        pickUpSound = GameObject.Find("Game Manager/Sound Manager").GetComponent<audioScript>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        deltaDistance = Vector3.Distance(player.position, interactionSpot.position);
		if (!hasInteracted)
		{
			if (deltaDistance <= radiusOfInteraction)
			{
				Interact();
				hasInteracted = true;
			}
		}
        if (hasInteracted)
		{
			if (deltaDistance > radiusOfInteraction)
		    {
                hasInteracted = false;
            }
		}
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
