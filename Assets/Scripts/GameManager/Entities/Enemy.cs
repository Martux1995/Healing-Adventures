using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public float score;

	public bool canJump;

	public float minJumpTimeFreq;
	public float maxJumpTimeFreq;
	private float jumpTimeFreq;
	private float jumpTimeFreqCounter;

	public bool reset;
	private bool enterAnimation = false;

	public Transform activePoint;

	public override void Start () {
		base.Start();

		if (FindObjectOfType<GameManager>().isInfiniteLevel) {
			jumpTimeFreq = Random.Range(minJumpTimeFreq,maxJumpTimeFreq);
		}

	}

	void Update () {
		if (reset){
			ResetEnemy();
		}

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		if (activePoint){
			if (activePoint.position.x < transform.position.x){
				myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			} else {
				myRigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
			}
		}
		if (canJump) HandleJump();
		HandleAnimations();
	}

	void HandleJump () {
		if (grounded) {
			if (jumpTimeFreqCounter >= jumpTimeFreq){
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x,jumpForce);

				jumpTimeFreq = Random.Range(minJumpTimeFreq,maxJumpTimeFreq);
				jumpTimeFreqCounter = 0;

			} else {
				jumpTimeCounter = jumpTime;
				
				jumpTimeFreqCounter += Time.deltaTime;
			}
			falling = false;
		} else {
			if(jumpTimeCounter > 0){
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x,jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			} else {
				falling = true;
			}
		}
	}

	void HandleAnimations () {
		myAnimator.SetBool("falling",falling);
		myAnimator.SetBool("grounded",grounded);

		if (defeated && !enterAnimation){
			myAnimator.SetTrigger("defeated");
			myAnimator.SetBool("Respawn",true);
			StartCoroutine("DefeatedEnemy");
			enterAnimation = true;
		} else {
			myAnimator.SetBool("Respawn",false);
		}
	}


	public void ResetEnemy () {
		falling = false;
		grounded = true;
		reset = false;
		ResetLife();
		enterAnimation = false;
		myAnimator.ResetTrigger("defeated");
		myAnimator.SetBool("Respawn",true);
	}

	public IEnumerator DefeatedEnemy () {
		yield return new WaitForSeconds(0.4f);
		theGameManager.IncreaseScore(score);
		yield return new WaitForSeconds(0.6f);
		ResetEnemy();
		gameObject.SetActive(false);
	}


}