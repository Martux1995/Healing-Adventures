using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirProjectile : Projectile {

	public bool grounded;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float groundCheckRadius;

	public override void Start () {
		base.Start();
		myAnimator = GetComponent<Animator>();
	}

	void Update () {

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		if (!grounded){
			myAnimator.SetTrigger("Fire");
			myRigidBody.velocity = new Vector2(0,-finalVelocity);
		} else {
			myRigidBody.velocity = new Vector2(0,0);
			myAnimator.SetTrigger("OnGround");
		}
	}

}
