using UnityEngine;

public class playerDoorOpen : interactionScript
{

    public GameObject door;
    private bool interacted = false;

    public override void Interact()
    {
        if(!interacted)
        {
            base.Interact();
            openDoor();
            interacted = true;
        }
    }

    void openDoor()
    {
        door.transform.rotation *= Quaternion.Euler(Vector3.up * 90);
    }
}
