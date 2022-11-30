using UnityEngine;

public class bulletScript : MonoBehaviour
{

    private TrailRenderer myTrail;
    private float timer = .025f;

    void Start()
    {
        myTrail = GetComponent<TrailRenderer>();
        myTrail.enabled = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            myTrail.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag != "Player") && (other.tag != "Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
