using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInLoadScreenBehaviour : MonoBehaviour {

	bool FadeIn = false;
	float alphaLevel = 0;
	// Use this for initialization
	void Start () {
		FadeIn = true;
		gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (FadeIn) {
			if (alphaLevel < 1) {
				alphaLevel += Time.deltaTime * 0.5f;
				gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, alphaLevel);
			} else {
				FadeIn = false;
			}
		}
	}
}
