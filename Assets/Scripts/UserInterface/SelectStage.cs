using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectStage : MonoBehaviour {

    public Animator buttonMenu;

    public void cambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void esconderBoton(bool esconder)
    {
        buttonMenu.SetBool("Esconder",esconder);
    }
}
