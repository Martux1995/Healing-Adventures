using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AdminSelection : MonoBehaviour {

    public Animator titleSelection;
    public Animator buttonBoy;
    public Animator buttonGirl;

    public void cambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void esconderBoy(bool esconder)
    {
        buttonBoy.SetBool("Esconder", esconder);
    }

    public void esconderGirl(bool esconder)
    {
        buttonGirl.SetBool("Esconder", esconder);
    
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
}
