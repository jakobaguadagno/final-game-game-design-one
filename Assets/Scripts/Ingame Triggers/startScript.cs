using UnityEngine;

public class startScript : MonoBehaviour
{

    private GameObject player;
    private Transform start;

    void Start()
    {
        start = GetComponent<Transform>();
        player = GameObject.Find("Player");
        player.transform.position = start.transform.position;
    }
}
