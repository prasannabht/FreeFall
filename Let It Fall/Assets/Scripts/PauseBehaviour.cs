using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour {

	BallBehaviour ballScript;

	public GameObject PauseMenu;
	GameObject myPauseMenu;
	bool pauseClicked = false;
	bool soundPlayed = false;


	void Start(){
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}

	void OnMouseDown () {

		//Play click sound
		FindObjectOfType<AudioManager>().Play("Click");

		pauseGame ();

	}

	public void disableResumeObjects(){
		Destroy (myPauseMenu);
		pauseClicked = false;
		soundPlayed = false;
	}

	public void pauseGame(){
		if (pauseClicked == false) {
			if (ballScript.getStopMovementFlag () == false) {

//				//play sound
//				if (!soundPlayed) {
//					FindObjectOfType<AudioManager>().Play("StartTheme");
//					soundPlayed = true;
//				}

				myPauseMenu = Instantiate (PauseMenu, new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);

				Time.timeScale = 0;
				ballScript.setGamePausedFlag (true);
				pauseClicked = true;
			}
		}
	}
}
