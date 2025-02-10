using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

	public GameObject destructionPoint;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < destructionPoint.transform.position.x){
			gameObject.SetActive(false);
		}
	}
}
