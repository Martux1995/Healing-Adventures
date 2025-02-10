using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminMain : MonoBehaviour {

    public Animator buttonBegin;
    public Animator buttonExit;
    public Animator buttonCredit;
    public Animator title;

    public void salir()
    {
        Application.Quit();
    }
	
	public void cambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }


    public void esconderBotones(bool esconder)
    {
        buttonBegin.SetBool("Esconder", esconder);
        buttonExit.SetBool("Esconder", esconder);
        buttonCredit.SetBool("Esconder", esconder);
        title.SetBool("Esconder", esconder);
    }
}
