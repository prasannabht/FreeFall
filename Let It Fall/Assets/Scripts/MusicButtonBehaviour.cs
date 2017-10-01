using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButtonBehaviour : MonoBehaviour {

	public Sprite musicOn;
	public Sprite musicOff;

	//AudioSource audio;

	// Use this for initialization
	void Start () {

		 if (PlayerPrefs.GetInt("sound")==1) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = musicOn;
		} else if(PlayerPrefs.GetInt("sound")==0){
			gameObject.GetComponent<SpriteRenderer> ().sprite = musicOff;
		}

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		
		if (PlayerPrefs.GetInt("sound")==1) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = musicOff;
			PlayerPrefs.SetInt ("sound", 0);
			AudioListener.volume = 0;
			//audio.mute = true;
		} else if(PlayerPrefs.GetInt("sound")==0){
			gameObject.GetComponent<SpriteRenderer> ().sprite = musicOn;
			PlayerPrefs.SetInt ("sound", 1);
			AudioListener.volume = 1;
			//audio.mute = false;
		}
	}
}