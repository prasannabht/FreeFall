using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour {
	//Object references for buttons
	public GameObject StartButton;
	public GameObject SoundButton;
	public GameObject ShareButton;
	public GameObject PauseButton;
	public GameObject ScoreText;
	public GameObject StartBackground;
	public GameObject LineShadow;

	public GameObject CurrentScore;
	public GameObject HighestScore;

	static bool StartScreenTransition = false;
	bool GameOverTransition = false;
	float transitionSpeed = 15;
	float alphaLevel = 1;
	Vector3 screen;

	Vector3 StartButtonInitPos;
	Vector3 SoundButtonInitPos;
	Vector3 ShareButtonInitPos;
	Vector3 LineShadowInitPos;
	Vector3 CurrentScoreInitPos;
	Vector3 HighestScoreInitPos;

	public void fnStartGameButton () {
		print ("start game");
		StartScreenTransition = true;
	}

	public void fnPauseGameButton(){
		print ("pause game");
	}

	public void fnSoundButton(){
		print ("sound");
	}

	public void fnShareButton(){
		print ("share");
	}

	void StartGame(){
		GameManager.SetBallFallingFlag (true);
	}

	public void GameOverScreen(){
		GameOverTransition = true;
	}

	void ResetStartScreenButtons(){
		StartButton.SetActive(true);
		SoundButton.SetActive(true);
		ShareButton.SetActive(true);
		LineShadow.SetActive (true);
		CurrentScore.SetActive (true);
		HighestScore.SetActive (true);

		StartButton.transform.position = StartButtonInitPos;
		SoundButton.transform.position = SoundButtonInitPos;
		ShareButton.transform.position = ShareButtonInitPos;
		LineShadow.transform.position = LineShadowInitPos;
		CurrentScore.transform.position = CurrentScoreInitPos;
		HighestScore.transform.position = HighestScoreInitPos;

		StartButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		SoundButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		ShareButton.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		StartBackground.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		LineShadow.GetComponent<Image>().color = new Color (1, 1, 1, 1);
		CurrentScore.transform.Translate (0, Time.deltaTime * transitionSpeed, 1);
		HighestScore.transform.Translate (0, Time.deltaTime * transitionSpeed, 1);

		PauseButton.GetComponent<Image>().color = new Color (1, 1, 1, 0);
		ScoreText.GetComponent<Text>().color = new Color (1, 1, 1, 0);
	}

	void Start(){
		StartButtonInitPos = StartButton.transform.position;
		SoundButtonInitPos = SoundButton.transform.position;
		ShareButtonInitPos = ShareButton.transform.position;
		LineShadowInitPos = LineShadow.transform.position;
		CurrentScoreInitPos = ShareButton.transform.position;
		HighestScoreInitPos = LineShadow.transform.position;

		PauseButton.GetComponent<Image>().color = new Color (1f, 1f, 1f, 1 - alphaLevel);
		ScoreText.GetComponent<Text>().color = new Color (1f, 1f, 1f, 1 - alphaLevel);
	}

	void Update(){
		if (StartScreenTransition){
			StartButton.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
			SoundButton.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
			ShareButton.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
			LineShadow.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);

			CurrentScore.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);
			HighestScore.transform.Translate (0, Time.deltaTime * transitionSpeed, 0);

			if (alphaLevel > 0.0f)
				alphaLevel -= Time.deltaTime * 2.5f;

			StartButton.GetComponent<Image>().color = new Color (1f, 1f, 1f, alphaLevel);
			SoundButton.GetComponent<Image>().color = new Color (1f, 1f, 1f, alphaLevel);
			ShareButton.GetComponent<Image>().color = new Color (1f, 1f, 1f, alphaLevel);
			StartBackground.GetComponent<Image>().color = new Color (1f, 1f, 1f, alphaLevel);
			LineShadow.GetComponent<Image>().color = new Color (1f, 1f, 1f, alphaLevel);

			CurrentScore.GetComponent<Text>().color = new Color (1f, 1f, 1f, alphaLevel);
			HighestScore.GetComponent<Text>().color = new Color (1f, 1f, 1f, alphaLevel);

			PauseButton.GetComponent<Image>().color = new Color (1f, 1f, 1f, 1 - alphaLevel);
			ScoreText.GetComponent<Text>().color = new Color (1f, 1f, 1f, 1 - alphaLevel);

			screen = Camera.main.WorldToViewportPoint (StartButton.transform.position);
			if(screen.y > 0.75f){
				StartButton.SetActive(false);
				SoundButton.SetActive(false);
				ShareButton.SetActive(false);
				StartBackground.SetActive (false);
				LineShadow.SetActive (false);
				CurrentScore.SetActive (false);
				HighestScore.SetActive (false);

				StartScreenTransition = false;

				StartGame ();
			}


		}

		if (GameOverTransition) {
			ResetStartScreenButtons ();
			GameOverTransition = false;
		}
	}
}
