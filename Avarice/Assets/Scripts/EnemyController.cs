using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	Wander,
	Follow,
	Die,
    Attack,
    Idle,
};

public class EnemyController : MonoBehaviour
{

	GameObject player;
	public EnemyState currState = EnemyState.Idle;

	public float range;
	public float speed;
    public float attackingRange;
    public float health;
	private bool chooseDir = false;
	private bool dead = false;
	private Vector3 randomDir;
    private bool coolDownAttack = false;
    public float coolDown;
    public bool notInRoom = false;
    public int attackingDamage;
    public AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = health + GameController.Level*2;

    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)
        {
        	case(EnemyState.Wander):
        		Wander();
        		break;
        	case(EnemyState.Follow):
        		Follow();
        		break;
        	case(EnemyState.Die):
        		break;
            case(EnemyState.Attack):
                Attack();
                break;
            case(EnemyState.Idle):
                Idle();
                break;


        }

        if(!notInRoom)
        {
            if(Vector3.Distance(transform.position, player.transform.position) <= attackingRange)
            {
                currState = EnemyState.Attack;
            }
            else if(currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)
    {
    	return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
    	chooseDir = true;
    	yield return new WaitForSeconds(Random.Range(2f,8f));
    	randomDir = new Vector3(0,0,Random.Range(0,360));
    	Quaternion nextRotation = Quaternion.Euler(randomDir);
    	transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));

    }

    void Wander()
    {

    	if(IsPlayerInRange(range))
    	{
    		currState = EnemyState.Follow;
    	}
    }

    void Follow()
    {
        Vector3 target = player.transform.position - transform.position;
        float angle = Mathf.Atan2(target.x,target.y)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    	transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed*Time.deltaTime);
    }

    void Attack()
    {
        if(!coolDownAttack)
        {
            attackSound.Play();
            int damageBoost = (int)Mathf.Floor(GameController.Level/2)+1;
            GameController.DamagePlayer(damageBoost*attackingDamage);
            StartCoroutine(CoolDown());
        }
    }

    void Idle()
    {
        
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);

    }

    public void Damaged()
    {
        health = health - GameController.PlayerDamage;
        if(health <= 0){
            dead = true;
            PlayerMovement.collectedAmount+=5;

            currState = EnemyState.Die;
            Death();
        }
    }
}
