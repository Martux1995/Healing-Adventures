using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdminInsertName : MonoBehaviour {

    public Animator panelInsertNameBoy;
    public Animator panelInsertNameGirl;
    public Text nombreGirl;
    public Text nombreBoy;
    string nombre;

    public void esconderPanelInsertNameBoy (bool esconder) {
        panelInsertNameBoy.enabled = true;
        panelInsertNameBoy.SetBool("Esconder", esconder);
        PlayerPrefs.SetString("CharacterSelected", "man");        
    }

    public void esconderPanelInsertNameGirl (bool esconder) {
        panelInsertNameGirl.enabled = true;
        panelInsertNameGirl.SetBool("Esconder", esconder);
        PlayerPrefs.SetString("CharacterSelected", "woman");
    }

    public void guardarNombre () {
        if (nombreGirl.text.Length != 0) {
            nombre = nombreGirl.text;
            PlayerPrefs.SetString("NameOfPj", nombre);
        } else {
            // HAY QUE MOSTRAR UN ERROR PARA QUE SE INSERTE UN NOMBRE
            nombre = nombreBoy.text;
            PlayerPrefs.SetString("NameOfPj", nombre);
        }

        
    }
   
}
