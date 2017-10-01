using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonBehaviour : MonoBehaviour {

	public GameObject QuitMenu;

	bool started = false;
	float alphaLevel;
	Vector3 screen;

	Transform parentObj;

	bool soundPlayed = false;

	void Awake(){
		//PlayerPrefs.DeleteAll ();
		if (!PlayerPrefs.HasKey ("sound")) {
			print ("first time");
			PlayerPrefs.SetInt ("sound", 1);
		}
	}

	void Start () {

		alphaLevel = 1.0f;
		parentObj = transform.parent.parent;

		//play theme
		if (!soundPlayed) {
			//GetComponent<AudioSource> ().Play();
			FindObjectOfType<AudioManager>().Play("StartTheme");
			soundPlayed = true;
		}
	}

	void Update(){
		if (started == true) {
			if (alphaLevel > 0.0f)
				alphaLevel -= Time.deltaTime * 3;
			//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, alphaLevel);
			foreach (SpriteRenderer mySprite in parentObj.GetComponentsInChildren<SpriteRenderer>()) {
				mySprite.color = new Color (1f, 1f, 1f, alphaLevel);
			}

			//transform.Translate(0, Time.deltaTime*5, 0);
			parentObj.Translate(0, Time.deltaTime*7, 0);
			//screen = Camera.main.WorldToViewportPoint (transform.position);
			screen = Camera.main.WorldToViewportPoint (parentObj.position);

			if(screen.y > 0.75f){
				started = false;
				Destroy (gameObject);
				Application.LoadLevel ("Level 2");
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Instantiate (QuitMenu, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);
		}
	}

	void OnMouseDown () {
		if (started == false) {

			//Play click sound
			FindObjectOfType<AudioManager>().Play("Click");

			started = true;
		}
	}
}
