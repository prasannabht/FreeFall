using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour {

	BallBehaviour ballScript;

	public GameObject PauseMenu;
	GameObject myPauseMenu;
	bool pauseClicked = false;



	void Start(){
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}

	void OnMouseDown () {

		pauseGame ();
//		if (pauseClicked == false) {
//			if (ballScript.getStopMovementFlag () == false) {
//				//foreach (Transform child in transform)
//				//	child.gameObject.SetActive (true);
//				myPauseMenu = Instantiate (PauseMenu, new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);
//
//				Time.timeScale = 0;
//				ballScript.setGamePausedFlag (true);
//				pauseClicked = true;
//				//gameObject.SetActive (false);
//			}
//		}
	}

	public void disableResumeObjects(){
		Destroy (myPauseMenu);
		pauseClicked = false;
		//gameObject.SetActive (true);
		//foreach (Transform child in transform)
		//	child.gameObject.SetActive (false);
	}

	public void pauseGame(){
		if (pauseClicked == false) {
			if (ballScript.getStopMovementFlag () == false) {
				//foreach (Transform child in transform)
				//	child.gameObject.SetActive (true);
				myPauseMenu = Instantiate (PauseMenu, new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);

				Time.timeScale = 0;
				ballScript.setGamePausedFlag (true);
				pauseClicked = true;
				//gameObject.SetActive (false);
			}
		}
	}
}
