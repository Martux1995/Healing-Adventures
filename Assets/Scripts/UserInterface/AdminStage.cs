using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminStage : MonoBehaviour {
    public Button level02, level03, level04, level05, level06;
    int levelPassed;
    public AdminHighScore stageHighScore;
    private string levelSelected;
    
    private void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");       
        if (PlayerPrefs.GetString("Level1") != "pasado")
        {
            level02.interactable = false;
            level03.interactable = false;
            level04.interactable = false;
            level05.interactable = false;
            level06.interactable = false;
        }
        if(PlayerPrefs.GetString("Level2") != "pasado")
        {
            level03.interactable = false;
            level04.interactable = false;
            level05.interactable = false;
            level06.interactable = false;
        }
        if (PlayerPrefs.GetString("Level3") != "pasado")
        {
            
            level04.interactable = false;
            level05.interactable = false;
            level06.interactable = false;
        }
        if (PlayerPrefs.GetString("Level4") != "pasado")
        {
           
            level05.interactable = false;
            level06.interactable = false;
        }
        if (PlayerPrefs.GetString("Level5") != "pasado")
        {
           
            level06.interactable = false;
        }
        switch (levelPassed)
            {
                case 1:
                    level02.interactable = true;
                    break;
                case 2:
                    level02.interactable = true;
                    level03.interactable = true;
                    break;
                case 3:
                    level02.interactable = true;
                    level03.interactable = true;
                    level04.interactable = true;
                    break;
                case 4:
                    level02.interactable = true;
                    level03.interactable = true;
                    level04.interactable = true;
                    level05.interactable = true;
                    break;
                case 5:
                    level02.interactable = true;
                    level03.interactable = true;
                    level04.interactable = true;
                    level05.interactable = true;
                    level06.interactable = true;
                    break;
            }
        

    }

    void Update () {
        if (Input.GetKey(KeyCode.Escape)){
            if (stageHighScore.gameObject.activeSelf)
                stageHighScore.gameObject.SetActive(false);
            else
                Back();
        }
    }

    public void OpenHighScoreWindow (string level) {
        switch (level) {
            case "1": PlayerPrefs.SetString("LevelSelected", "red");     levelSelected = "Level 1";       break;
            case "2": PlayerPrefs.SetString("LevelSelected", "blue");    levelSelected = "Level 2";       break;
            case "3": PlayerPrefs.SetString("LevelSelected", "calypso"); levelSelected = "Level 3";       break;
            case "4": PlayerPrefs.SetString("LevelSelected", "orange");  levelSelected = "Level 4";       break;
            case "5": PlayerPrefs.SetString("LevelSelected", "purple");  levelSelected = "Level 5";       break;
            default:  PlayerPrefs.SetString("LevelSelected", "red");     levelSelected = "InfiniteLevel"; break;
        }
        stageHighScore.OpenHighScoreWindow(level);
    }

    public void OpenLevel () {
        SceneManager.LoadScene(levelSelected);
    }

    public void Back () {
        SceneManager.LoadScene("CharacterSelect");
    }
}
