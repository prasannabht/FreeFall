using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

	BallBehaviour ballScript;

	public GameObject pauseMenu;
	public GameObject retryMenu;
	public GameObject quitMenu;
	//public static bool pauseClicked = false;
	bool resumeClicked = false;

	float alphaLevel = 1.0f;

	RaycastHit2D hit;

	// Use this for initialization
	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}
	
	// Update is called once per frame
	void Update () {
		if (resumeClicked) {
//
			if (alphaLevel > 0.0f) {
				alphaLevel -= Time.deltaTime * 5;
				foreach (SpriteRenderer mySprite in transform.parent.GetComponentsInChildren<SpriteRenderer>()) {
					mySprite.color = new Color (1f, 1f, 1f, alphaLevel);
				}
			}

			if (alphaLevel <= 0f) {
//
//				PlayerPrefs.SetInt("isPaused", 0);
//				transform.parent.gameObject.SetActive (false);
				PlayerPrefs.SetInt("isMoving", 1);
				transform.parent.gameObject.SetActive (false);
				resumeClicked = false;
			}


		}
	}

	void OnMouseDown () {

		Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		hit = Physics2D.Raycast (worldPoint, Vector2.zero);
		print (hit.collider.name);

		switch (hit.collider.name) {
		//typeNum 1 = slider
		case "Pause":
			if (PlayerPrefs.GetInt("isMoving") == 1) {
				//if (ballScript.getStopMovementFlag () == false) {
				GameObject myPauseMenu = Instantiate (pauseMenu, new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);

				foreach (SpriteRenderer mySprite in pauseMenu.GetComponentsInChildren<SpriteRenderer>()) {
						mySprite.color = new Color (1f, 1f, 1f, 1f);
					}

					Time.timeScale = 0;
					//ballScript.setGamePausedFlag (true);
					PlayerPrefs.SetInt("isMoving", 0);
				//}
			}
			break;

		case "Resume":
			if (resumeClicked == false) {
				Time.timeScale = 1;
				resumeClicked = true;

			}
			break;
		}
	}

	public void setRetryMenu(){
		Instantiate (retryMenu, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);
		Time.timeScale = 0;
	}
}
