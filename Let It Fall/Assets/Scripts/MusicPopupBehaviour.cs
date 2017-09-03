using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPopupBehaviour : MonoBehaviour {

	public GameObject QuitMenu;
	bool quitClicked = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		//if (quitClicked == false) {
		//Play click sound
		FindObjectOfType<AudioManager>().Play("Click");

		transform.parent.parent.FindChild ("Quit").gameObject.SetActive (true);
		transform.parent.gameObject.SetActive (false);
		//Instantiate (QuitMenu, new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);
		quitClicked = true;
		//}
	}
}
