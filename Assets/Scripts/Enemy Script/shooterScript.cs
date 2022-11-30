using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

public class shooterScript : MonoBehaviour
{
    public float lRadius = 10f;
	private float deltaDistance;

	public Transform player, myHead, playerHead;
	NavMeshAgent enemy;
    Animator animator;
    BoxCollider hitBox;
    public MeshRenderer pistol;
    RigBuilder rb;
    public GameObject enemyBullet;
    public audioScript sounds;
    
    private GameObject tempB;
    private Rigidbody tempR;

    Vector3 direction, enemyPos;
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

	void Start () 
	{
        inventoryUI = GameObject.Find("Game Manager/Canvas/InventoryUI");
        sounds = GameObject.Find("Game Manager/Sound Manager").GetComponent<audioScript>();
        player = GameObject.Find("Player").transform;
		playerHead = GameObject.Find("Player/Head").transform;
		enemy = GetComponent<NavMeshAgent>();
        hitBox = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        rb = GetComponent<RigBuilder>();
        attackCo = AttackingPlayer();
        StartCoroutine(attackCo);
	}
	

	void Update () 
    {
        sHealth(health);
        enemyPos = myHead.transform.position;
        deltaDistance = Vector3.Distance(player.position, transform.position);
        if(!dead)
        {
            inRange = deltaDistance <= enemy.stoppingDistance;
            animator.SetFloat("Speed", (enemy.velocity.magnitude/10), .1f, Time.deltaTime);
        }
        if(health<=0)
        {
            Destroy(hitBox);
            dead = true;
        }
        animator.SetBool("Alive", !dead);
        if(!dead)
        {
            direction = playerHead.position - enemyPos;
            if (Physics.Raycast(enemyPos, direction, out RaycastHit hit1)) 
            {
                if (hit1.transform == player) 
                {
                    if(!inRange)
                    {
                        enemy.isStopped = true;
                        enemy.destination = player.position;
                        enemy.isStopped = false;
                    }
                    if (inRange)
                    {
                        direction = (player.position - transform.position).normalized;
                        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                        transform.rotation = lookRotation;
                        enemy.velocity = Vector3.zero;
                    }
                }
                else
                {
                    enemy.isStopped = true;
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
            pistol.enabled = false;
            rb.enabled = false;
        }
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lRadius);
        //Gizmos.DrawLine(myHead.position, player.position);
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
                sounds.PlayPistol();
                Vector3 shotDirection = player.transform.position;
                shotDirection = (player.position - transform.position).normalized;
                Quaternion shotQ = Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.z));
                tempB = Instantiate(enemyBullet, myHead.position, shotQ);
                tempR = tempB.GetComponent<Rigidbody>();
                tempR.AddForce(tempR.transform.forward * 4000f);
                Destroy(tempB, .8f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
	}

    private void sHealth(int h)
    {
        s.value = h;
    }
}

/*
attackC -= Time.deltaTime;
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



                shotDirection = player.transform.position;
                if(Physics.Raycast(shotLine, out RaycastHit hit))
                {
                    mousePos3D = hit.point;
                }
                shotDirection = (mousePos3D - playerPos).normalized;
                shotQ = Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.z));
                tempB = Instantiate(bullet, playerCenter.transform.position, shotQ);
                tempR = tempB.GetComponent<Rigidbody>();
                tempR.AddForce(tempR.transform.forward * 4000f);
                Destroy(tempB, .8f);
*/
