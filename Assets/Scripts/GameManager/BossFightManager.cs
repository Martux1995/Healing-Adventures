using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossFightManager : MonoBehaviour {

	public AudioSource bossMusic;

	public Transform doorStartPosition;
	public Transform doorEndPosition;
	private Vector3 bossRoomOriginalPosition;

	private bool omitIntro;
	public bool fighting;
	private bool inDoor;

	private Player thePlayer;
	private Boss theBoss;
	private CameraManager theCamera;
	private GameManager theGameManager;

	private GameBoxDialog bossDialog;

	public Transform playerAttackPosition;
	private Transform playerIdlePosition;
	public float playerAttackTime;
	private float playerAttackTimeCounter;

	public GameObject theFightIntro;
	private Animator theFightIntroAnimator;

	private float playerCameraOffset;
	private bool enter = false;
	private bool finishEvent;

	private float resurrectTime = 1f;
	private float resurrectTimeCounter = 1.1f;

	private Rigidbody2D myRigidbody;

	public AudioSource versusSound;
	public string bossScientificNace;
	public string bossName;
	private bool enterCoroutine;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<Player>();
		theBoss = FindObjectOfType<Boss>();
		theCamera = FindObjectOfType<CameraManager>();
		theGameManager = FindObjectOfType<GameManager>();
		bossDialog = theBoss.GetComponent<GameBoxDialog>();
		myRigidbody = GetComponent<Rigidbody2D>();
		theFightIntroAnimator = theFightIntro.GetComponent<Animator>();
		enterCoroutine = false;
		foreach (Transform x in theFightIntro.transform){
			switch (x.gameObject.name) {
				case "Player":
				    string playerGender = PlayerPrefs.GetString("CharacterSelected");
        			string playerColor = PlayerPrefs.GetString("LevelSelected");
        
        			string path = "Sprites/" + playerGender + "_" + playerColor;
					x.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
					break;
				case "Enemy":
					x.GetComponent<Image>().sprite = theBoss.GetComponent<SpriteRenderer>().sprite;
					break;
				case "PlayerName":
					x.GetComponent<Text>().text = PlayerPrefs.GetString("NameOfPj");
					break;
				case "EnemyName":
					x.GetComponent<Text>().text = bossScientificNace + "\n" + bossName;
					break;
			}
		}

		bossRoomOriginalPosition = gameObject.transform.position;
		ResetBattle();
	}
	
	// Update is called once per frame
	void Update () {
		if (enter){
			if (!theBoss.isDefeated() && !thePlayer.isDefeated() && resurrectTimeCounter > resurrectTime) {
				if (fighting){
					switch (theBoss.bossName){
						case TypeBoss.Cell:		CellFight();		break;
						case TypeBoss.Fists:	FistsFight();		break;
						case TypeBoss.Dragon:	DragonFight();		break;
						case TypeBoss.Twezzers:	TwezzersFight();	break;
						case TypeBoss.XForce:	XForceFight();		break;
					}
				} else if (bossDialog.finish && !omitIntro && !enterCoroutine) {
					StartCoroutine("ShowIntro"); 
					enterCoroutine = true;
				} 

			} else if (resurrectTimeCounter <= resurrectTime && !thePlayer.isDefeated()) {
				resurrectTimeCounter += Time.deltaTime;
			} else if (theBoss.isDefeated() && !finishEvent) {
				finishEvent = true;
				StartCoroutine("EndBattle",3f);
			}
		} else {
			EntryEffect();
		}
	}

	void EntryEffect () {
		if (thePlayer.transform.position.x >= doorEndPosition.position.x){
			thePlayer.stop = true;
			Vector3 position = new Vector3 (thePlayer.transform.position.x + playerCameraOffset,
											thePlayer.transform.position.y,
											thePlayer.transform.position.z);
			theCamera.MoveCameraPositionSmooth(position,6);
			theGameManager.StopScore();
			enter = true;
			theGameManager.StopMusic(1.6f);
			StartCoroutine("ShowDialog",2f);
		} else if (thePlayer.transform.position.x >= doorStartPosition.position.x && !inDoor && !thePlayer.isDefeated()){
			theCamera.StopCamera();
			theGameManager.DisableUI();
			playerCameraOffset = theCamera.transform.position.x - thePlayer.transform.position.x;
			playerIdlePosition = doorEndPosition;
			inDoor = true;
		}
	}
	
	public IEnumerator ShowDialog (float delay) {
		yield return new WaitForSeconds (delay);
		theGameManager.EnableUI();
		if (!omitIntro) {
			theGameManager.HideUserInterface();
			bossDialog.ShowDialog();
		} else {
			StartCoroutine("ShowIntro");
		}
	}

	public IEnumerator ShowIntro () {
		if (!omitIntro){
			theFightIntro.SetActive(true);
			theFightIntroAnimator.SetTrigger("Start");
			yield return new WaitForSeconds(3.5f);
			theFightIntro.SetActive(false);
		}

		theGameManager.ShowUserInterface();
		theBoss.ShowHealthBar();
		fighting = omitIntro = true;
		theGameManager.SwitchToBossMusic();
	}

	// ------ FUNCIONES QUE CONTROLAN LOS PATRONES DE ATAQUE DE CADA JEFE ------ //

	public void CellFight () {
		if (!theBoss.enable){
			
			if (playerAttackTime >= playerAttackTimeCounter && thePlayer.transform.position.x >= playerAttackPosition.position.x) {
				// Esta en la posicion de ataque dentro del tiempo de ataque
				thePlayer.stop = true;
				playerAttackTimeCounter += Time.deltaTime;
			} else {
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x >= playerIdlePosition.position.x) {
					// El player retrocede a la posicion de defensa
					thePlayer.MovePlayer(playerIdlePosition.position.x);
					thePlayer.stop = false;

				} else if (playerAttackTimeCounter == 0 && thePlayer.transform.position.x <= playerAttackPosition.position.x){
					// El player avanza a la posicion de ataque
					thePlayer.MovePlayer(playerAttackPosition.position.x);
					thePlayer.stop = false;

				}
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x <= playerIdlePosition.position.x){
					// Volvió a la posición de defensa, el boss se activa y el player se detiene
					playerAttackTimeCounter = 0;
					thePlayer.stop = true;
					theBoss.enable = true;

				}
			}
		}
	}

	public void FistsFight () {
		if (thePlayer.transform.position.x <= playerAttackPosition.position.x) {
			thePlayer.MovePlayer(playerAttackPosition.position.x);
			thePlayer.stop = false;
		} else {
			thePlayer.stop = true;
		}
		if (!theBoss.enable){
			if (playerAttackTime >= playerAttackTimeCounter) {
				playerAttackTimeCounter += Time.deltaTime;
			} else {
				playerAttackTimeCounter = 0;
				theBoss.enable = true;
			}
		}
	}	

	public void DragonFight () {
		if (!theBoss.enable){
			if (playerAttackTime >= playerAttackTimeCounter && thePlayer.transform.position.x >= playerAttackPosition.position.x) {
				// Atacando
				thePlayer.stop = true;
				playerAttackTimeCounter += Time.deltaTime;
			} else {
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x >= doorEndPosition.position.x) {
					// Retrocediendo
					myRigidbody.velocity = new Vector2(10f,0);
					theCamera.MoveCameraAuto(10f);
					theBoss.MoveBoss(10f);
					thePlayer.stop = true;

				} else if (playerAttackTimeCounter == 0 && thePlayer.transform.position.x <= playerAttackPosition.position.x){
					// Avanzando
					myRigidbody.velocity = new Vector2(0,0);
					theCamera.MoveCameraAuto(0);
					theBoss.MoveBoss(0);
					thePlayer.MovePlayer(playerAttackPosition.position.x);
					thePlayer.stop = false;

				}
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x <= doorEndPosition.position.x){
					//Posicion de defensa
					playerAttackTimeCounter = 0;
					thePlayer.stop = false;
					theBoss.enable = true;
				}
			}
		} else {
			myRigidbody.velocity = new Vector2(10f,0);
			thePlayer.stop = false;
			theCamera.MoveCameraAuto(10f);
			theBoss.MoveBoss(10f);
		}
	}

	public void TwezzersFight () {
		if (!theBoss.enable){
			if (playerAttackTime >= playerAttackTimeCounter && thePlayer.transform.position.x >= playerAttackPosition.position.x) {
				// Atacando
				thePlayer.stop = true;
				playerAttackTimeCounter += Time.deltaTime;
			} else {
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x >= doorEndPosition.position.x) {
					// Retrocediendo
					//thePlayer.MovePlayer(playerIdlePosition.position.x);
					//thePlayer.stop = false;
					myRigidbody.velocity = new Vector2(10f,0);
					theCamera.MoveCameraAuto(10f);
					theBoss.MoveBoss(10f);
					thePlayer.stop = true;

				} else if (playerAttackTimeCounter == 0 && thePlayer.transform.position.x <= playerAttackPosition.position.x){
					// Avanzando
					myRigidbody.velocity = new Vector2(0,0);
					theCamera.MoveCameraAuto(0);
					theBoss.MoveBoss(0);
					thePlayer.MovePlayer(playerAttackPosition.position.x);
					thePlayer.stop = false;

				}
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x <= doorEndPosition.position.x){
					//Posicion de defensa
					
					playerAttackTimeCounter = 0;
					thePlayer.stop = false;

					theBoss.enable = true;
					//myRigidbody.velocity = new Vector2(10f,0);
					//theCamera.MoveCameraAuto(10f);
					//theBoss.MoveBoss(10f);
				}
			}
		} else {
			myRigidbody.velocity = new Vector2(10f,0);
			thePlayer.stop = false;
			theCamera.MoveCameraAuto(10f);
			theBoss.MoveBoss(10f);
		}
	}

	public void XForceFight () {

		if (!theBoss.enable){
			if (playerAttackTime >= playerAttackTimeCounter && thePlayer.transform.position.x >= playerAttackPosition.position.x) {
				// Atacando
				thePlayer.stop = true;
				playerAttackTimeCounter += Time.deltaTime;
			} else {
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x >= doorEndPosition.position.x) {
					// Retrocediendo
					//thePlayer.MovePlayer(playerIdlePosition.position.x);
					//thePlayer.stop = false;
					myRigidbody.velocity = new Vector2(10f,0);
					theCamera.MoveCameraAuto(10f);
					theBoss.MoveBoss(10f);
					thePlayer.stop = true;

				} else if (playerAttackTimeCounter == 0 && thePlayer.transform.position.x <= playerAttackPosition.position.x){
					// Avanzando
					myRigidbody.velocity = new Vector2(0,0);
					theCamera.MoveCameraAuto(0);
					theBoss.MoveBoss(0);
					thePlayer.MovePlayer(playerAttackPosition.position.x);
					thePlayer.stop = false;

				}
				if (playerAttackTimeCounter >= playerAttackTime && thePlayer.transform.position.x <= doorEndPosition.position.x){
					//Posicion de defensa
					
					playerAttackTimeCounter = 0;
					thePlayer.stop = false;

					theBoss.enable = true;
					//myRigidbody.velocity = new Vector2(10f,0);
					//theCamera.MoveCameraAuto(10f);
					//theBoss.MoveBoss(10f);
				}
			}
		} else {
			myRigidbody.velocity = new Vector2(10f,0);
			thePlayer.stop = false;
			theCamera.MoveCameraAuto(10f);
			theBoss.MoveBoss(10f);
		}
	}

	// ------ CONTROLADORES DE LA BATALLA ------ //

	public IEnumerator EndBattle (float timeToEnd) {
		theGameManager.DisableUI();
		theGameManager.StopMusic();
		thePlayer.stop = true;
		theBoss.MoveBoss(0);
		myRigidbody.velocity = new Vector2(0,0);
		theCamera.StopCamera();
		yield return new WaitForSeconds(timeToEnd);
		theGameManager.EnableUI();
		theGameManager.FinishLevel();
	}

	public void StopBattle () {
		playerAttackTimeCounter = 0;
		resurrectTimeCounter = 0;
		//theBoss.enable = false;
		theBoss.ResetAttack();

		theCamera.StopCamera();
		myRigidbody.velocity = new Vector2(0,0);
		theBoss.MoveBoss(0);

		//transform.position = bossRoomOriginalPosition;
	}

	public void ContinueBattle () {
		playerAttackTimeCounter = playerAttackTime + 1f;
		//theBoss.enable = true;
	}

	public void ResetBattle () {
		fighting = false;
		inDoor = false;
		enter = false;
		playerAttackTimeCounter = playerAttackTime + 1f;
		theBoss.ResetAttack();
		theBoss.ResetBoss();
		finishEvent = false;

		theBoss.MoveBoss(0);
		transform.position = bossRoomOriginalPosition;
		myRigidbody.velocity = new Vector2(0,0);
	}

}
