using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueWindow : MonoBehaviour {

	public Image theBackground;
	public Text theMessage;
	public Button theButton;

	private byte counter = 0;

	// Use this for initialization
	void Start () {
		Reset();
	}

	public void Show () {
		StartCoroutine("Action");
	}

	public IEnumerator Action () {
		theBackground.gameObject.SetActive(true);
		while (counter < 100) {
			theBackground.color = new Color32(0, 0, 0, ++counter);
			yield return new WaitForSeconds(0.01f);
		}
		counter = 0;
		theMessage.gameObject.SetActive(true);
		theButton.gameObject.SetActive(true);
	}

	public void Reset (){
		theBackground.color = new Color32(0, 0, 0, 0);  
		theMessage.gameObject.SetActive(false);
		theButton.gameObject.SetActive(false);
	}
}
