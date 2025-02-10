using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelection : MonoBehaviour {

    public AudioSource fuenteReturnGirl;
    public AudioSource fuenteReturnBoy;
    public AudioSource fuenteBeginBoy;
    public AudioSource fuenteBeginGirl;
    public AudioClip clip;

    void Start () {

        fuenteReturnGirl.clip = clip;
        fuenteReturnBoy.clip = clip;
        fuenteBeginGirl.clip = clip;
        fuenteBeginBoy.clip = clip;

    }
	
	public void reproducir () {

        fuenteBeginBoy.Play();
        fuenteBeginGirl.Play();
        fuenteReturnBoy.Play();
        fuenteReturnGirl.Play();

	}
}
