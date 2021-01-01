using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCoinBehaviour : MonoBehaviour {
	float initSize, minSize;
	float speed = 0.5f;

	private bool waited = false;
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

			if (waited) {
				Destroy (gameObject);
			}
		}
	}

	void OnMouseDown () {
		FindObjectOfType<AudioManager>().Play("Coin");
		float coinCount = PlayerPrefs.GetFloat ("lifeCoins");
		coinCount += 1;
		//GameManager.lifecoinCollected += 1;
		//float lifecoinsCollected = PlayerPrefs.GetFloat("lifeCoins");
		PlayerPrefs.SetFloat ("lifeCoins", coinCount);
		//print ("Coins collected: " + GameManager.lifecoinCollected);
		UIManager.FindObjectOfType<UIManager>().UpdateCoinsCounter();
		gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0);
		transform.Find ("CoinPlusOne").gameObject.SetActive (true);
		print (waited);
		if (!waited) {
			StartCoroutine (waitForMe ());
		}

	}

	private IEnumerator waitForMe(){
		yield return new WaitForSeconds(0.5f);
		waited = true;
	}
}
