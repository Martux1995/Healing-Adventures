using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	public float startVelocity;
	public float finalVelocity;
	protected float velocityCounter;

	public float damage;
	public GameObject destructionPoint;
	public bool isDestructible;

	protected Rigidbody2D myRigidBody;
	protected Animator myAnimator;

	protected bool resetAnimation;

	public virtual void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
	}

	public void ResetProjectile () {
		resetAnimation = true;
		velocityCounter = startVelocity;
	}

	public void Destroy() {
		if (isDestructible){
			gameObject.SetActive(false);
		}
	}


}
