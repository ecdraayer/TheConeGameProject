using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector2 change;
    private Animator animator;

    public Camera cam;
    private Vector2 mousePos;

    //public HealthBar healthBar;
    public int currentHealth;
    public int maxHealth = 25;
        
    public Text collectedText;
    public static int collectedAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        collectedText.text = "Treasure Collected: " + collectedAmount;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //myRigidbody.velocity = new Vector3(change.x*speed, change.y*speed,0);
        //UpdateAnimationAndMove();

        
    }

    void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + change.normalized * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - myRigidbody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        myRigidbody.rotation = angle;
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= 1;
        //healthBar.SetHealth(currentHealth);
    }

    /*
    void UpdateAnimationAndMove()
    {
      if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }  
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(
           transform.position + change.normalized * speed * Time.deltaTime
        );
    }
    */
}
