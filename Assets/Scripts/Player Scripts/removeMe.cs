using UnityEngine;
using UnityEngine.SceneManagement;

public class removeMe : MonoBehaviour
{
    public Transform itemParent;

    public void RemoveItem()
    {
        Destroy(itemParent.gameObject);
    }

}
