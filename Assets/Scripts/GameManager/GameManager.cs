using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool isInfiniteLevel;

	private ObjectGenerator[] objectGenerators;
	private Vector3 platformStartPoint;
	private Vector3 slimeStartPoint;

	private Player thePlayer;
	private Vector3 playerStartPoint;

	private ObjectDestroyer[] platformList;

	private BossFightManager bossFightManager;
	private ScoreManager scoreManager;
	private CameraManager theCamera;

	public GameObject userInterface;
	public PauseMenu theEndGameScreen;
	public ContinueWindow theDefeatScreen;
	public VictoryWindow theVictoryWindow;
	public GameObject disableUI;

	private AudioSource stageMusic;
	private AudioSource bossMusic;
	private bool playingStageMusic;

	private Enemy[] theEnemies;
	
	// Use this for initialization
	void Start () {
		if (isInfiniteLevel){
			objectGenerators = FindObjectsOfType<ObjectGenerator>();
			foreach (ObjectGenerator og in objectGenerators){
				if (og.transform.name == "PlatformGenerator") {
					platformStartPoint = og.transform.position;
				}
				if (og.transform.name == "SlimeGenerator") {
					slimeStartPoint = og.transform.position;
				}
			}
		} else {
			bossFightManager = FindObjectOfType<BossFightManager>();
			theEnemies = FindObjectsOfType<Enemy>();
		}

		thePlayer = FindObjectOfType<Player>();
		playerStartPoint = thePlayer.transform.position;

		scoreManager = FindObjectOfType<ScoreManager>();
		ContinueScore();

		theCamera = FindObjectOfType<CameraManager>();
		theCamera.MoveCameraAuto(thePlayer.movementVelocity);

		AudioSource[] music = FindObjectsOfType<AudioSource>();
		foreach (AudioSource x in music) {
			if (x.gameObject.tag == "StageMusic") {
				stageMusic = x;
				continue;
			}
			if (x.gameObject.tag == "BossMusic") {
				bossMusic = x;
				continue;
			}
		}
		playingStageMusic = true;
	}


	public void StopGame (string cause){
		theCamera.StopCamera();
		StopScore();

		if (playingStageMusic)	stageMusic.Stop();

		if (isInfiniteLevel){
			if (cause == "Fall"){
				thePlayer.stop = true;
				thePlayer.gameObject.SetActive(false);
			}
			theEndGameScreen.gameObject.SetActive(true);
			scoreManager.SaveHighScore(scoreManager.scoreCount);
		} else {
			if (cause == "Fall"){
				thePlayer.gameObject.SetActive(false);
				// Revivir en un punto cercano
				theEndGameScreen.gameObject.SetActive(true);
				return;
			}
			if (bossFightManager.fighting){
				bossFightManager.StopBattle();
			}
			if (!theDefeatScreen.gameObject.activeSelf){
				theDefeatScreen.gameObject.SetActive(true);
				theDefeatScreen.Show();
			}
		}
	}

	public void ContinueGame () {
		theDefeatScreen.Reset();
		StartCoroutine("ResurrectPlayer");
	}

	public IEnumerator ResurrectPlayer () {
		thePlayer.ResetPlayer();
		thePlayer.ResurrectPlayer(1f);
		if (playingStageMusic){
			stageMusic.Stop();
			stageMusic.Play();
		}

		yield return new WaitForSeconds (1f);

		theDefeatScreen.gameObject.SetActive(false);
		if (!isInfiniteLevel){
			if (bossFightManager.fighting){
				bossFightManager.ContinueBattle();
			} else {
				ContinueScore();
				thePlayer.stop = false;
				theCamera.MoveCameraAuto(thePlayer.movementVelocity);
			}
		} else {
			ContinueScore();
			thePlayer.stop = false;
			theCamera.MoveCameraAuto(thePlayer.movementVelocity);
		}
	}

	public void ResetLevel () {
		if (isInfiniteLevel) {
			foreach	(ObjectGenerator og in objectGenerators){
				if (og.transform.name == "PlatformGenerator") {
					og.transform.position = platformStartPoint;
				}
				if (og.transform.name == "SlimeGenerator") {
					og.transform.position = slimeStartPoint;
				}
			}
			foreach (ObjectDestroyer x in FindObjectsOfType<ObjectDestroyer>()){
				x.gameObject.SetActive(false);
			}


		} else {
			if (bossFightManager.fighting){
				bossFightManager.ResetBattle();
			}
			Projectile[] p = FindObjectsOfType<Projectile>();
			foreach (Projectile x in p) {
				x.gameObject.SetActive(false);
			}
			foreach (Enemy x in theEnemies){
				x.reset = true;
				x.gameObject.SetActive(true);
			}
		}

		thePlayer.transform.position = playerStartPoint;
		thePlayer.gameObject.SetActive(true);
		thePlayer.ResetPlayer();

		if (!playingStageMusic){
			bossMusic.Stop();
			playingStageMusic = true;
		}
		stageMusic.Stop();
		stageMusic.Play();

		theCamera.RestartCamera();
		theCamera.MoveCameraAuto(thePlayer.movementVelocity);

		scoreManager.scoreCount = 0;
		ContinueScore();
		thePlayer.stop = false;
	}

	public void FinishLevel () {
		string level = PlayerPrefs.GetString("LevelSelected");
		switch (level) {
            case "red":
                PlayerPrefs.SetString("Level1", "pasado");
                PlayerPrefs.SetInt("LevelPassed", 1);
				theVictoryWindow.SetScore(scoreManager.scoreCount,1);
                break;
            case "blue":
                PlayerPrefs.SetString("Level2", "pasado");
                PlayerPrefs.SetInt("LevelPassed", 2);
				theVictoryWindow.SetScore(scoreManager.scoreCount,2);
                break;
            case "calypso":
                PlayerPrefs.SetString("Level3", "pasado");
                PlayerPrefs.SetInt("LevelPassed", 3);
				theVictoryWindow.SetScore(scoreManager.scoreCount,3);
                break;
            case "orange":
                PlayerPrefs.SetInt("LevelPassed", 4);
                PlayerPrefs.SetString("Level4","pasado");
				theVictoryWindow.SetScore(scoreManager.scoreCount,4);
                break;
            case "purple":
                PlayerPrefs.SetString("Level5", "pasado");
                PlayerPrefs.SetInt("LevelPassed", 5);
				theVictoryWindow.SetScore(scoreManager.scoreCount,5);
                break;
        }
		theVictoryWindow.OpenWindow();
	}

	public void StopScore ()				{ scoreManager.scoreIncreasing = false; }
	public void ContinueScore () 			{ scoreManager.scoreIncreasing = true;  }
	public void IncreaseScore (float score) { scoreManager.scoreCount += score; }

	public void HideUserInterface () { userInterface.SetActive(false); }
	public void ShowUserInterface () { userInterface.SetActive(true);  }

	public void DisableUI () { disableUI.SetActive(true);  }
	public void EnableUI ()  { disableUI.SetActive(false); }

	public void StopMusic(float delay = 0) {
		if (playingStageMusic) {
			if (delay <= 0) {
				stageMusic.Stop();
			} else {
				StartCoroutine("ReduceVolumeStageMusic",delay);
			} 
		} else {	
			bossMusic.Stop();
		}
	}

	public void PauseMusic () {
		if (playingStageMusic)	stageMusic.Pause();
		else					bossMusic.Pause();
	}

	public void PlayMusic () {
		if (playingStageMusic)	stageMusic.Play();
		else					bossMusic.Play();
	}

	public void SwitchToBossMusic () {
		playingStageMusic = false;
		stageMusic.Stop();
		bossMusic.Play();
	}

	public IEnumerator ReduceVolumeStageMusic (float delay){
		float counter = 0.1f / delay;
		float actualVolume = stageMusic.volume;
		while (stageMusic.volume > 0){
			stageMusic.volume -= counter;
			yield return new WaitForSeconds(0.1f);
		}
		stageMusic.Stop();
		stageMusic.volume = actualVolume;
	}



}
