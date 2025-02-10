using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBoss{Cell,Fists,Dragon,Twezzers,XForce};

public abstract class Boss : Character {

	public TypeBoss bossName;
	public float movementVelocity;

	public int attackAmount;
	public float delayBetweenAttacks;
	protected int attackAmountCounter = 0;
	protected float delayBetweenAttacksCounter = 0;

	protected bool defeatedAnimation;

	// Use this for initialization
	public override void Start () {
		base.Start();
		ResetBoss();
	}

	public abstract void HandleAttack ();

	public void HandleAnimations () {
		if (bossName != TypeBoss.Cell) {
			if (isAttacking) {
				myAnimator.SetTrigger("Attack");
			} else {
				myAnimator.ResetTrigger("Attack");
			}
		}
		if (defeated && !defeatedAnimation) {
			defeatedAnimation = true;
			HideHealthBar();
			StartCoroutine("DefeatAnimation");
		}
	}

	public IEnumerator DefeatAnimation () {
		myAnimator.SetBool("Defeated",true);
		yield return new WaitForSeconds(2f);
		myAnimator.SetBool("Defeated",false);
	}

	public void ShowHealthBar () {
		theLifeBar.gameObject.SetActive(true);
	}

	public void HideHealthBar () {
		theLifeBar.gameObject.SetActive(false);
	}

	public void MoveBoss (float speed) {
		movementVelocity = speed;
		//movementVelocity = (isAttacking ? velocity : -velocity);
	}

	public void Attack () {
		attackAmountCounter = 0;
		enable = false;
		isAttacking = true;
	}

	public void ResetAttack () {
		attackTimeCounter = 0;
		delayBetweenAttacksCounter = 0;
		attackAmountCounter = 0;
		isAttacking = false;
		enable = false;
	}

	public void ResetBoss () {
		theLifeBar.lifeAmount = (int) (maxHealth);
		ResetLife();
		HideHealthBar();
		enable = false;
		defeatedAnimation = false;;
		
	}
}
