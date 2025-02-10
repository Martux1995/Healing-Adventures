using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Boss {

	public Transform attackPosition;

	// Use this for initialization
	public override void Start () {
		base.Start();

	}
	
	// Update is called once per frame
	void Update () {
		if (enable) {
			HandleAttack();
			moveToAttackPosition();
		}
		

		HandleAnimations();
	}

	void moveToAttackPosition () {
		if (transform.position.x >= attackPosition.position.x){
			myRigidbody.velocity = new Vector2(-10,0);
		} else {
			myRigidbody.velocity = new Vector2(0,0);
		}
	}

	public override void HandleAttack () {
		// Si tiene ataques disponibles
		if (attackAmountCounter <= attackAmount){
			// Si pasó el tiempo mínimo entre ataques
			if (delayBetweenAttacksCounter >= delayBetweenAttacks) {
				// Si ya hizo el último ataque, detén el ataque
				if (attackAmountCounter == attackAmount) {
					attackTimeCounter = 0;
					delayBetweenAttacksCounter = 0;
					attackAmountCounter = 0;
					enable = false;
					return;
				}
				// Si aun no pasa el tiempo de ataque
				if (attackTimeCounter < attackTime){
					isAttacking = true;
					attackTimeCounter += Time.deltaTime;
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
