using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCoinBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		GameManager.lifecoinCollected += 1;
		//float lifecoinsCollected = PlayerPrefs.GetFloat("lifeCoins");
		PlayerPrefs.SetFloat ("lifeCoins", GameManager.lifecoinCollected);
		//print ("Coins collected: " + GameManager.lifecoinCollected);

		Destroy (gameObject);
	}
}
