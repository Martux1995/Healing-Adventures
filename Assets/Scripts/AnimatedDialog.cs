using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatedDialog : MonoBehaviour {
    public Text textArea;
    public Image imagenFondo;
    public string[] strings;
    public float speed = 0.1f;
    int stringIndex = 0;
    int characterIndex = 0;
    byte color = 250;
   
    // Use this for initialization
    void Start () {
        imagenFondo.GetComponent<Image>().color = new Color32(250, 250, 250, 255);       
        StartCoroutine (DisplayTimer());
        
    }

    private IEnumerator DisplayTimer()
    {
        
        while (true)
        {
            imagenFondo.GetComponent<Image>().color = new Color32(color, color, color, 255);
            color--;
            yield return new WaitForSeconds(speed);
            if (characterIndex > strings[stringIndex].Length)
            {
                continue;
            }
            
            textArea.text = strings[stringIndex].Substring(0, characterIndex);
            characterIndex++;
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
    public void omitir( string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
