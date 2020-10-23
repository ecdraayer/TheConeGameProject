using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
    private Animator animator;
        
    public static int collectedAmount = 0;
    public Text collectedText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        collectedText.text = "Items Collected: " + collectedAmount;
        myRigidbody.velocity = new Vector3(change.x*speed, change.y*speed,0);
        UpdateAnimationAndMove();

        
    }

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
}
