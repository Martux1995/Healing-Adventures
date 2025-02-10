using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalProjectile : Projectile {

	public override void Start () {
		base.Start();
		velocityCounter = startVelocity;
	}

	void Update () {
		while (velocityCounter < finalVelocity) {
			myRigidBody.velocity = new Vector2(-velocityCounter,0);
			velocityCounter += 0.5f;
		}
	}
}
