using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : LifeManager {

	public Slider lifeBar;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		lifeBar.value = ( (100f * actualLifeAmount) / lifeAmount );
	}

	public override void ResetLife (){
		actualLifeAmount = lifeAmount;
		lifeBar.value = ( (100f * actualLifeAmount) / lifeAmount );
	}
}
