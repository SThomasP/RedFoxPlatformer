using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{

    public float acceleration;
    public float maxSpeed;
    public float jumpPower;
	public float wallJumpPower;
	public float crouchJumpPower;
	public bool disabled = false;
	public CompositeCollider2D stage;


	private Collider2D coll; 
    private Animator anim;
	private SpriteRenderer sr;
    private Rigidbody2D rb2d;
	private float crouchTime;
    bool facingRight = true;
	bool canDoubleJump = false;
	bool isGrounded = true;
	bool isWalled = false;
	float timeSinceLastStep;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void TurnLeft(){
		sr.flipX = true;
	}
	void TurnRight(){
		sr.flipX = false;
	}

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name == "Floors")
		{
			anim.SetBool ("Grounded", true);
			isGrounded = true;
			Debug.Log ("Grounded");
		}

		if (theCollision.gameObject.name == "Walls")
		{
			isWalled = true;
			Debug.Log ("Walled");
		}
	}

	void OnCollisionExit2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name == "Floors")
		{
			anim.SetBool ("Grounded", false);
			isGrounded = false;
			Debug.Log ("Not grounded");
		}

		if (theCollision.gameObject.name == "Walls")
		{
			isWalled = false;
			Debug.Log ("dewalled");
		}
	}

	// On death
	public void Die(){
		anim.SetTrigger ("Die");
		disabled = true;
		SoundManagerScript.PlaySound ("death");
		GameObject ds = GetComponent<Transform> ().GetChild (3).gameObject;
		ds.SetActive (true);
	}

	// Not used
	public bool getFacingRight() {
		return facingRight;
	}

	// Player jump, used within the Update() function
	void jump(Vector2 vector) {
		SoundManagerScript.PlaySound ("jump");
		anim.SetTrigger ("Jump");
		rb2d.AddForce (vector, ForceMode2D.Impulse);

	}

	// Update is called once per frame
    void Update()
    {
		bool onStage = coll.IsTouching (stage);
		if (!disabled) {
			// Jumping mechanic
			if (Input.GetButtonDown ("Jump")) {
				// Single jump
				if (isGrounded) {
					jump (new Vector2 (0f, jumpPower - rb2d.velocity.y));
					canDoubleJump = true;
					Debug.Log ("jump");
				}
			// Wall jump
			else if (!(isGrounded) && isWalled) {
					if (facingRight) {
						jump(new Vector2 (0.5f * -wallJumpPower, wallJumpPower - rb2d.velocity.y));
						Debug.Log ("wall jump right");
					} else {
						jump(new Vector2 (0.5f * wallJumpPower, wallJumpPower - rb2d.velocity.y));
						Debug.Log ("wall jump left");
					}
				}
			// Double jump
			else if (!(isGrounded) && canDoubleJump) {
					jump(new Vector2 (0f, jumpPower - rb2d.velocity.y));
					canDoubleJump = false;
					Debug.Log ("double jump");
				}

			}
			// Crouching
			if (Input.GetButton ("Crouch") && isGrounded) {
				anim.SetBool ("Crouched", true);
				if (crouchTime < 1.0f) {
					crouchTime += Time.deltaTime;
					anim.SetFloat("Power", 5.0f * crouchTime);
				}
			// Crouch jump
			} else if (Input.GetButtonUp ("Crouch") && isGrounded) {
				anim.SetBool ("Crouched", false);
				float boosted = crouchJumpPower * (1 + 2.0f * crouchTime);
				rb2d.AddForce (new Vector2 (0f, boosted));
				Debug.Log (boosted);
				crouchTime = 0.0f;
				anim.SetFloat ("Power", 1.0f);
				anim.SetTrigger ("Jump");
				SoundManagerScript.PlaySound ("super jump");
			} else {
				// Moving mechanic
				float movement = Input.GetAxis ("Horizontal");
				if (movement * rb2d.velocity.x < maxSpeed) {
					rb2d.AddForce (Vector2.right * movement * acceleration);
				}

				if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
					rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
				}
				// Falling
				if (rb2d.velocity.y < 0.0f & !(isGrounded)) {
					anim.SetTrigger ("Fall");
				}
				float speed = rb2d.velocity.x;
				// Facing right
				if (speed > 0.0f) {
					facingRight = true;
					TurnRight ();
					anim.SetBool ("Running", true);
				}

			// Facing left
			else if (speed < 0.0f) {
					facingRight = false;
					TurnLeft ();
					anim.SetBool ("Running", true);
				} else {

					anim.SetBool ("Running", false);
				}
			}
		}
		if (Input.GetButtonDown("Reset")){
			SceneManager.LoadScene (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		}
    }


}
