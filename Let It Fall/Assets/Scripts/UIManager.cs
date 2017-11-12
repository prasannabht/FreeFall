using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Animator StartScreenAnimator;
	public Animator QuitScreenAnimator;
	public Animator BackgroundAnimator;
	public Animator CheckpointAnimator;
//	public Animator BackgroundHills;
//	public Animator BackgroundColor;
//	public Animator BallAnimator;
	public GameObject StartScreenButtons;
	public GameObject QuitScreen;
	public GameObject Title;
	public GameObject PauseButton;
	public GameObject ScoreText;
	public GameObject CelebrateHighScoreObj;
	public GameObject ScoreLabel;
	public GameObject HighScoreLabel;
	public GameObject HighScoreStar;
	public GameObject Sound;
	public GameObject CheckpointButton;

	public Sprite SoundOn;
	public Sprite SoundOff;
	public Sprite checkpointOn;
	public Sprite checkpointOff;

	bool gameOverFlag = false;
	bool quitDisplayed = false;
	static bool hasGameStarted = false;
	public static bool celebrateHighScore = false;
	public static bool enableCheckpoint = false;
	public static float currentCheckpointScore;
	public static bool checkpoint = false;
	bool IsCheckpointShown = false;
	bool IsCheckpointActive = false;
	GameManager myGameManager;

	void Awake(){
		//PlayerPrefs.DeleteAll ();
		if (!PlayerPrefs.HasKey ("sound")) {
			PlayerPrefs.SetInt ("sound", 1);
		}
	}

	void Start(){
		//SlideInStartScreen ();
		DisablePauseAndScore ();
		FadeInBackground ();
		DisableScoreLabels ();
		DisableCelebrateHighScore ();
		DisableHighScoreStar ();
		//MoveBallToMiddle ();

		if (PlayerPrefs.GetInt("sound")==1) {
			Sound.GetComponent<Image> ().sprite = SoundOn;
		} else if(PlayerPrefs.GetInt("sound")==0){
			Sound.GetComponent<Image> ().sprite = SoundOff;
		}

		hasGameStarted = false;

//		//test
//		hasGameStarted = true;
//		GameManager.SetBallFallingFlag (true);

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameManager.IsBallFalling ()) {
				Title.SetActive (false);
				GameManager.SetBallFallingFlag (false);
				FindObjectOfType<GameManager>().DisplayScore ();
				SlideInStartScreen ();
				DisablePauseAndScore ();
				DisableHighScoreStar ();
				EnableScoreLabels ();
				FadeInBackground ();
				HideCheckpointButton ();

			} else {
				if (!quitDisplayed) {
					QuitGameScreen ();
				} else {
					print ("Exit game");
					Application.Quit ();
				}
			}
		}

		if (celebrateHighScore) {
			EnableHighScoreStar ();
		}

		if (enableCheckpoint){
			print ("Enable checkpoint");
			EnableCheckpointButton ();
			enableCheckpoint = false;
		}
	}

	public void StartGame(){
		hasGameStarted = true;
		celebrateHighScore = false;
		SlideOutStartScreen ();
		//MoveBallToTop ();
		GameManager.SetBallFallingFlag (true);
		if (gameOverFlag && !checkpoint) {
			FindObjectOfType<ObstacleManager> ().ResetObstacles();
			gameOverFlag = false;
		}

		if (gameOverFlag && checkpoint) {
			FindObjectOfType<ObstacleManager> ().ResetToCheckpoint ();
			gameOverFlag = false;
			ResetCheckpointValues ();
		} else {
			GameManager.nextCheckpoint = GameManager.checkpointInitScore;
		}

		EnablePauseAndScore ();
		DisableHighScoreStar ();
		FadeOutBackground ();
		DisableScoreLabels ();
		DisableCelebrateHighScore ();
		ShowCheckpointButton ();
			
		//HideCheckpointButton ();
		//MoveInBackgroundHills ();
		//BackgroundDarkToLight ();
	}

	public void GameOver(){
		gameOverFlag = true;
		Title.SetActive (false);
		SlideInStartScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		FadeInBackground ();
		EnableScoreLabels ();
		DisableCheckpointButton ();
		if (celebrateHighScore) {
			EnableCelebrateHighScore ();
			celebrateHighScore = false;
		}
		//MoveOutBackgroundHills ();
	}

	public void PauseGame(){
		Title.SetActive (false);
		GameManager.SetBallFallingFlag (false);
		FindObjectOfType<GameManager>().DisplayScore ();
		SlideInStartScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		FadeInBackground ();
		EnableScoreLabels ();
		HideCheckpointButton ();
	}

	public void QuitGameScreen(){
		SlideOutStartScreen ();
		GameManager.SetBallFallingFlag (false);
		SlideInQuitScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		quitDisplayed = true;
		IsCheckpointShown = false;

		FadeInBackground ();
		//HideCheckpointButton ();
	}

	public void QuitGameYes(){
		print ("Exit game");
		Application.Quit ();
	}

	public void QuitGameNo(){
		quitDisplayed = false;
		SlideInStartScreen ();
		SlideOutQuitScreen ();
	}

	public void SetSound(){
		if (PlayerPrefs.GetInt("sound")==1) {
			Sound.GetComponent<Image> ().sprite = SoundOff;
			PlayerPrefs.SetInt ("sound", 0);
			AudioListener.volume = 0;
		} else if(PlayerPrefs.GetInt("sound")==0){
			Sound.GetComponent<Image> ().sprite = SoundOn;
			PlayerPrefs.SetInt ("sound", 1);
			AudioListener.volume = 1;
		}
	}

	public void DisablePauseAndScore(){
		PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 0);
	}


	public void EnablePauseAndScore(){
		PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 1);
	}

	public void DisableCelebrateHighScore(){
		CelebrateHighScoreObj.GetComponent<Text>().color = new Color (1, 1, 1, 0);
	}

	public void EnableCelebrateHighScore(){
		CelebrateHighScoreObj.GetComponent<Text>().color = new Color (1, 1, 1, 1);
	}


	public void DisableScoreLabels(){
//		ScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 0);
//		HighScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 0);
		ScoreLabel.SetActive(false);
		HighScoreLabel.SetActive (false);
	}

	public void EnableScoreLabels(){
//		ScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 1);
//		HighScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 1);
		ScoreLabel.SetActive(true);
		HighScoreLabel.SetActive (true);
	}

	public void DisableHighScoreStar(){
		HighScoreStar.SetActive (false);
	}

	public void EnableHighScoreStar(){
		HighScoreStar.SetActive (true);
	}


	public void SlideInQuitScreen(){

		QuitScreenAnimator.SetBool ("SlideInQuitScreen", true);
	}

	public void SlideOutQuitScreen(){

		QuitScreenAnimator.SetBool ("SlideInQuitScreen", false);
	}

	public void SlideInStartScreen(){

		StartScreenAnimator.SetBool ("SlideInStartScreen", true);
	}

	public void SlideOutStartScreen(){

		StartScreenAnimator.SetBool ("SlideInStartScreen", false);
	}

	public void FadeInBackground(){
		BackgroundAnimator.SetBool ("FadeInBackground", true);
	}

	public void FadeOutBackground(){
		BackgroundAnimator.SetBool ("FadeInBackground", false);
	}

	public static bool GameStarted(){
		return hasGameStarted;
	}
//
//	public void MoveBallToTop(){
//
//		BallAnimator.SetBool ("BallAtTop", true);
//	}
//
//	public void MoveBallToMiddle(){
//
//		BallAnimator.SetBool ("BallAtTop", false);
//	}
//
//	public void MoveInBackgroundHills(){
//
//		BackgroundHills.SetBool ("GameStarted", true);
//	}
//
//	public void MoveOutBackgroundHills(){
//
//		BackgroundHills.SetBool ("GameStarted", false);
//	}
//	

	public void EnableCheckpointButton(){
		IsCheckpointActive = true;
		CheckpointAnimator.SetBool ("EnableCheckpointButton", true);
		CheckpointButton.GetComponentInChildren<Text> ().text = "+";
		CheckpointButton.GetComponentInChildren<Text> ().fontSize = 80;
		CheckpointButton.GetComponent<Image> ().sprite = checkpointOn;

		IsCheckpointShown = true;
	}

	public void DisableCheckpointButton(){
		CheckpointAnimator.SetBool ("EnableCheckpointButton", false);
	}

	public void ShowCheckpointButton(){
		CheckpointButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		CheckpointButton.GetComponentInChildren<Text>().color = new Color (1, 1, 1, 1);
	}

	public void HideCheckpointButton(){
		CheckpointButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		CheckpointButton.GetComponentInChildren<Text>().color = new Color (1, 1, 1, 0);
	}

	public void AddCheckpoint(){
		if (IsCheckpointActive) {
			float currScore = FindObjectOfType<GameManager> ().score;
			float interval = GameManager.checkpointRepeat;
			currentCheckpointScore = currScore;
			GameManager.nextCheckpoint = currScore + interval;
			checkpoint = true;
			print ("Checkpoint at: " + currentCheckpointScore);
			FindObjectOfType<ObstacleManager> ().SaveCheckpointDetails ();
			//HideCheckpointButton ();
			CheckpointButton.GetComponentInChildren<Text> ().text = currentCheckpointScore.ToString ();
			CheckpointButton.GetComponentInChildren<Text> ().fontSize = 40;
			CheckpointButton.GetComponent<Image> ().sprite = checkpointOff;

			IsCheckpointActive = false;
		}
	}


	void ResetCheckpointValues(){
		checkpoint = false;
		GameManager.nextCheckpoint = currentCheckpointScore + GameManager.checkpointRepeat;
		print ("next checkpoint: " + GameManager.nextCheckpoint);
	}
}
