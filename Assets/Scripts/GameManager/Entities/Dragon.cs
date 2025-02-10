using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Boss {

	public ProjectileGenerator projectileSpawn;
	public bool launchProjectile;
	public bool launched = true;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (enable) {
			HandleAttack();
		}

		myRigidbody.velocity = new Vector2(movementVelocity,0);

		HandleAnimations ();
	}

	public override void HandleAttack () {
		if (attackAmountCounter <= attackAmount){
			if (delayBetweenAttacksCounter >= delayBetweenAttacks) {
				if (attackAmountCounter == attackAmount) {
					attackTimeCounter = 0;
					delayBetweenAttacksCounter = 0;
					attackAmountCounter = 0;
					enable = false;
					return;
				}
				if (attackTimeCounter < attackTime){
					isAttacking = true;
					attackTimeCounter += Time.deltaTime;
				} else {
					isAttacking = false;
					launched = false;
					attackTimeCounter = 0;
					attackAmountCounter += 1;
					delayBetweenAttacksCounter = 0;
				}

			} else {
				delayBetweenAttacksCounter += Time.deltaTime;
			}
		}
		if (launchProjectile && !launched){
			projectileSpawn.GenerateProjectile(attackDamage);
			launched = true;
		}
	}
}
