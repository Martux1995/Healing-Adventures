using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	public float movementVelocity;
    private bool runningToFront;

    private string playerGender;
    private string playerColor;

	public float immunityTime;
	private float immunityTimeCounter;

	public AudioSource jumpSound;
	public AudioSource attackSound;
	public AudioSource damagedSound;

	public bool stop;

	public override void Start () {
		base.Start();

        playerGender = PlayerPrefs.GetString("CharacterSelected");
        playerColor = PlayerPrefs.GetString("LevelSelected");

		falling = false;
        
        string path = "Animations/" + playerGender + "_" + playerColor;
        myAnimator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(path);

        ResetPlayer();
        myAnimator.SetBool("Respawn",false);
        stop = false;
	}

	void Update () {
		if (!stop){
			myRigidbody.velocity = new Vector2( (runningToFront ? 1f : -1f) * movementVelocity,myRigidbody.velocity.y);
		} else {
			runningToFront = true;
			myRigidbody.velocity = new Vector2(0,myRigidbody.velocity.y);
		}
		
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		
		HandleImmunity();
		HandleJump();
		HandleAttack();
		HandleAnimations();
	}

	void HandleImmunity () {
		if (immune){
			if (immunityTimeCounter >= immunityTime){
				immune = false;
				immunityTimeCounter = 0;
			} else {
				immunityTimeCounter += Time.deltaTime;
			}
		}
	}

	void HandleJump () {
		if (!stoppedJumping){
			if(jumpTimeCounter > 0 && !isAttacking){
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x,jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			} else {
				falling = true;
				stoppedJumping = true;
			}
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
			falling = false;
		}
	}

	void HandleAttack () {
		if (isAttacking) {
			attackTimeCounter -= Time.deltaTime;
			if (attackTimeCounter <= 0){
				attackTimeCounter = attackTime;
				isAttacking = false;
			}
		}
	}

	void HandleAnimations () {
		myAnimator.SetFloat("Speed", (runningToFront ? 1f : -1f) * myRigidbody.velocity.x);
		myAnimator.SetBool("OnGround",grounded);
		myAnimator.SetBool("Falling",falling);
		myAnimator.SetBool("Attacking",isAttacking);
		myAnimator.SetBool("Defeated",defeated);
		myAnimator.SetBool("Respawn",false);
		
		if (runningToFront && transform.localScale.x < 0 || !runningToFront && transform.localScale.x > 0) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}

	public void PressButton (string action) {
		switch (action){
			case "Jump":
				if(grounded){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x,jumpForce);
					stoppedJumping = false;
					falling = false;
					jumpSound.Play();
				}
				break;
			case "Attack":
				if (attackTimeCounter == attackTime){
					isAttacking = true;
					attackSound.Play();
				}
				break;
		}
	}

	public void ReleaseButton (string action) {
		switch (action){
			case "Jump":
				jumpTimeCounter = 0;
				stoppedJumping = true;
				falling = true;
				break;
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "enemy"){
			Enemy x = other.transform.parent.GetComponent<Enemy>();
			if (isAttacking) {
				x.ReduceLife(attackDamage);
			} else if (!immune && !x.defeated){
				damagedSound.Play();
				ReduceLife(other.transform.parent.GetComponent<Enemy>().attackDamage);
				if (!defeated){
					immune = true;
					StartCoroutine("ImmuneTimeFunction",0);
					myAnimator.SetTrigger("Damaged");
				} else {
					stop = true;
					theGameManager.StopGame("Defeated");
				}
			}
		}
		if (other.tag == "boss") {
			Boss b = other.gameObject.GetComponent<Boss>();
			if (isAttacking) {
				b.ReduceLife(attackDamage);
			} else if (!immune){
				damagedSound.Play();
				ReduceLife(b.attackDamage);
				if (!defeated){
					immune = true;
					StartCoroutine("ImmuneTimeFunction",0);
					myAnimator.SetTrigger("Damaged");
				} else {
					stop = true;
					theGameManager.StopGame("Defeated");
				}
			}
		}
		if (other.tag == "projectile") {
			Projectile x = other.gameObject.GetComponent<Projectile>();
			if (isAttacking && x.isDestructible) {
				x.Destroy();
			} else if (!immune){
				damagedSound.Play();
				ReduceLife(other.gameObject.GetComponent<Projectile>().damage);
				if (!defeated){
					immune = true;
					StartCoroutine("ImmuneTimeFunction",0);
					myAnimator.SetTrigger("Damaged");
				} else {
					stop = true;
					theGameManager.StopGame("Defeated");
				}
			}
		}
		if (other.tag == "killbox"){
			theGameManager.StopGame("Fall");
		}
	}

	public void MovePlayer (float xPosition) {
		if (xPosition < transform.position.x) {
			runningToFront = false;
		} else if (xPosition > transform.position.x){
			runningToFront = true;
		} else {
			stop = true;
		}
	}

	public void ResetPlayer () {
		myAnimator.SetFloat("Speed",0);
		myAnimator.SetBool("OnGround",true);
		myAnimator.SetBool("Falling",false);
		myAnimator.ResetTrigger("Damaged");
		myAnimator.SetBool("Defeated",false);
		myAnimator.SetBool("Attacking",false);
		myAnimator.SetBool("Respawn",true);
		
		attackTimeCounter = attackTime;

		immune = defeated = falling = false;
		stoppedJumping = runningToFront = stop = true;

		mySpriteRenderer.color = new Color32(255,255,255,255);
		
		immunityTimeCounter = 0;

		ResetLife();
		if (theLifeBar is HeartBarManager){
			HeartBarManager h = (HeartBarManager) theLifeBar;
			h.lifeAmount = (int) (maxHealth);
		}
		theLifeBar.ResetLife();
	}

	public void ResurrectPlayer (float delay) {
		immune = true;
		immunityTimeCounter = 0 - delay;
		StartCoroutine("ImmuneTimeFunction",delay);
	}

	public IEnumerator ImmuneTimeFunction (float delay = 0) {
		if (delay >= 0){
			yield return new WaitForSeconds(delay);
		}
		while (immune) {
			//mySpriteRenderer.enabled = false;
			mySpriteRenderer.color = new Color32(255,255,255,0);
			yield return new WaitForSeconds(0.1f);
			//mySpriteRenderer.enabled = true;
			mySpriteRenderer.color = new Color32(255,255,255,255);
			yield return new WaitForSeconds(0.1f);
		}
	}

}