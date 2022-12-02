using UnityEngine;

public class removeMe : MonoBehaviour
{
    public Transform itemParent;

    void Update()
    {
        if(!playerInventory.keepInventory && playerInventory.dead)
        {
            Destroy(itemParent.gameObject);
        }
    }

    public void RemoveItem()
    {
        Destroy(itemParent.gameObject);
    }

}
