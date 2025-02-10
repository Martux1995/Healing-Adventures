using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;

	private GameManager theGameManager;

	void Start () {
		theGameManager = FindObjectOfType<GameManager>();
	}
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            PauseGame();
    }
    public void PauseGame() {
		pauseMenu.SetActive(true);
		//stageMusic.Pause();
		theGameManager.PauseMusic();
		Time.timeScale = 0f;
	}

	public void ResumeGame() {
		pauseMenu.SetActive(false);
		//stageMusic.Play();
		theGameManager.PlayMusic();
		Time.timeScale = 1f;
	}

	public void RestartGame() {
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		FindObjectOfType<GameManager>().ResetLevel();
	}

	public void QuitToStages () {
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		SceneManager.LoadScene("StageSelect");
	}
/*
	public void ExitGame() {
        Application.Quit();
	}
	*/
}
