using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameManager GameManagerScript;
	AudioManager AudioManagerScript;
	ObstacleManager ObstacleManagerScript;
	//AdManager AdManagerScript;

	public Animator BackgroundAnimator;
	public GameObject StartScreenAnimator;
	public GameObject Canvas;
	public GameObject StartScreenButtons;

	public GameObject BackgroundImage;
	public GameObject BonusButtons;
	public GameObject CheckpointButton;
	public GameObject SuperSpeedButton;
	public GameObject SlowDownButton;
	public GameObject CoinsCounter;
	public GameObject SuperSpeedTransparent;

	public GameObject QuitScreen;
	public GameObject AboutScreen;
	public GameObject ChanceScreen;
	public GameObject ChanceButton;
	public GameObject ChanceLabel;
	public GameObject Title;
	public GameObject TitleBG;
	public GameObject AnotherChance;
	public GameObject CoinsCreditedPopup;
	public GameObject PauseButton;
	public GameObject ScoreText;
	public GameObject CelebrateHighScoreObj;
	public GameObject ScoreLabel;
	public GameObject HighScoreLabel;
	public GameObject HighScoreStar;
	public GameObject Sound;

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
	public static bool enableCoinsCounter = false;
	public static float currentCheckpointScore;
	public static bool checkpoint = false;

	bool chanceScreen = false;
	bool aboutScreen = false;
	public static bool shareScreen= false;

	bool IsCheckpointActive = false;
	bool IsSuperSpeedActive = false;

	public bool IsSuperSpeed = false;
	int SuperSpeedNumOfObstacles = 5;
	float SuperSpeedStartScore, SuperSpeedEndScore;

	bool IsSlowDownActive = false;
	public bool IsSlowDown = false;
	int SlowDownNumOfObstacles = 2;
	float SlowDownStartScore, SlowDownEndScore;

	bool LifeTaken = false;
	bool EnableStartButtonGlow = false;

	bool bgColorChanged = false;
	bool bgDark = false;
	//string[] HintsAndAds = new string[] {"ads", "earncoins", "slowdown", "superspeed", "checkpoint", "lifecoin"};
	int hintSize;
	bool highScoreOnScreen;

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
		AudioManagerScript = FindObjectOfType<AudioManager>();
		//AdManagerScript = FindObjectOfType<AdManager>();

		//hintSize = HintsAndAds.Length;
	}

	void Start(){
		//SlideInStartScreen ();
		DisablePauseAndScore ();
		//FadeInBackground ();
		DisableScoreLabels ();
		DisableCelebrateHighScore ();
		DisableHighScoreStar ();
		EnableStartScreenAnimation ();

		//Bonus Buttons
		HideCheckpointButton ();
		HideSuperSpeedButton ();
		HideSlowDownButton ();
		HideCoinsCounterButton ();

		if (PlayerPrefs.GetInt("sound")==1) {
			Sound.GetComponent<Image> ().sprite = SoundOn;
		} else if(PlayerPrefs.GetInt("sound")==0){
			Sound.GetComponent<Image> ().sprite = SoundOff;
		}

		hasGameStarted = false;
		highScoreOnScreen = false;
		//Play theme music
		AudioManagerScript.Play("StartTheme");

//		//test
//		hasGameStarted = true;
//		GameManager.SetBallFallingFlag (true);

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameManager.IsBallFalling ()) {
				Title.SetActive (false);
				TitleBG.SetActive (false);
				StartScreenButtons.transform.Find ("GamePauseLabel").gameObject.SetActive (true);
				StartScreenButtons.transform.Find ("GameOverLabel").gameObject.SetActive (false);
//				Destroy(Title);
//				Destroy (TitleBG);
				GameManager.SetBallFallingFlag (false);
				GameManagerScript.DisplayScore ();
				SlideInStartScreen ();
				DisablePauseAndScore ();
				DisableHighScoreStar ();
				EnableScoreLabels ();
				FadeInBackground ();

				SlideOutBonusButtons ();

				//Play theme music
				AudioManagerScript.Stop ("Theme");
				AudioManagerScript.Play ("StartTheme");

			} else if (chanceScreen) {
				BackFromChance ();
			} else if (aboutScreen) {
				BackFromAbout ();
			} else if (shareScreen) {
				ShareManager.FindObjectOfType<ShareManager> ().CancelShare ();
			} else {
				if (!quitDisplayed) {
					QuitGameScreen ();
				} else {
					print ("Exit game");
					Application.Quit ();
				}
			}
		}

		if (celebrateHighScore && !highScoreOnScreen) {
			EnableHighScoreStar ();
			highScoreOnScreen = true;
		}

		if (enableCheckpoint){
			print ("Enable checkpoint");
			IsCheckpointActive = true;
			ShowCheckpointButton ();
			enableCheckpoint = false;
		}

		if (enableSuperSpeed) {
			print ("Enable superspeed");
			IsSuperSpeedActive = true; 
			ShowSuperSpeedButton();
			enableSuperSpeed = false;
		}

		if (enableSlowDown) {
			print ("Enable Slowdown");
			IsSlowDownActive = true;
			ShowSlowDownButton();
			enableSlowDown = false;
		}

		if (enableCoinsCounter) {
			print ("Enable CoinsCounter");
			ShowCoinsCounterButton ();
			enableCoinsCounter = false;
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
			StartScreenButtons.transform.Find ("StartButtonGlow").gameObject.SetActive (true);
			EnableStartButtonGlow = false;
		}

		if (gameOverFlag) {
			//show hint
		}

		/*
		if (AdManagerScript.AdReturn) {
			
			if (AdManagerScript.AdSeen) {
				print ("Ad Seen");
				CoinsCreditedPopup.GetComponent<Animator> ().Play ("CoinsCredited");
				//AnotherChance.GetComponent<Animator> ().SetBool ("CoinsCredited", true);
				//yield return new WaitUntil (() => AdManagerScript.AdReturn);
				PlayerPrefs.SetFloat ("lifeCoins", PlayerPrefs.GetFloat ("lifeCoins") + GameManager.lifecoinRedeem / 2);
				if (PlayerPrefs.GetFloat("lifeCoins") >= GameManager.lifecoinRedeem) {
					//AnotherChance.transform.FindChild ("LifeAvailable").GetComponent<Text> ().text = "";
					AnotherChance.transform.Find ("HeartButton").GetComponent<Image> ().sprite = LifeOn;
					AnotherChance.transform.Find ("LifeCoin glow").gameObject.SetActive (true);
					AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeCoins").ToString ();
				} else {
					AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeCoins").ToString ();
				}

				AdManagerScript.AdSeen = false;
			}
			AdManagerScript.AdReturn = false;
		}
		*/

		if (GameManagerScript.score % GameManager.backgroundColorChange == 0 && GameManagerScript.score != 0) {
			if (!bgColorChanged) {
				print ("Time to change BG color");
				if (!bgDark) {
					BackgroundImage.GetComponent<Animator> ().Play ("backgroundColorDark");
					bgDark = true;
				}
				else {
					BackgroundImage.GetComponent<Animator> ().Play ("backgroundColorLight");
					bgDark = false;
				}
				bgColorChanged = true;
			}
		} else {
			bgColorChanged = false;
		}

	}

	public void StartGame(){
		hasGameStarted = true;
		celebrateHighScore = false;
		highScoreOnScreen = false;
		StartScreenButtons.transform.Find ("StartButtonGlow").gameObject.SetActive (false);
		StartScreenButtons.transform.Find ("GamePauseLabel").gameObject.SetActive (false);
		StartScreenButtons.transform.Find ("GameOverLabel").gameObject.SetActive (false);
		//Play theme music
		AudioManagerScript.Stop("StartTheme");
		AudioManagerScript.Play("Theme");

		SlideOutStartScreen ();
		DisableStartScreenAnimation ();
		//MoveBallToTop ();
		GameManager.SetBallFallingFlag (true);
		print ("Score: " + GameManagerScript.score);
		if (gameOverFlag) {
		//if (gameOverFlag && !checkpoint) {

			if (LifeTaken) {
				print ("Life taken");
				ObstacleManagerScript.ResetObstaclesToCurrentScore ();
				gameOverFlag = false;
				LifeTaken = false;
				if (checkpoint)
					checkpoint = false;
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

				HideCheckpointButton ();
				HideSuperSpeedButton ();
				HideSlowDownButton ();
				HideCoinsCounterButton ();

				ResetBackgroundColor ();
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
		UpdateCoinsCounter ();
		SlideInBonusButtons ();
		//ShowCheckpointButton ();
		//ShowSuperSpeedButton ();
		//ShowSlowDownButton ();

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
		StartScreenButtons.transform.Find ("GamePauseLabel").gameObject.SetActive (false);
		StartScreenButtons.transform.Find ("GameOverLabel").gameObject.SetActive (true);
		SlideInStartScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		FadeInBackground ();
		CalculateLife ();
		EnableScoreLabels ();
//		DisableCheckpointButton ();
//		DisableSuperSpeedButton ();
//		DisableSlowDownButton ();
//		HideCheckpointButton ();
//		HideSuperSpeedButton ();
//		HideSlowDownButton ();
		SlideOutBonusButtons();
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
		AudioManagerScript.Stop("Theme");
		AudioManagerScript.Play("StartTheme");
	
		//AdManagerScript.showBanner = true;
	}

	public void PauseGame(){
		Title.SetActive (false);
		TitleBG.SetActive (false);
		StartScreenButtons.transform.Find ("GamePauseLabel").gameObject.SetActive (true);
		StartScreenButtons.transform.Find ("GameOverLabel").gameObject.SetActive (false);
		GameManager.SetBallFallingFlag (false);
		GameManagerScript.DisplayScore ();
		SlideInStartScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		FadeInBackground ();
		EnableScoreLabels ();
//		HideCheckpointButton ();
//		HideSuperSpeedButton ();
//		HideSlowDownButton ();
		SlideOutBonusButtons();

		AudioManagerScript.Stop("Theme");
		AudioManagerScript.Play("StartTheme");
	}

	public void QuitGameScreen(){
		SlideOutStartScreen ();
		GameManager.SetBallFallingFlag (false);
		SlideInQuitScreen ();
		DisablePauseAndScore ();
		DisableHighScoreStar ();
		quitDisplayed = true;

		FadeInBackground ();
		//HideCheckpointButton ();
	}

	public void AnotherChanceScreen(){
		SlideOutStartScreen ();
		//GameManager.SetBallFallingFlag (false);
		SlideInChanceScreen ();
		//DisablePauseAndScore ();
		//DisableHighScoreStar ();
		chanceScreen = true;
		//IsCheckpointShown = false;

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

	public void BackFromChance(){
		chanceScreen = false;
		SlideInStartScreen ();
		SlideOutChanceScreen ();
	}

	public void BackFromAbout(){
		aboutScreen = false;
		SlideInStartScreen ();
		SlideOutAboutScreen ();
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
		CelebrateHighScoreObj.GetComponent<Image>().color = new Color (1, 1, 1, 0);
	}

	public void EnableCelebrateHighScore(){
		CelebrateHighScoreObj.GetComponent<Image>().color = new Color (1, 1, 1, 1);
	}


	public void DisableScoreLabels(){
//		ScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 0);
//		HighScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 0);
		ScoreLabel.SetActive(false);
		HighScoreLabel.SetActive (false);
		ChanceButton.SetActive (false);
		ChanceLabel.SetActive (false);
	}

	public void EnableScoreLabels(){
//		ScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 1);
//		HighScoreLabel.GetComponent<Text>().color = new Color (1, 1, 1, 1);
		ScoreLabel.SetActive(true);
		HighScoreLabel.SetActive (true);
		ChanceButton.SetActive (true);
		ChanceLabel.SetActive (true);
	}

	public void DisableHighScoreStar(){
		print ("Disable star");
		HighScoreStar.SetActive (false);
	}

	public void EnableHighScoreStar(){
		print ("Enable star");
		HighScoreStar.SetActive (true);
	}


	public void SlideInQuitScreen(){
//		if (firstTimeQuitScreen) {
//			QuitScreen.GetComponent<Animator> ().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("Animations/QuitPanel", typeof(RuntimeAnimatorController)));
//			firstTimeQuitScreen = false;
//		}

		QuitScreen.GetComponent<Animator>().SetBool ("SlideInMenu", true);
		//QuitScreenObj.GetComponent<Animator>().SetBool ("SlideInMenu", true);
	}

	public void SlideOutQuitScreen(){

		QuitScreen.GetComponent<Animator>().SetBool ("SlideInMenu", false);
	}

	public void SlideInStartScreen(){

		StartScreenButtons.GetComponent<Animator>().SetBool ("SlideInMenu", true);
	}

	public void SlideOutStartScreen(){

		StartScreenButtons.GetComponent<Animator>().SetBool ("SlideInMenu", false);
	}

	public void SlideInChanceScreen(){
		ChanceScreen.GetComponent<Animator>().SetBool ("SlideInMenu", true);
	}

	public void SlideOutChanceScreen(){

		ChanceScreen.GetComponent<Animator>().SetBool ("SlideInMenu", false);
	}

	public void SlideInAboutScreen(){
		//Instantiate (AboutScreen, AboutScreen.transform.position, Quaternion.identity, Canvas.transform);
		//AboutScreen.GetComponent<Animator>().SetBool ("SlideInMenu", true);
		AboutScreen.GetComponent<Animator>().Play("SlideInMenu");
		//AboutScreen.GetComponent<Animation> ().Play ("SlideInMenu");
	}

	public void SlideOutAboutScreen(){

		//AboutScreen.GetComponent<Animator>().SetBool ("SlideInMenu", false);
		AboutScreen.GetComponent<Animator>().Play("SlideOutMenu");

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



	//BONUS BUTTONS
	void SlideInBonusButtons(){
		BonusButtons.GetComponent<Animator>().SetBool ("SlideInBonusButtons", true);
	}

	void SlideOutBonusButtons(){
		BonusButtons.GetComponent<Animator>().SetBool ("SlideInBonusButtons", false);
	}

	public void ShowCheckpointButton(){
		CheckpointButton.SetActive (true);
		if (IsCheckpointActive) {
			CheckpointButton.GetComponent<Image> ().sprite = checkpointOn;
			CheckpointButton.GetComponentInChildren<Text> ().text = "+";
			CheckpointButton.GetComponentInChildren<Text> ().fontSize = 80;
		} else {
			CheckpointButton.GetComponent<Image> ().sprite = checkpointOff;
		}
	}

	public void HideCheckpointButton(){
		CheckpointButton.SetActive (false);
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
			CheckpointButton.GetComponentInChildren<Text> ().text = currentCheckpointScore.ToString ();
			CheckpointButton.GetComponentInChildren<Text> ().fontSize = 40;

			IsCheckpointActive = false;

			ShowCheckpointButton ();
		}
	}
		
	void ResetCheckpointValues(){
		checkpoint = false;
		GameManager.nextCheckpoint = currentCheckpointScore + GameManager.checkpointRepeat;
		print ("next checkpoint: " + GameManager.nextCheckpoint);
	}
		



	public void ShowSuperSpeedButton(){
		SuperSpeedButton.SetActive (true);
		if (IsSuperSpeedActive) {
			SuperSpeedButton.GetComponent<Image> ().sprite = superspeedOn;
			SuperSpeedButton.GetComponent<Button> ().interactable = true;
		} else {
			SuperSpeedButton.GetComponent<Image> ().sprite = superspeedOff;
			SuperSpeedButton.GetComponent<Button> ().interactable = false;
		}
	}

	public void HideSuperSpeedButton(){
		SuperSpeedButton.SetActive (false);
	}


	public void StartSuperSpeed(){
		if (IsSuperSpeedActive) {
			IsSuperSpeed = true;
			SuperSpeedTransparent.SetActive (true);
			SuperSpeedStartScore = GameManagerScript.score;
			print (Time.time + ": Super Speed started at obstacle: " + SuperSpeedStartScore);
			SuperSpeedButton.GetComponent<Image> ().sprite = superspeedOff;
			SlideOutBonusButtons();
			IsSuperSpeedActive = false;
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

		SlideInBonusButtons();
	}



	public void ShowSlowDownButton(){
		SlowDownButton.SetActive (true);
		if (IsSlowDownActive) {
			SlowDownButton.GetComponent<Image> ().sprite = slowdownOn;
			SlowDownButton.GetComponent<Button> ().interactable = true;
		} else {
			SlowDownButton.GetComponent<Image> ().sprite = slowdownOff;
			SlowDownButton.GetComponent<Button> ().interactable = false;
		}
	}

	public void HideSlowDownButton(){
		SlowDownButton.SetActive (false);
	}

	public void StartSlowDown(){
		if (IsSlowDownActive) {
			IsSlowDown = true;
			SuperSpeedTransparent.SetActive (true);
			SlowDownStartScore = GameManagerScript.score;
			print (Time.time + ": Slow DOwn started at obstacle: " + SlowDownStartScore);
			SlowDownButton.GetComponent<Image> ().sprite = slowdownOff;
			SlideOutBonusButtons();
			IsSlowDownActive = false;
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
		SlideInBonusButtons();

	}

	void ShowCoinsCounterButton(){
		CoinsCounter.SetActive (true);
	}

	void HideCoinsCounterButton(){
		CoinsCounter.SetActive (false);
	}

	public void UpdateCoinsCounter(){
		print ("Coin collected");
		CoinsCounter.transform.Find ("CoinsCounterText").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeCoins").ToString();
	}


	void CalculateLife (){
		if (PlayerPrefs.GetFloat("lifeCoins") < GameManager.lifecoinRedeem) {
			AnotherChance.transform.Find ("HeartButton").GetComponent<Image> ().sprite = LifeOff;
			AnotherChance.transform.Find ("LifeCoin glow").gameObject.SetActive (false);
			AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat("lifeCoins").ToString();
		} else {
			AnotherChance.transform.Find ("HeartButton").GetComponent<Image> ().sprite = LifeOn;
			AnotherChance.transform.Find ("LifeCoin glow").gameObject.SetActive (true);
			AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat("lifeCoins").ToString();
		}
	}

	public void AnotherLife(){
		print (PlayerPrefs.GetFloat("lifeCoins"));
		if (PlayerPrefs.GetFloat("lifeCoins") >= GameManager.lifecoinRedeem && !LifeTaken) {
			LifeTaken = true;
			EnableStartButtonGlow = true;
			PlayerPrefs.SetFloat ("lifeCoins", PlayerPrefs.GetFloat ("lifeCoins") - GameManager.lifecoinRedeem);
			if (PlayerPrefs.GetFloat("lifeCoins") < GameManager.lifecoinRedeem) {
				//AnotherChance.transform.FindChild ("LifeAvailable").GetComponent<Text> ().text = "";
				AnotherChance.transform.Find ("HeartButton").GetComponent<Image> ().sprite = LifeOff;
				AnotherChance.transform.Find ("LifeCoin glow").gameObject.SetActive (false);
				AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeCoins").ToString ();
			} else {
				AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeCoins").ToString ();
			}
			BackFromChance ();
		}
	}

	public void EarnCoins(){
		print ("Watch Ad");
		//AdManagerScript.ShowAdVideo ();
	}
		
	public void EarnCoinsFromHint(){
		print ("Watch Ad");
		//AdManagerScript.ShowAdVideo ();
		HideAllHints ();
		SlideOutStartScreen ();
		SlideInChanceScreen ();
	}

	void EnableHintScreen(){
		int currHintNum;
		float currHighScore = PlayerPrefs.GetFloat ("highscore");

		HintScreen.SetActive (true);
		hintSize = 6;
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

		if (GameManager.showAchievement) {
			currHintNum = 100;
			GameManager.showAchievement = false;
		}
			
		//string[] HintsAndAds = new string[] {"ads", "earncoins", "slowdown", "superspeed", "checkpoint", "lifecoin"};
		switch (currHintNum) {
		case 0:
			HintScreen.transform.Find ("Rate").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 1:
			HintScreen.transform.Find ("Earn Coins").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 2:
			HintScreen.transform.Find ("Hint Slowdown").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 3:
			HintScreen.transform.Find ("Hint Superspeed").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 4:
			HintScreen.transform.Find ("Hint Checkpoint").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 5:
			HintScreen.transform.Find ("Hint Lifecoins").gameObject.SetActive (true);
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			break;
		case 100:
			HintScreen.transform.Find ("Achievement").gameObject.SetActive (true);
			HintScreen.transform.Find ("Achievement").Find ("Score").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("achievement").ToString ();
			HintScreen.GetComponent<Animator> ().Play ("SlideInHint", -1, 0);
			PlayerPrefs.SetFloat ("lifeCoins", PlayerPrefs.GetFloat ("lifeCoins") + 10);
			AnotherChance.transform.Find ("CoinsCounter").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("lifeCoins").ToString ();

			break;
		}
	}

//	void DisableHintScreen(){
//		IsHintActive = false;
//	}

	void HideAllHints(){
		HintScreen.transform.Find ("Hint Lifecoins").gameObject.SetActive (false);
		HintScreen.transform.Find ("Hint Slowdown").gameObject.SetActive (false);
		HintScreen.transform.Find ("Hint Superspeed").gameObject.SetActive (false);
		HintScreen.transform.Find ("Hint Checkpoint").gameObject.SetActive (false);
		HintScreen.transform.Find ("Rate").gameObject.SetActive (false);
		HintScreen.transform.Find ("Earn Coins").gameObject.SetActive (false);
		HintScreen.transform.Find ("Achievement").gameObject.SetActive (false);

		HintScreen.SetActive (false);
	}

	void EnableStartScreenAnimation(){
		StartScreenAnimator.SetActive (true);
	}

	void DisableStartScreenAnimation(){
		//StartScreenAnimator.SetActive (false);
		Destroy(StartScreenAnimator);
	}

	public void ShowAboutPage(){
		print ("About page");
		SlideOutStartScreen ();
		//GameManager.SetBallFallingFlag (false);
		SlideInAboutScreen ();
		aboutScreen = true;
	}

	void ResetBackgroundColor (){
		bgColorChanged = false;
		if (bgDark) {
			BackgroundImage.GetComponent<Animator> ().Play ("backgroundColorLight");
			bgDark = false;
		}
	}
}
