using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public float scoreCount;

	// public Text highScoreText
	// public float highScoreCount;

	public float pointsPerSecond;	// Puntuacion para ganar puntos al momento de moverse

	public bool scoreIncreasing;
    public int scoreObtained;
    
    public int auxScore;
    public int auxScore2;
    public string auxName;
    public string auxName2;

	// Use this for initialization
	void Start () {
		/*if ( PlayerPrefs.getFloat("HighScore") != null ){
			highScoreCount = PlayerPrefs.GetFloat("HishScore");
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreIncreasing){
			scoreCount += pointsPerSecond * Time.deltaTime;
		}
/*
		if (scoreCount > highScoreCount){
			highScoreCount = scoreCount;
			PlayerPrefs.SetFloat("HighScore",highScoreCount);
		}
*/
		scoreText.text = "Puntuación: " + Mathf.Round(scoreCount);
		//highScoreText.text = "Mejor:\n" + highScoreCount;
	}

	public void SaveHighScore (float newHighScore) {
        scoreObtained = (int)newHighScore;
        string name = PlayerPrefs.GetString("NameOfPj");
        if (PlayerPrefs.GetInt("ScoreLevelInfinito-1") == 0)
        {
            PlayerPrefs.SetInt("ScoreLevelInfinito-1", scoreObtained);
            PlayerPrefs.SetString("NameScoreLevelInfinito-1", name);
        }
        else
        {
            if (PlayerPrefs.GetInt("ScoreLevelInfinito-1") < scoreObtained)
            {
                auxScore = PlayerPrefs.GetInt("ScoreLevelInfinito-1");
                auxName = PlayerPrefs.GetString("NameScoreLevelInfinito-1");
                auxScore2 = PlayerPrefs.GetInt("ScoreLevelInfinito-2");
                auxName2 = PlayerPrefs.GetString("NameScoreLevelInfinito-2");
                PlayerPrefs.SetInt("ScoreLevelInfinito-1", scoreObtained);
                PlayerPrefs.SetString("NameScoreLevelInfinito-1", name);
                PlayerPrefs.SetInt("ScoreLevelInfinito-2", auxScore);
                PlayerPrefs.SetString("NameScoreLevelInfinito-2", auxName);
                PlayerPrefs.SetInt("ScoreLevelInfinito-3", auxScore2);
                PlayerPrefs.SetString("NameScoreLevelInfinito-3", auxName2);
            }
            else
            {
                if (PlayerPrefs.GetInt("ScoreLevelInfinito-2") < scoreObtained)
                {
                    auxScore = PlayerPrefs.GetInt("ScoreLevelInfinito-2");
                    auxName = PlayerPrefs.GetString("NameScoreLevelInfinito-2");
                    PlayerPrefs.SetInt("ScoreLevelInfinito-2", scoreObtained);
                    PlayerPrefs.SetString("NameScoreLevelInfinito-2", name);
                    PlayerPrefs.SetInt("ScoreLevelInfinito-3", auxScore);
                    PlayerPrefs.SetString("NameScoreLevelInfinito-3", auxName);
                }
                else
                {
                    if (PlayerPrefs.GetInt("ScoreLevelInfinito-3") < scoreObtained)
                    {
                        PlayerPrefs.SetInt("ScoreLevelInfinito-3", scoreObtained);
                        PlayerPrefs.SetString("NameScoreLevelInfinito-3", name);
                    }
                }
            }
       
        }
	}
}
