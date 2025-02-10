using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStage : MonoBehaviour {

    public AudioSource stageSoundButton;

    public void reproducir () {
        stageSoundButton.Play();
    }
}
