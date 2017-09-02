using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBehaviour : MonoBehaviour {

	bool isHomeClicked = false;
	float alphaLevel = 1f;

	void Update(){
		if (isHomeClicked) {
			if (alphaLevel > 0.0f) {
				alphaLevel -= Time.deltaTime * 5;
				foreach (SpriteRenderer mySprite in transform.parent.GetComponentsInChildren<SpriteRenderer>()) {
					mySprite.color = new Color (1f, 1f, 1f, alphaLevel);
				}
			}

			if (alphaLevel <= 0f) {
				//transform.root.FindChild ("Instruction").gameObject.SetActive(false);
				isHomeClicked = false;
				Application.LoadLevel ("Level 1");
			}
		}
	}

	void OnMouseDown () {
		Time.timeScale = 1;
		//Application.LoadLevel ("Level 1");
		isHomeClicked = true;
	}
}
