using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScoreBehaviour : MonoBehaviour {

//	public Text scoreText;
//
//	public Text currentScoreText;
//	public Text highestScoreText;

	static float score;

	void Start () {
		score = 0;
	}

//	public static void updateScore (int scoreIncrement){
//		score += scoreIncrement;
//		scoreText.text = "" + score;
//	}

	public float getScore(){
		return score;
	}
//
//	public void setCurrentAndHighestScores(float currentScore, float highestScore){
//		//print (currentScore.ToString () + "\nScore");
//		currentScoreText.text = currentScore.ToString() + "\nScore";
//		highestScoreText.text = highestScore.ToString() + "\nHighest";
//	}
}
