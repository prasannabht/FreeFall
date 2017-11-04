using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Animator StartScreenAnimator;
	public Animator QuitScreenAnimator;
	public Animator BackgroundAnimator;
	public Animator BackgroundHills;
	public Animator BallAnimator;
	public GameObject StartScreenButtons;
	public GameObject QuitScreen;
	public GameObject Title;
	public GameObject PauseButton;
	public GameObject ScoreText;
	public GameObject Sound;
	public Sprite SoundOn;
	public Sprite SoundOff;

	bool gameOverFlag = false;
	bool quitDisplayed = false;
	static bool hasGameStarted = false;

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
		MoveBallToMiddle ();

		if (PlayerPrefs.GetInt("sound")==1) {
			Sound.GetComponent<Image> ().sprite = SoundOn;
		} else if(PlayerPrefs.GetInt("sound")==0){
			Sound.GetComponent<Image> ().sprite = SoundOff;
		}

		hasGameStarted = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameManager.IsBallFalling ()) {
				Title.SetActive (false);
				GameManager.SetBallFallingFlag (false);
				FindObjectOfType<GameManager>().DisplayScore ();
				SlideInStartScreen ();
				DisablePauseAndScore ();
				FadeInBackground ();

			} else {
				if (!quitDisplayed) {
					QuitGameScreen ();
				} else {
					print ("Exit game");
					Application.Quit ();
				}
			}
		}
	}

	public void StartGame(){
		hasGameStarted = true;
		SlideOutStartScreen ();
		MoveBallToTop ();
		GameManager.SetBallFallingFlag (true);
		if (gameOverFlag) {
			FindObjectOfType<ObstacleManager> ().ResetObstacles();
			gameOverFlag = false;
		}
		EnablePauseAndScore ();
		FadeOutBackground ();
		MoveInBackgroundHills ();
	}

	public void GameOver(){
		gameOverFlag = true;
		Title.SetActive (false);
		//FindObjectOfType<ObstacleManager> ().ResetObstacles();
		//FindObjectOfType<GameManager>().DisplayScore ();
		SlideInStartScreen ();
		DisablePauseAndScore ();
		FadeInBackground ();
		MoveOutBackgroundHills ();
	}

	public void PauseGame(){
		Title.SetActive (false);
		GameManager.SetBallFallingFlag (false);
		FindObjectOfType<GameManager>().DisplayScore ();
		SlideInStartScreen ();
		DisablePauseAndScore ();
		FadeInBackground ();
	}

	public void QuitGameScreen(){
		SlideOutStartScreen ();
		GameManager.SetBallFallingFlag (false);
		SlideInQuitScreen ();
		DisablePauseAndScore ();
		quitDisplayed = true;
		FadeInBackground ();
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
			//audio.mute = true;
		} else if(PlayerPrefs.GetInt("sound")==0){
			Sound.GetComponent<Image> ().sprite = SoundOn;
			PlayerPrefs.SetInt ("sound", 1);
			AudioListener.volume = 1;
			//audio.mute = false;
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

	public void MoveBallToTop(){

		BallAnimator.SetBool ("BallAtTop", true);
	}

	public void MoveBallToMiddle(){

		BallAnimator.SetBool ("BallAtTop", false);
	}

	public void MoveInBackgroundHills(){

		BackgroundHills.SetBool ("GameStarted", true);
	}

	public void MoveOutBackgroundHills(){

		BackgroundHills.SetBool ("GameStarted", false);
	}
}
