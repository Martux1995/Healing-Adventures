using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryWindow : MonoBehaviour {

	public AudioSource winnerSound;

	private int scoreObtained;

    private int auxScore;
    private int auxScore2;
    private string auxName;
    private string auxName2;

    public void OpenWindow () {
        gameObject.SetActive(true);
		winnerSound.Play();
	}

	public void SetScore (float score,int scene) {
		scoreObtained = (int) (score);
        comparar (scene);
	}
	public void ContinueButtonAction () {
		SceneManager.LoadScene("StageSelect");
	}

    public void comparar(int level)
    {
        string name = PlayerPrefs.GetString("NameOfPj");
        if (PlayerPrefs.GetInt("ScoreLevel" + level + "-1") == 0)
        {
            PlayerPrefs.SetInt("ScoreLevel" + level + "-1", scoreObtained);
            PlayerPrefs.SetString("NameScoreLevel" + level + "-1", name);
        }
        else
        {
            if (PlayerPrefs.GetInt("ScoreLevel" + level + "-1") < scoreObtained)
            {
                auxScore = PlayerPrefs.GetInt("ScoreLevel" + level + "-1");
                auxName = PlayerPrefs.GetString("NameScoreLevel" + level + "-1");
                auxScore2 = PlayerPrefs.GetInt("ScoreLevel" + level + "-2");
                auxName2 = PlayerPrefs.GetString("NameScoreLevel" + level + "-2");
                PlayerPrefs.SetInt("ScoreLevel" + level + "-1", scoreObtained);
                PlayerPrefs.SetString("NameScoreLevel" + level + "-1", name);
                PlayerPrefs.SetInt("ScoreLevel" + level + "-2", auxScore);
                PlayerPrefs.SetString("NameScoreLevel" + level + "-2", auxName);
                PlayerPrefs.SetInt("ScoreLevel" + level + "-3", auxScore2);
                PlayerPrefs.SetString("NameScoreLevel" + level + "-3", auxName2);
            }
            else
            {
                if (PlayerPrefs.GetInt("ScoreLevel" + level + "-2") < scoreObtained)
                {
                    auxScore = PlayerPrefs.GetInt("ScoreLevel" + level + "-2");
                    auxName = PlayerPrefs.GetString("NameScoreLevel" + level + "-2");
                    PlayerPrefs.SetInt("ScoreLevel" + level + "-2", scoreObtained);
                    PlayerPrefs.SetString("NameScoreLevel" + level + "-2", name);
                    PlayerPrefs.SetInt("ScoreLevel" + level + "-3", auxScore);
                    PlayerPrefs.SetString("NameScoreLevel" + level + "-3", auxName);
                }
                else
                {
                    if (PlayerPrefs.GetInt("ScoreLevel" + level + "-3") < scoreObtained)
                    {
                        PlayerPrefs.SetInt("ScoreLevel" + level + "-3", scoreObtained);
                        PlayerPrefs.SetString("NameScoreLevel" + level + "-3", name);
                    }
                }
            }
        }
    }
     
}
