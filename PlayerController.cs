using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Animator anim;

	public float runningSpeed = 2;
	public float walkingSpeed = 1;
	public float jumpForce;
	float moveVelocity;
	bool isFacingRight = true;

	private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	private int extraJumps;
	public int extraJumpsValue;


	// Use this for initialization
	Collectable cn;
	//EventTrigger et;

	void Flip () 
	{
		Vector3 flipScale;
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();

		flipScale = GetComponent<Rigidbody2D> ().transform.localScale;
		flipScale.x *= -1;

		rigidBody.transform.localScale = flipScale;

		isFacingRight = !isFacingRight;
	}
		

	void Start () 
	{
		extraJumps = extraJumpsValue;
		anim = GetComponent<Animator>();
		cn = GameObject.Find ("Collectables").GetComponent<Collectable> ();
	}

	void FixedUpdate(){
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Coin") {
			cn.CoinCollected ();
			Destroy (other.gameObject);
		}
		if (other.tag == "WinTrigger")
			{
				SceneManager.LoadScene ("WinMenu");
			}
		if (other.tag == "Enemy")
		{
			SceneManager.LoadScene ("LoseMenu");
		}
	}

	// Update is called once per frame
	void Update () 
	{
		moveVelocity = 0.00f;

		if ((Input.GetKey (KeyCode.D)) && (Input.GetKey (KeyCode.LeftShift) == true)) 
		{
			moveVelocity = walkingSpeed;
			anim.SetFloat ("speed", walkingSpeed);
			if (isFacingRight == false) 
			{
				Flip ();
			}
		} 
		else if ((Input.GetKey (KeyCode.D)) && (Input.GetKey (KeyCode.LeftShift) == false)) 
		{
			moveVelocity = runningSpeed;
			anim.SetFloat ("speed", runningSpeed);
			if (isFacingRight == false) 
			{
				Flip ();
			}
		}
		else if ((Input.GetKey (KeyCode.A)) && (Input.GetKey (KeyCode.LeftShift) == true)) {
			moveVelocity = -walkingSpeed;
			anim.SetFloat ("speed", walkingSpeed);
			if (isFacingRight == true) {
				Flip ();
			}
		} else if ((Input.GetKey (KeyCode.A)) && (Input.GetKey (KeyCode.LeftShift) == false)) {
			moveVelocity = -runningSpeed;
			anim.SetFloat ("speed", runningSpeed);
			if (isFacingRight == true) {
				Flip ();
			} 
		} 

		else 
		{
			anim.SetFloat ("speed", 0.0f);
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);


		if (isGrounded == true)
		{
			extraJumps = extraJumpsValue;
			anim.SetBool ("isJumping", false);
		}
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded == false)
		{
			anim.SetBool ("isJumping", true);
		}

		if (Input.GetKeyDown (KeyCode.Space) && extraJumps > 0)
		{
			GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpForce;
			extraJumps--;
		} 

		else if (Input.GetKeyDown (KeyCode.Space) && extraJumps == 0 && isGrounded == true)
		{
			GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpForce;
		}
	}
}