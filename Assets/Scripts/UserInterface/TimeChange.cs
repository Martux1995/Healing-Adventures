using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TimeChange : MonoBehaviour {

    float currentTime = 0;
    float maxTime = 15;
	
    public string stageToChange;

	void Update () {

        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            SceneManager.LoadScene(stageToChange);
        }
	}
}
