using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Boss {

	public ProjectileGenerator[] projectileSpawn;

	private float standByPosition;
	private float attackPosition;

	// Use this for initialization
	public override void Start () {
		base.Start();
		standByPosition = transform.position.x;
		attackPosition = transform.position.x + 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (enable) {
			HandleAttack();
		}

		HandleAnimations ();

		if (delayBetweenAttacksCounter < delayBetweenAttacks/2f){
			movementVelocity = -5;
		} else if (delayBetweenAttacksCounter >= delayBetweenAttacks/2f){
			movementVelocity = 5;
		}

		if (isAttacking && transform.position.x >= attackPosition ){
			myRigidbody.velocity = new Vector2(0,0);
		} else if (!isAttacking && transform.position.x <= standByPosition) {
			myRigidbody.velocity = new Vector2(0,0);
		} else {
			myRigidbody.velocity = new Vector2(movementVelocity,0);
		}
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
					if ( (float) (Random.Range(0,10f)) < 5f ){
						projectileSpawn[Random.Range(0,projectileSpawn.Length)].GenerateProjectile(attackDamage);
					}
				} else {
					isAttacking = false;
					attackTimeCounter = 0;
					attackAmountCounter += 1;
					delayBetweenAttacksCounter = 0;
				}

			} else {
				delayBetweenAttacksCounter += Time.deltaTime;
			}
		}
	}
}
