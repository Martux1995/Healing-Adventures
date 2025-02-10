using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProjectile : Projectile {

	public override void Start () {
		base.Start();
	}

	void Update () {
		if (resetAnimation){
			myAnimator.SetTrigger("Fire");
			resetAnimation = false;
		}
		myRigidBody.velocity = new Vector2(-finalVelocity,0);
	}

}
