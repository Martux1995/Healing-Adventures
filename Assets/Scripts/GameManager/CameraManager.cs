using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	private float movementVelocity = 0;

	private Vector3 initialPosition;

	private bool smoothMovement;
	private float intensity;
	private Vector3 selectedPosition;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		initialPosition = transform.position;
		smoothMovement = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (smoothMovement) {
			if (transform.position.x >= selectedPosition.x) {
				smoothMovement = false;
			} else {
				myRigidbody.velocity = new Vector2(intensity,myRigidbody.velocity.y);
			}
		} else {
			myRigidbody.velocity = new Vector2(movementVelocity,myRigidbody.velocity.y);
		}
	}

	public void MoveCameraAuto (float movementVelocity) {
		this.movementVelocity = movementVelocity;
	}

	public void MoveCameraPosition (Vector3 position) {
		transform.position = position;
	}

	public void MoveCameraPositionSmooth (Vector3 position, float intensity) {
		smoothMovement = true;
		this.intensity = intensity;
		this.selectedPosition = position;
		this.movementVelocity = 0;
	}

	public void StopCamera () {
		this.movementVelocity = 0;
	}

	public void RestartCamera () {
		transform.position = initialPosition;
	}

}
