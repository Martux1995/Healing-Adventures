using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class AdminHighScore : MonoBehaviour {
    public Text nivel;
    public Text highScore;
    public GameObject stageHighScore;
    public Image level;

    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Sprite level4;
    public Sprite level5;
    public Sprite level6;

    private string selectedLevel;
   
    public void imprimir (string level) {
        string scoreImprimir = "";
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("ScoreLevel" + level + "-" + i) >0)
            {
                string name = PlayerPrefs.GetString("NameScoreLevel" + level + "-" + i);
                int puntos = PlayerPrefs.GetInt("ScoreLevel" + level + "-" + i);
                scoreImprimir = scoreImprimir + "\n\r" + name + ": " + puntos;
                
            }
        }
        highScore.text = scoreImprimir;
    }

    public void OpenHighScoreWindow (string stage) {
        nivel.text = "HighScore Nivel " + stage;
        imprimir(stage);
        switch (stage) {
            case "1":        level.sprite = level1; break;
            case "2":        level.sprite = level2; break;
            case "3":        level.sprite = level3; break;
            case "4":        level.sprite = level4; break;
            case "5":        level.sprite = level5; break;
            case "Infinito": level.sprite = level6; break;
        }
        stageHighScore.SetActive(true);
    }

    public void comeBack () {
        stageHighScore.SetActive(false);
    }

    void Update () {
        if (Input.GetKey(KeyCode.Escape))
            stageHighScore.SetActive(false);
    }

}
