using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighScore : MonoBehaviour {
    public AudioSource Play;
    public AudioSource Back;
    public AudioClip clip;
    // Use this for initialization
    void Start () {
        Play.clip = clip;
        Back.clip = clip;
	}
	
	// Update is called once per frame
	
    public void reproducir()
    {
        Play.Play();
        Back.Play();

    }
}
