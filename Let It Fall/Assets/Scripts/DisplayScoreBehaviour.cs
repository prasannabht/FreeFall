using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreBehaviour : MonoBehaviour {

	public Text currentScoreText;
	public Text highestScoreText;

	float currentScore, highestScore;
	public static float highScore;

	UpdateScoreBehaviour scoreScript;
	// Use this for initialization
	void Start () {
		scoreScript = GameObject.FindObjectOfType (typeof(UpdateScoreBehaviour)) as UpdateScoreBehaviour;

		currentScore = scoreScript.getScore ();
		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);

		if (currentScore > PlayerPrefs.GetFloat ("highscore")) {
			PlayerPrefs.SetFloat ("highscore", currentScore);
		}

		//print (currentScore.ToString () + "\nScore");
		currentScoreText.text = currentScore.ToString() + "\nScore";
		highestScoreText.text = highestScore.ToString() + "\nHighest";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setCurrentAndHighestScores(){
//		currentScore = scoreScript.getScore ();
//		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);
//
//		if (currentScore > PlayerPrefs.GetFloat ("highscore")) {
//			PlayerPrefs.SetFloat ("highscore", currentScore);
//		}
//
//		//print (currentScore.ToString () + "\nScore");
//		currentScoreText.text = currentScore.ToString() + "\nScore";
//		highestScoreText.text = highestScore.ToString() + "\nHighest";
	}
}
