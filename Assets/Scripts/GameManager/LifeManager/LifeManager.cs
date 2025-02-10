using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LifeManager : MonoBehaviour {

	public int lifeAmount;
	protected int actualLifeAmount;

	public virtual void Start () {
		ResetLife();
	}

	public void RestoreLife (int hearthAmount){
		if (actualLifeAmount + hearthAmount > lifeAmount){
			actualLifeAmount = lifeAmount;
		} else {
			actualLifeAmount += hearthAmount;
		}
	}

	public void ReduceLife  (int lifeAmount){
		if (actualLifeAmount - lifeAmount <= 0){
			actualLifeAmount = 0;
		} else {
			actualLifeAmount -= lifeAmount;
		}
	}

	public abstract void ResetLife ();

}
