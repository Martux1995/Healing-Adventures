using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public Transform background1;
	public Transform background2;

	public Transform cam;

	private float offset;
	private float currentWidth;

	private bool whichOne = true;

	void Start () {
		offset = background2.localPosition.x - background1.localPosition.x;
		currentWidth = offset;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWidth < cam.position.x){
			if (whichOne)
				background1.localPosition = new Vector3 (background1.localPosition.x + offset * 2,0,10);
			else
				background2.localPosition = new Vector3 (background2.localPosition.x + offset * 2,0,10);

			currentWidth += offset;

			whichOne = !whichOne;
		}
		if (currentWidth > cam.position.x + offset){
			if (whichOne)
				background2.localPosition = new Vector3 (background2.localPosition.x - offset * 2,0,10);
			else
				background1.localPosition = new Vector3 (background1.localPosition.x - offset * 2,0,10);

			currentWidth -= offset;

			whichOne = !whichOne;
		}	
	}
}
