using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBehaviour : MonoBehaviour {

	float alphaLevel;
	bool isRestartClicked = false;

	//SpriteRenderer currScore;
	//SpriteRenderer highScore;
	//bool isReadyToRestart = false;

	void Start(){
		alphaLevel = 1.0f;

		//currScore = transform.root.FindChild ("currentScore").GetComponent<SpriteRenderer>();
		//currScore = GameObject.Find("GameController").GetComponent<SpriteRenderer>();
		//highScore = GameObject.Find("highScore").GetComponent<SpriteRenderer>();
	}

	void Update(){
		if (isRestartClicked) {
			
			if (alphaLevel > 0.0f) {
				alphaLevel -= Time.deltaTime * 5;
				foreach (SpriteRenderer mySprite in transform.parent.GetComponentsInChildren<SpriteRenderer>()) {
					mySprite.color = new Color (1f, 1f, 1f, alphaLevel);
				}
			}
			//scoreObj.color = new Color (1f, 1f, 1f, alphaLevel);
			//highScore.color = new Color (1f, 1f, 1f, alphaLevel);

			if (alphaLevel <= 0f) {
				Application.LoadLevel ("Level 2");
			}
		}


	}

	void OnMouseDown(){
		isRestartClicked = true;
		Time.timeScale = 1;
	}
}
