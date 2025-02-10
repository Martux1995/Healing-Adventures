using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoxDialog : MonoBehaviour {

	public GameObject theDialog;
	public Image theContinueIndicator;
	
	public Text theText;

	public string[] theLines;
	private string lastLine = "";
	private string theLineToWrite;
	private int lineCounter = 0;
	private int caracterIndex = 0;

	private bool playing = false;

	private float speed = 0.05f;

	private bool released = true;

	private bool omit = false;
	// Use this for initialization
	public bool finish = false;

	private bool enable = false;

	void Start () {
		theDialog.SetActive(false);
		theContinueIndicator.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (enable){
			if (released){
				if (playing && (Input.touchCount >= 1 || Input.GetMouseButtonDown(0) )) {
					omit = true;	
					released = false;
				} else if (!playing && (Input.touchCount >= 1 || Input.GetMouseButtonDown(0))) {
					if (lineCounter < theLines.Length - 1) {
						NextString();
					} else {
						DestroyDialog();
					}
					released = false;
				}

			} else {
				released = (Input.touchCount <= 0 || Input.GetMouseButtonUp(0));
			}

		}

	}	

	public void ShowDialog () {
		theDialog.SetActive(true);
		theLineToWrite = theLines[0];
		lineCounter = 0;
		if (theLines.Length > 1) {
			theLineToWrite += "\n";
			theLineToWrite += theLines[1];
			lineCounter = 1;
		}
		theText.text = "";
		enable = true;
		playing = true;
		StartCoroutine("PlayLine");
	}

	public void NextString () {
		theText.text = lastLine = theLines[lineCounter];
		theLineToWrite = "\n" + theLines[lineCounter+1];
		lineCounter++;
		playing = true;
		StartCoroutine("PlayLine");
	}

	public void DestroyDialog () {
		theDialog.SetActive(false);
		finish = true;
		enable = false;
	}

	public IEnumerator PlayLine() {
		while (playing && caracterIndex < theLineToWrite.Length){
			if (omit){
				theText.text = lastLine + theLineToWrite;
				omit = false;
				break;
			}
			yield return new WaitForSeconds(speed);
			theText.text += theLineToWrite[caracterIndex++];
		}
		caracterIndex = 0;
		playing = false;
		StartCoroutine("ContinueIndicator");
	}

	public IEnumerator ContinueIndicator () {
		while (!playing) {
			theContinueIndicator.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			theContinueIndicator.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.2f);
		}
		theContinueIndicator.gameObject.SetActive(false);
	}
}
