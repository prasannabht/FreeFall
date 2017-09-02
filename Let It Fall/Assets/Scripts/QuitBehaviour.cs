using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBehaviour : MonoBehaviour {

	Transform parentObj;

	void OnMouseDown(){
		if (gameObject.name == "Yes") {
			Application.Quit ();
		} else if (gameObject.name == "No") {
			parentObj = transform.parent.parent;
			if (parentObj.name.Contains ("Start"))
				parentObj.FindChild ("StartComponents").gameObject.SetActive (true);
			else if(parentObj.name.Contains("GameOver"))
				parentObj.FindChild ("GameOverMenu").gameObject.SetActive (true);
			else if(parentObj.name.Contains("Pause")){
				parentObj.FindChild ("PauseMenu").gameObject.SetActive (true);
			}

			transform.parent.gameObject.SetActive (false);
		}
	}
}
