using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameManager GameManagerScript;
	ObstacleManager ObstacleManagerScript;

	//public Animator StartScreenAnimator;
	//public Animator QuitScreenAnimator;
	public Animator BackgroundAnimator;
	public GameObject StartScreenAnimator;
	//public Animator CheckpointAnimator;
	//public Animator SuperSpeedAnimator;
	//public Animator SlowDownAnimator;
//	public Animator BackgroundHills;
//	public Animator BackgroundColor;
//	public Animator BallAnimator;
	public GameObject StartScreenButtons;
	public GameObject QuitScreen;
	public GameObject Title;
	public GameObject TitleBG;
	public GameObject LifeCoins;
	public GameObject PauseButton;
	public GameObject ScoreText;
	public GameObject CelebrateHighScoreObj;
	public GameObject ScoreLabel;
	public GameObject HighScoreLabel;
	public GameObject HighScoreStar;
	public GameObject Sound;
	public GameObject CheckpointButton;
	//GameObject CheckpointButton;
	Animator CheckpointAnimator;
	AnimationClip EnableCheckpointClip;

	public GameObject SuperSpeedButton;
	public GameObject SlowDownButton;
	public GameObject SuperSpeedTransparent;

	public GameObject HintScreen;

	public Sprite SoundOn;
	public Sprite SoundOff;
	public Sprite checkpointOn;
	public Sprite checkpointOff;
	public Sprite superspeedOn;
	public Sprite superspeedOff;
	public Sprite slowdownOn;
	public Sprite slowdownOff;
	public Sprite LifeOn;
	public Sprite LifeOff;

	bool gameOverFlag = false;
	bool quitDisplayed = false;
	static bool hasGameStarted = false;
	public static bool celebrateHighScore = false;
	public static bool enableCheckpoint = false;
	public static bool enableSuperSpeed = false;
	public static bool enableSlowDown = false;
	public static float currentCheckpointScore;
	public static bool checkpoint = false;

	bool IsCheckpointShown = false;
	bool IsCheckpointActive = false;

	bool IsSuperSpeedButtonShown = false;
	bool IsSuperSpeedActive = false;
	public bool IsSuperSpeed = false;
	int SuperSpeedNumOfObstacles = 5;
	float SuperSpeedStartScore, SuperSpeedEndScore;

	bool IsSlowDownButtonShown = false;
	bool IsSlowDownActive = false;
	public bool IsSlowDown = false;
	int SlowDownNumOfObstacles = 2;
	float SlowDownStartScore, SlowDownEndScore;

	bool IsLifeAvailable = false;
	bool LifeTaken = false;
	bool EnableStartButtonGlow = false;

	bool IsHintActive = false;
	int HintCheckpointShown = 0;
	int HintSuperspeedShown = 0;
	int HintSlowdownShown = 0;
	int LifecoinShown = 0;

	bool firstTimeCheckpoint = true;
	bool firstTimeSlowDown = true;
	bool firstTimeSuperSpeed = true;
	bool firstTimeQuitScreen = true;

	//string[] HintsAndAds = new string[] {"ads", "earncoins", "slowdown", "superspeed", "checkpoint", "lifecoin"};
	int hintSize;

	GameManager myGameManager;

	void Awake(){
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.DeleteKey("lifeCoins");
		//PlayerPrefs.DeleteKey("lifeAvailable");
		if (!PlayerPrefs.HasKey ("sound")) {
			PlayerPrefs.SetInt ("sound", 1);
		}

		if (!PlayerPrefs.HasKey ("lifeCoins")) {
			PlayerPrefs.SetInt ("lifeCoins", 0);
		}

		if (!PlayerPrefs.HasKey ("lifeAvailable")) {
			PlayerPrefs.SetInt ("lifeAvailable", 0);
		}

		if (!PlayerPrefs.HasKey ("HintShown")) {
			PlayerPrefs.SetInt ("HintShown", 0);
		}

		GameManagerScript = FindObjectOfType<GameManager>();
		ObstacleManagerScript = FindObjectOfType<ObstacleManager>();

		//hintSize = HintsAndAds.Length;
	}

	void Start(){
		//SlideInStartScreen ();
		DisablePauseAndScore ();
		FadeInBackground ();
		DisableScoreLabels ();
		DisableCelebrateHighScore ();
		DisableHighScoreStar ();
		EnableStartScreenAnimation ();
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
				TitleBG.SetActive (false);
				GameManager.SetBallFallingFlag (false);
				GameManagerScript.DisplayScore ();
				SlideInStartScreen ();
				DisablePauseAndScore ();
				DisableHighScoreStar ();
				EnableScoreLabels ();
				FadeInBackground ();
				HideCheckpointButton ();
				HideSuperSpeedButton ();
				HideSlowDownButton ();

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

		if (enableSuperSpeed) {
			EnableSuperSpeedButton();
			enableSuperSpeed = false;
		}

		if (enableSlowDown) {
			EnableSlowDownButton();
			enableSlowDown = false;
		}

		if (IsSuperSpeed) {
			float myScore = GameManagerScript.score;
			if (myScore >= SuperSpeedStartScore + SuperSpeedNumOfObstacles) {
				StopSuperSpeed ();
			}
		}

		if (IsSlowDown) {
			float myScore = GameManagerScript.score;
			if (myScore >= SlowDownStartScore + SlowDownNumOfObstacles) {
				StopSlowDown ();
			}
		}

		if (EnableStartButtonGlow || checkpoint) {
			StartScreenButtons.transform.FindChild ("StartButtonGlow").gameObject.SetActive (true);
			EnableStartButtonGlow = false;
		}

		if (gameOverFlag) {
			//show hint
		}

	}

	public void StartGame(){
		hasGameStarted = true;
		celebrateHighScore = false;
		StartScreenButtons.transform.FindChild ("StartButtonGlow").gameObject.SetActive (false);
		SlideOutStartScreen ();
		DisableStartScreenAnimation ();
		//MoveBallToTop ();
		GameManager.SetBallFallingFlag (true);
		print ("Score: " + GameManagerScript.score);
		if (gameOverFlag) {
		//if (gameOverFlag && !checkpoint) {
			print ("Checkpoint?: " + checkpoint);
			if (LifeTaken) {
				print ("Life taken");
				LifeTaken = false;
			} else if (checkpoint) {
				ObstacleManagerScript.ResetToCheckpoint ();
				gameOverFlag = false;
				ResetCheckpointValues ();
				GameManagerScript.score = UIManager.currentCheckpointScore;
				print ("Updating score: " + GameManagerScript.score);
				GameManagerScript.DisplayScore();
				GameManager.nextSuperspeed = currentCheckpointScore + GameManager.superspeedRepeat;
				GameManager.nextSlowdown = currentCheckpointScore + GameManager.slowdownRepeat;
			} else {
				print ("Life not taken");
				ObstacleManagerScript.ResetObstacles ();
				gameOverFlag = false;
				GameManagerScript.score = 0;
				print ("Updating score: " + GameManagerScript.score);
				GameManagerScript.DisplayScore();
				DisableCheckpointButton ();
				DisableSuperSpeedButton ();
				DisableSlowDownButton ();
				GameManager.nextCheckpoint = GameManager.checkpointInitScore;
				GameManager.nextSuperspeed = GameManager.superspeedInitScore;
				GameManager.nextSlowdown = GameManager.slowdownInitScore;
			}
		}

//		if (gameOverFlag && checkpoint) {
//			ObstacleManagerScript.ResetToCheckpoint ();
//			gameOverFlag = false;
//			ResetCheckpointValues ();
//			GameManagerScript.score = UIManager.currentCheckpointScore;
//			print ("Updating score: " + GameManagerScript.score);
//			GameManagerScript.DisplayScore();
//			GameManager.nextSuperspeed = currentCheckpointScore + GameManager.superspeedRepeat;
//			GameManager.nextSlowdown = currentCheckpointScore + GameManager.slowdownRepeat;
//
//		} else {
//			//GameManager.nextCheckpoint = GameManager.checkpointInitScore;
//		}

		EnablePauseAndScore ();
		DisableHighScoreStar ();
		FadeOutBackground ();
		DisableScoreLabels ();
		DisableCelebrateHighScore ();
		ShowCheckpointButton ();
		ShowSuperSpeedButton ();
		ShowSlowDownButton ();

		HideAllHints ();

		//HideCheckpointButton ();
		//MoveInBackgroundHills ();
		//BackgroundDarkToLight ();

		//FindObjectOfType<GameManager> ().DisplayScore();
	}

	public void GameOver(){
		gameOverFlag = true;
		Title.SetActive (false);
		TitleBG.SetActive (false);
		SlideInStartScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		FadeInBackground ();
		CalculateLife ();
		EnableScoreLabels ();
//		DisableCheckpointButton ();
//		DisableSuperSpeedButton ();
//		DisableSlowDownButton ();
		HideCheckpointButton ();
		HideSuperSpeedButton ();
		HideSlowDownButton ();
		EnableHintScreen ();
		if (celebrateHighScore) {
			EnableCelebrateHighScore ();
			celebrateHighScore = false;
		}

		if(IsSlowDown){
			IsSlowDown = false;
			SuperSpeedTransparent.SetActive (false);
		}
		//MoveOutBackgroundHills ();

	}

	public void PauseGame(){
		Title.SetActive (false);
		TitleBG.SetActive (false);
		GameManager.SetBallFallingFlag (false);
		GameManagerScript.DisplayScore ();
		SlideInStartScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		FadeInBackground ();
		EnableScoreLabels ();
		HideCheckpointButton ();
		HideSuperSpeedButton ();
		HideSlowDownButton ();
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
		LifeCoins.SetActive (false);
	}

	public void EnableScoreLabels(){
//		ScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 1);
//		HighScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 1);
		ScoreLabel.SetActive(true);
		HighScoreLabel.SetActive (true);
		LifeCoins.SetActive (true);
	}

	public void DisableHighScoreStar(){
		HighScoreStar.SetActive (false);
	}

	public void EnableHighScoreStar(){
		HighScoreStar.SetActive (true);
	}


	public void SlideInQuitScreen(){
		if (firstTimeQuitScreen) {
			QuitScreen.GetComponent<Animator> ().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("Animations/QuitPanel", typeof(RuntimeAnimatorController)));
			firstTimeQuitScreen = false;
		}
		QuitScreen.GetComponent<Animator>().SetBool ("SlideInQuitScreen", true);
	}

	public void SlideOutQuitScreen(){

		QuitScreen.GetComponent<Animator>().SetBool ("SlideInQuitScreen", false);
	}

	public void SlideInStartScreen(){

		StartScreenButtons.GetComponent<Animator>().SetBool ("SlideInStartScreen", true);
	}

	public void SlideOutStartScreen(){

		StartScreenButtons.GetComponent<Animator>().SetBool ("SlideInStartScreen", false);
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
		//CheckpointButton.GetComponent<Animator>().SetBool ("EnableCheckpointButton", true);
		CheckpointButton.GetComponent<Animator>().SetBool ("EnableCheckpointButton", true);
		CheckpointButton.GetComponentInChildren<Text> ().text = "+";
		CheckpointButton.GetComponentInChildren<Text> ().fontSize = 80;
		CheckpointButton.GetComponent<Image> ().sprite = checkpointOn;

		IsCheckpointShown = true;
	}

	public void DisableCheckpointButton(){
		CheckpointButton.GetComponent<Animator>().SetBool ("EnableCheckpointButton", false);
	}

	public void ShowCheckpointButton(){
		if (firstTimeCheckpoint) {
			CheckpointButton.GetComponent<Animator> ().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("Animations/CheckpointButton", typeof(RuntimeAnimatorController)));
			firstTimeCheckpoint = false;
		}

		CheckpointButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		CheckpointButton.GetComponentInChildren<Text>().color = new Color (1, 1, 1, 1);
		CheckpointButton.GetComponent<Button> ().interactable = true;
	}

	public void HideCheckpointButton(){
		CheckpointButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		CheckpointButton.GetComponentInChildren<Text>().color = new Color (1, 1, 1, 0);
		CheckpointButton.GetComponent<Button> ().interactable = false;
	}

	public void AddCheckpoint(){
		if (IsCheckpointActive) {
			float currScore = GameManagerScript.score;
			float interval = GameManager.checkpointRepeat;
			currentCheckpointScore = currScore;
			GameManager.nextCheckpoint = currScore + interval;
			checkpoint = true;
			print ("Checkpoint at: " + currentCheckpointScore);
			ObstacleManagerScript.SaveCheckpointDetails ();
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

	public void StartSuperSpeed(){
		if (IsSuperSpeedActive) {
			IsSuperSpeed = true;
			SuperSpeedTransparent.SetActive (true);
			SuperSpeedStartScore = GameManagerScript.score;
			print (Time.time + ": Super Speed started at obstacle: " + SuperSpeedStartScore);
			SuperSpeedButton.GetComponent<Image> ().sprite = superspeedOff;
			IsSuperSpeedActive = false;

			HideCheckpointButton ();
			HideSuperSpeedButton ();
			HideSlowDownButton ();
		}
	}

	public void StopSuperSpeed(){
		IsSuperSpeed = false;
		SuperSpeedTransparent.SetActive (false);
		SuperSpeedEndScore = GameManagerScript.score;
		print (Time.time + ": Super Speed stopped at obstacle: " + SuperSpeedEndScore);
		ObstacleManagerScript.DisableColliders ();

		float currScore = GameManagerScript.score;
		float interval = GameManager.superspeedRepeat;
		GameManager.nextSuperspeed = currScore + interval;

		ShowCheckpointButton ();
		ShowSuperSpeedButton ();
		ShowSlowDownButton ();
	}

	public void EnableSuperSpeedButton(){
		IsSuperSpeedActive = true; 
		SuperSpeedButton.GetComponent<Animator>().SetBool ("EnableSuperSpeedButton", true);
		SuperSpeedButton.GetComponent<Image> ().sprite = superspeedOn;

		IsSuperSpeedButtonShown = true;
	}

	public void DisableSuperSpeedButton(){
		SuperSpeedButton.GetComponent<Animator>().SetBool ("EnableSuperSpeedButton", false);
	}

	public void ShowSuperSpeedButton(){
		if (firstTimeSuperSpeed) {
			SuperSpeedButton.GetComponent<Animator> ().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("Animations/SuperSpeedButton", typeof(RuntimeAnimatorController)));
			firstTimeSuperSpeed = false;
		}
		SuperSpeedButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		SuperSpeedButton.GetComponent<Button> ().interactable = true;
	}

	public void HideSuperSpeedButton(){
		SuperSpeedButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		SuperSpeedButton.GetComponent<Button> ().interactable = false;
	}



	public void StartSlowDown(){
		if (IsSlowDownActive) {
			IsSlowDown = true;
			SuperSpeedTransparent.SetActive (true);
			SlowDownStartScore = GameManagerScript.score;
			print (Time.time + ": Slow DOwn started at obstacle: " + SlowDownStartScore);
			SlowDownButton.GetComponent<Image> ().sprite = slowdownOff;
			IsSlowDownActive = false;

			HideCheckpointButton ();
			HideSuperSpeedButton ();
			HideSlowDownButton ();
		}
	}

	public void StopSlowDown(){
		IsSlowDown = false;
		SuperSpeedTransparent.SetActive (false);
		SlowDownEndScore = GameManagerScript.score;
		print (Time.time + ": Slow Down stopped at obstacle: " + SlowDownEndScore);
		ObstacleManagerScript.DisableColliders ();

		float currScore = GameManagerScript.score;
		float interval = GameManager.slowdownRepeat;
		GameManager.nextSlowdown = currScore + interval;

		ShowCheckpointButton ();
		ShowSuperSpeedButton ();
		ShowSlowDownButton ();
	}

	public void EnableSlowDownButton(){
		IsSlowDownActive = true; 
		SlowDownButton.GetComponent<Animator>().SetBool ("EnableSlowDownButton", true);
		SlowDownButton.GetComponent<Image> ().sprite = slowdownOn;

		IsSlowDownButtonShown = true;
	}

	public void DisableSlowDownButton(){
		SlowDownButton.GetComponent<Animator>().SetBool ("EnableSlowDownButton", false);
	}

	public void ShowSlowDownButton(){
		if (firstTimeSlowDown) {
			SlowDownButton.GetComponent<Animator> ().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("Animations/SlowDownButton", typeof(RuntimeAnimatorController)));
			firstTimeSlowDown = false;
		}
		SlowDownButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		SlowDownButton.GetComponent<Button> ().interactable = true;
	}

	public void HideSlowDownButton(){
		SlowDownButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		SlowDownButton.GetComponent<Button> ().interactable = false;
	}

	void CalculateLife (){
		if (PlayerPrefs.GetFloat("lifeAvailable") == 0) {
			LifeCoins.transform.FindChild ("LifeCoinButton").GetComponent<Image> ().sprite = LifeOff;
			LifeCoins.transform.FindChild ("LifeCoin glow").gameObject.SetActive (false);
		} else {
			LifeCoins.transform.FindChild ("LifeCoinButton").GetComponent<Image> ().sprite = LifeOn;
			LifeCoins.transform.FindChild ("LifeCoin glow").gameObject.SetActive (true);
			LifeCoins.transform.FindChild ("LifeAvailable").GetComponent<Text> ().text = PlayerPrefs.GetFloat("lifeAvailable").ToString();
		}
	}

	public void AnotherLife(){
		if (PlayerPrefs.GetFloat ("lifeAvailable") > 0 && !LifeTaken) {
			LifeTaken = true;
			EnableStartButtonGlow = true;
			PlayerPrefs.SetFloat ("lifeAvailable", PlayerPrefs.GetFloat ("lifeAvailable") - 1);
			if (PlayerPrefs.GetFloat ("lifeAvailable") == 0) {
				LifeCoins.transform.FindChild ("LifeAvailable").GetComponent<Text> ().text = "";
				LifeCoins.transform.FindChild ("LifeCoinButton").GetComponent<Image> ().sprite = LifeOff;
				LifeCoins.transform.FindChild ("LifeCoin glow").gameObject.SetActive (false);
			} else {
				LifeCoins.transform.FindChild ("LifeAvailable").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeAvailable").ToString ();
			}
		}
	}

	void EnableHintScreen(){
		int currHintNum;
		float currHighScore = PlayerPrefs.GetFloat ("highscore");
		IsHintActive = true;
		HintScreen.SetActive (true);
		hintSize = 10;
		if (currHighScore < 30) {
			currHintNum = Random.Range (0, hintSize);
		} else if (currHighScore < 50) {
			currHintNum = Random.Range (0, hintSize - 1);
		} else if (currHighScore < 70) {
			currHintNum = Random.Range (0, hintSize - 2);
		} else if (currHighScore < 90) {
			currHintNum = Random.Range (0, hintSize - 3);
		} else {
			currHintNum = Random.Range (0, hintSize - 4);
		}
			
		//string[] HintsAndAds = new string[] {"ads", "earncoins", "slowdown", "superspeed", "checkpoint", "lifecoin"};
		switch (currHintNum) {
		case 0:
		case 1:
		case 2:
			HintScreen.transform.FindChild ("Ads").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInAds", -1, 0);
			break;
		case 3:
		case 4:
		case 5:
			HintScreen.transform.FindChild ("Earn Coins").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInAds", -1, 0);
			break;
		case 6:
			HintScreen.transform.FindChild ("Hint Slowdown").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 7:
			HintScreen.transform.FindChild ("Hint Superspeed").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 8:
			HintScreen.transform.FindChild ("Hint Checkpoint").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 9:
			HintScreen.transform.FindChild ("Hint Lifecoins").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		}
	}

	void DisableHintScreen(){
		IsHintActive = false;
	}

	void HideAllHints(){
		HintScreen.transform.FindChild ("Hint Lifecoins").gameObject.SetActive (false);
		HintScreen.transform.FindChild ("Hint Slowdown").gameObject.SetActive (false);
		HintScreen.transform.FindChild ("Hint Superspeed").gameObject.SetActive (false);
		HintScreen.transform.FindChild ("Hint Checkpoint").gameObject.SetActive (false);
		HintScreen.transform.FindChild ("Ads").gameObject.SetActive (false);
		HintScreen.transform.FindChild ("Earn Coins").gameObject.SetActive (false);

		HintScreen.SetActive (false);
	}

	void EnableStartScreenAnimation(){
		StartScreenAnimator.SetActive (true);
	}

	void DisableStartScreenAnimation(){
		StartScreenAnimator.SetActive (false);
	}
}
