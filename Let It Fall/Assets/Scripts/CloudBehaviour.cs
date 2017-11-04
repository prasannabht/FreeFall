using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

	float startingPositionY = -6.0f;
	bool firstCloud = true;
	GameObject currentCloud;
	float cloudDistance = 14f;
	//float topYpos = 6f;
	float cloudSpeed;
	float alphaLevel = 0.1f;
	public Transform clouds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (firstCloud) {
			firstCloud = false;
			currentCloud = Instantiate (clouds.FindChild("Cloud 1").gameObject, new Vector3 (-3f, startingPositionY, 0.15f), Quaternion.identity);
			currentCloud.transform.parent = transform;
			currentCloud.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
		}

		cloudSpeed = GameManager.GetSpeed() / 3;
		if (GameManager.IsBallFalling()) {
			currentCloud.transform.Translate (0, Time.deltaTime * cloudSpeed, 0);
		}

		//if (currentCloud.transform.position.y - cloudDistance > Camera.main.transform.position.y - shiftHeight / 2) {
		if (currentCloud.transform.position.y - startingPositionY > cloudDistance) {

			Destroy (currentCloud);

			int sideNum = Random.Range (1, 5);
			int cloudNum = Random.Range (-6, 6);
			float cludPos = cloudNum / 2f;

			currentCloud = Instantiate (clouds.FindChild("Cloud " + sideNum).gameObject, new Vector3 (cludPos, currentCloud.transform.position.y - cloudDistance, 0.15f), Quaternion.identity);
			currentCloud.transform.parent = transform;
			currentCloud.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
		}


	}
}
