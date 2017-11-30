using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCoinBehaviour : MonoBehaviour {
	float initSize, minSize;
	float speed = 0.5f;
	// Use this for initialization
	void Start () {
		initSize = transform.localScale.x;
		minSize = initSize - 0.2f;

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.IsBallFalling () || !UIManager.GameStarted ()) {
			float currSize = Mathf.PingPong (Time.time * speed, initSize - minSize) + minSize;
			transform.localScale = new Vector3 (currSize, currSize, currSize);
		}
	}

	void OnMouseDown () {
		GameManager.lifecoinCollected += 1;
		//float lifecoinsCollected = PlayerPrefs.GetFloat("lifeCoins");
		PlayerPrefs.SetFloat ("lifeCoins", GameManager.lifecoinCollected);
		//print ("Coins collected: " + GameManager.lifecoinCollected);

		Destroy (gameObject);
	}
}
