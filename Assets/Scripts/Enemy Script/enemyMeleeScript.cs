using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyMeleeScript : MonoBehaviour
{
    public float lRadius = 10f;
	private float deltaDistance;

	Transform player;
	NavMeshAgent enemy;
    Animator animator;
    BoxCollider hitBox;

    Vector3 direction;
    Quaternion lookRotation;

    private IEnumerator attackCo;

    private float attackC;
    private bool inRange;

    private int health = 100;
    private bool dead = false;
    private bool animationStart = false;
    public GameObject inventoryUI;

    private float speed;
    public Slider s;

    public audioScript sounds;

	void Start () 
	{
        inventoryUI = GameObject.Find("Game Manager/Canvas/InventoryUI");
        sounds = GameObject.Find("Game Manager/Sound Manager").GetComponent<audioScript>();
		player = GameObject.Find("Player").transform;
		enemy = GetComponent<NavMeshAgent>();
        hitBox = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        attackCo = AttackingPlayer();
        StartCoroutine(attackCo);
	}
	

	void Update () 
    {
        sHealth(health);
        if(health<=0)
        {
            Destroy(hitBox);
            dead = true;
        }
        animator.SetBool("Alive", !dead);
        if(!dead)
        {
            speed = enemy.velocity.magnitude;
            if(!inRange)
            {
                animator.SetFloat("Speed", speed, .1f, Time.deltaTime);
            }
            deltaDistance = Vector3.Distance(player.position, transform.position);
            inRange = deltaDistance <= enemy.stoppingDistance;
            

            if (deltaDistance <= lRadius)
            {

                enemy.SetDestination(player.position);


                if (inRange)
                {
                    animator.SetFloat("Speed", 0, 0f, Time.deltaTime);
                    direction = (player.position - transform.position).normalized;
                    lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                    enemy.velocity = Vector3.zero;
                }
            }
        }
        else
        {
            StopCoroutine(attackCo);
            if(!animationStart)
            {
                animator.SetTrigger("Death");
                animationStart = true;
            }
            Destroy(enemy);
        }
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lRadius);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(!dead)
        {
            if(other.tag == "Bullet")
            {
                sounds.PlayEnemyHit();
                health -= playerInventory.playerDamage;
                Debug.Log(health);
            }
        }
    }

    IEnumerator AttackingPlayer()
	{
        while(true)
        {
            if(inRange && !inventoryUI.activeSelf)
            {
                animator.SetTrigger("Attack1");
                sounds.PlayEnemyPunch();
                playerInventory.playerHealth -= 20;
                Debug.Log(playerInventory.playerHealth);
            }
            yield return new WaitForSeconds(2f);
        }
	}

    private void sHealth(int h)
    {
        s.value = h;
    }
}
