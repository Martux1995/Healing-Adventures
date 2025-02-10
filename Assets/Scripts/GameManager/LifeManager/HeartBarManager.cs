using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBarManager : LifeManager {

	public Image[] heart;

	public override void Start () {
		base.Start();
	}

	void Update () {
		for (int i = lifeAmount; i > 0; i--){
			if (i > actualLifeAmount) {
				heart[i-1].enabled = false;
			} else {
				heart[i-1].enabled = true;
			}
		}
	}

	public override void ResetLife (){
		actualLifeAmount = lifeAmount;
		for (int i = 0; i< heart.Length; i++){
			if (lifeAmount >= i + 1){
				heart[i].enabled = true;
			} else {
				heart[i].enabled = false;
			}
		}
	}

}