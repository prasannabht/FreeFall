using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour {
	//Object references for buttons
	public GameObject StartScreenButtons;

	public GameObject Title;
	public GameObject PauseButton;
	public GameObject ScoreText;
	public GameObject StartBackground;

	public GameObject CurrentScore;
	public GameObject HighestScore;

	public GameObject QuitScreen;

	GameManager myGameManager;

	float EndOfScreen = -8.5f;

	bool firstHomeScreenTransition = false;
	bool firstFrameHomeScreenTransition = true;
	bool firstHomeScreenTransitionCompleted = false;
	bool quitScreenTransition = false;
	bool firstFrameQuitScreenTransition = true;
	bool quitScreenTransitionCompleted = false;

	bool homeScreenTransition = false;
	bool homeScreenTransitionCompleted = false;

	bool PauseTransitionFlag = false;
	bool isPaused = false;
	bool GameOverTransitionFlag = false;

	//bool ResetQuitGameTransitionFlag = false;
	float transitionSpeed = 20;
	float StartTransitionSpeed = 15;
	float alphaLevel = 1;
	float quitAlphaLevel = 1;
	Vector3 screen;

	Vector3 StartScreenButtonsInitPos;
	Vector3 QuitScreenInitPos;

	float StartBackgroundAlpha;
	bool resetFirstFrameFlag = true;
//	bool quitScreenFirstFrameFlag = true;
	bool initStartScreenflag;
//	bool isFirstStart = true;
	bool isObstaclesReset = false;

	string PreviousScreen;
	//static float RestartCount = 0;

	//Custom functions
	public void fnStartGameButton () {
		print ("start game");
		//isObstaclesReset = false;
		//TitleScreenTransition= true;

		//print ("First Start: " + isFirstStart);
		//print ("Obstacles Reset: " + isObstaclesReset);
		//print ("Paused: " + isPaused);
		//if (!isFirstStart && !isObstaclesReset && !isPaused) {
		//	FindObjectOfType<ObstacleManager> ().ResetObstacles();
		//	isObstaclesReset = true;
		//}

		//if (TitleScreenTransitionCompleted) {
		//	GameManager.SetBallFallingFlag (true);
//			TitleScreenTransitionCompleted = false;
//			isFirstStart = false;
//			if (isPaused)
//				isPaused = false;
//
//		}
	}

	public void fnPauseGameButton(){
//		print ("pause game");
//		GameManager.SetBallFallingFlag (false);
//		myGameManager.DisplayScore ();
//		PauseTransitionFlag = true;
//		isPaused = true;
//		PreviousScreen = "Pause";
	}

	public void fnQuitGameScreen(){
		if (!QuitScreen.activeSelf) {
			print ("quit screen transition");
			quitScreenTransition = true;
		} else {
			print ("Exit game");
			Application.Quit ();
		}
	}

	public void fnQuitYesButton(){
//		print ("Exit game");
//		Application.Quit ();
	}

	public void fnQuitNoButton(){
//		resetFirstFrameFlag = true;
//		quitAlphaLevel = 1;
//		ResetQuitGameTransitionFlag = true;
//
//		print ("Previous Screen: " + PreviousScreen);
//		if (PreviousScreen == "Start") {
//			isFirstStart = true;
//			resetFirstFrameFlag = true;
//			ResetScreenTransition ();
//		} else if (PreviousScreen == "Pause") {
//			fnPauseGameButton ();
//		} else if (PreviousScreen == "GameOver") {
//			GameOverScreen ();
//		}
	}

	public void fnSoundButton(){
		print ("sound");
	}

	public void fnShareButton(){
		print ("share");
	}

	public void GameOverScreen(){
//		//RestartCount++;
//		GameOverTransitionFlag = true;
//		myGameManager.DisplayScore ();
//		PreviousScreen = "GameOver";
	}

	void Start(){

		myGameManager = FindObjectOfType<GameManager>();
		StartScreenButtonsInitPos = StartScreenButtons.transform.position;
		QuitScreenInitPos = QuitScreen.transform.position;
		//StartBackgroundAlpha = StartBackground.GetComponent<Image>().color.a;

		CurrentScore.SetActive (false);
		HighestScore.SetActive (false);
		Title.SetActive (true);

		PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 0);

		PreviousScreen = "Start";
		firstHomeScreenTransition = true;
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameManager.IsBallFalling ()) {
				//fnPauseGameButton ();
			} else {
				if (!QuitScreen.activeSelf) {
					print ("quit screen transition");
					quitScreenTransition = true;
				} else {
					print ("Exit game");
					Application.Quit ();
				}
			}
		}

		if (firstHomeScreenTransition) {
			ScreenTransition ("EnableFirstHomeScreen");
		}

		if (quitScreenTransition) {
			ScreenTransition ("DisableHomeScreen");
			ScreenTransition ("EnableQuitScreen");
		}
//
//		if (StartScreenTransitionFlag){
//			StartScreenTransition ();
//		}
//
//		if (GameOverTransitionFlag) {
//			ResetScreenTransition ();
//		}
//
//
//		if (PauseTransitionFlag) {
//			ResetScreenTransition ();
//		}
//

//
//		if (ResetQuitGameTransitionFlag) {
//			ResetQuitScreenTransition ();
//		}


	}

	void ScreenTransition(string TransitionType){

		if (TransitionType == "EnableFirstHomeScreen") {
			if (firstFrameHomeScreenTransition) {
				StartScreenButtons.SetActive (true);
				CurrentScore.SetActive (false);
				HighestScore.SetActive (false);
				StartScreenButtons.transform.position = new Vector3 (StartScreenButtonsInitPos.x, EndOfScreen, StartScreenButtonsInitPos.z);
				foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
					myImage.color = new Color (1f, 1f, 1f, 0);
				}
				foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
					myText.color = new Color (1f, 1f, 1f, 0);
				}
				firstFrameHomeScreenTransition = false;
			}

			StartScreenButtons.transform.Translate (0, Time.deltaTime * StartTransitionSpeed, 0);
			if (alphaLevel <= 1)
				alphaLevel += Time.deltaTime * 1f;

			foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
				myImage.color = new Color (1f, 1f, 1f, alphaLevel);
			}
			foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
				myText.color = new Color (1f, 1f, 1f, alphaLevel);
			}

			if (StartScreenButtons.transform.position.y >= StartScreenButtonsInitPos.y) {
				foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
					myImage.color = new Color (1f, 1f, 1f, 1f);
				}
				foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
					myText.color = new Color (1f, 1f, 1f, 1f);
				}

				StartScreenButtons.transform.position = StartScreenButtonsInitPos;

				if (firstHomeScreenTransition)
					firstHomeScreenTransition = false;

				firstFrameHomeScreenTransition = true;

			}
		} else if (TransitionType == "EnableQuitScreen") {
			if (firstFrameQuitScreenTransition) {
				QuitScreen.SetActive (true);
				QuitScreen.transform.position = new Vector3 (QuitScreenInitPos.x, EndOfScreen, QuitScreenInitPos.z);
				foreach (Text myText in QuitScreen.GetComponentsInChildren<Text>()) {
					myText.color = new Color (1f, 1f, 1f, 0);
				}
				firstFrameQuitScreenTransition = false;
			}

			QuitScreen.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);

			if (quitAlphaLevel <= 1) {
				quitAlphaLevel += Time.deltaTime * 1f;
			}
			foreach (Text myText in QuitScreen.GetComponentsInChildren<Text>()) {
				myText.color = new Color (1f, 1f, 1f, quitAlphaLevel);
			}

			if (QuitScreen.transform.position.y >= QuitScreenInitPos.y) {
				foreach (Text myText in QuitScreen.GetComponentsInChildren<Text>()) {
					myText.color = new Color (1f, 1f, 1f, 1f);
				}
				QuitScreen.transform.position = QuitScreenInitPos;

				if (quitScreenTransition)
					quitScreenTransition = false;

				firstFrameQuitScreenTransition = true;
			}
		}

		else if (TransitionType == "DisableHomeScreen") {
			
			StartScreenButtons.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
			if (alphaLevel >= 0)
				alphaLevel -= Time.deltaTime * 5f;

			foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
				myImage.color = new Color (1f, 1f, 1f, alphaLevel);
			}
			foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
				myText.color = new Color (1f, 1f, 1f, alphaLevel);
			}

			screen = Camera.main.WorldToViewportPoint (StartScreenButtons.transform.position);
			if(screen.y > 0.75f){
	
				StartScreenButtons.SetActive (false);
	
				PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
				ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 1);
	
//				StartScreenTransitionFlag = false;
//				startScreenTransitionCompleted = true;
			}
		}
	}

	void StartScreenTransition(){

//		StartScreenButtons.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
//
//		if (alphaLevel >= 0)
//			alphaLevel -= Time.deltaTime * 5f;
//
//		foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
//			myImage.color = new Color (1f, 1f, 1f, alphaLevel);
//		}
//		foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
//			myText.color = new Color (1f, 1f, 1f, alphaLevel);
//		}
//		//StartBackground.GetComponent<Image>().color = new Color (1, 1, 1, alphaLevel);
//
//		PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 1 - alphaLevel);
//		ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 1 - alphaLevel);
//
//		screen = Camera.main.WorldToViewportPoint (StartScreenButtons.transform.position);
//		if(screen.y > 0.75f){
//
//			StartScreenButtons.SetActive (false);
//			//StartBackground.SetActive (false);
//
//			if (!isFirstStart) {
//				Title.SetActive (false);
//			} else {
//				CurrentScore.SetActive (false);
//				HighestScore.SetActive (false);
//			}
//
//			PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
//			ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 1);
//
//			StartScreenTransitionFlag = false;
//			startScreenTransitionCompleted = true;
//		}
	}
		

//	void ResetScreenTransition (){
//
//		if (resetFirstFrameFlag) {
//
//			StartScreenButtons.SetActive (true);
//			//StartBackground.SetActive (true);
//
//			if (isFirstStart) {
//				Title.SetActive (true);
//				CurrentScore.SetActive (false);
//				HighestScore.SetActive (false);
//			} else {
//				Title.SetActive (false);
//				CurrentScore.SetActive (true);
//				HighestScore.SetActive (true);
//			}
//
//			StartScreenButtons.transform.position = new Vector3 (StartScreenButtonsInitPos.x, EndOfScreen, StartScreenButtonsInitPos.z);
//			resetFirstFrameFlag = false;
//		}
//
//		if (isFirstStart) {
//			StartScreenButtons.transform.Translate (0, Time.deltaTime * StartTransitionSpeed, 0);
//		} else {
//			StartScreenButtons.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
//		}
//
//		if (alphaLevel <= 1) {
//
//			//if(isFirstStart)
//				alphaLevel += Time.deltaTime * 1f;
//			//else
//			//	alphaLevel += Time.deltaTime * 5f;
//		}
//		foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
//			myImage.color = new Color (1f, 1f, 1f, alphaLevel);
//		}
//		foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
//			myText.color = new Color (1f, 1f, 1f, alphaLevel);
//		}
//
//		//StartBackground.GetComponent<Image>().color = new Color (1, 1, 1, StartBackgroundAlpha);
//
//		PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
//		ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 0);
//
//		if (StartScreenButtons.transform.position.y >= StartScreenButtonsInitPos.y) {
//			foreach (Image myImage in StartScreenButtons.GetComponentsInChildren<Image>()) {
//				myImage.color = new Color (1f, 1f, 1f, 1f);
//			}
//			foreach (Text myText in StartScreenButtons.GetComponentsInChildren<Text>()) {
//				myText.color = new Color (1f, 1f, 1f, 1f);
//			}
//
//			StartScreenButtons.transform.position = StartScreenButtonsInitPos;
//
//			if (PauseTransitionFlag)
//				PauseTransitionFlag = false;
//
//			if (GameOverTransitionFlag) {
//				GameOverTransitionFlag = false;
//			}
//
//			if (isFirstStart)
//				isFirstStart = false;
//
//			if (initStartScreenflag)
//				initStartScreenflag = false;
//			resetFirstFrameFlag = true;
//
//		}
//	}

//	void QuitScreenTransition(){
//		if (quitScreenFirstFrameFlag) {
//
//			QuitScreen.SetActive (true);
//			//StartBackground.SetActive (true);
//
//			QuitScreen.transform.position = new Vector3 (QuitScreenInitPos.x, EndOfScreen, QuitScreenInitPos.z);
//			quitScreenFirstFrameFlag = false;
//		}
//			
//		QuitScreen.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
//
//		if (quitAlphaLevel <= 1) {
//			quitAlphaLevel += Time.deltaTime * 1f;
//		}
//		foreach (Text myText in QuitScreen.GetComponentsInChildren<Text>()) {
//			myText.color = new Color (1f, 1f, 1f, quitAlphaLevel);
//		}
//
//		if (QuitScreen.transform.position.y >= QuitScreenInitPos.y) {
//			foreach (Text myText in QuitScreen.GetComponentsInChildren<Text>()) {
//				myText.color = new Color (1f, 1f, 1f, 1f);
//			}
//			QuitScreen.transform.position = QuitScreenInitPos;
//
//			if (QuitGameTransitionFlag)
//				QuitGameTransitionFlag = false;
//			
//			quitScreenFirstFrameFlag = true;
//		}
//	}
//
//	void ResetQuitScreenTransition(){
//		QuitScreen.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
//
//		if (quitAlphaLevel >= 0)
//			quitAlphaLevel -= Time.deltaTime * 5f;
//
//		foreach (Text myText in QuitScreen.GetComponentsInChildren<Text>()) {
//			myText.color = new Color (1f, 1f, 1f, quitAlphaLevel);
//		}
//
//		screen = Camera.main.WorldToViewportPoint (QuitScreen.transform.position);
//		if(screen.y > 0.75f){
//
//			QuitScreen.SetActive (false);
//
//			ResetQuitGameTransitionFlag = false;
//		}
//	}
}
