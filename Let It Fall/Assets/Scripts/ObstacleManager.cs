using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	public Obstacle[] obstacles;

	float shiftHeight = 10.1f;
	float startingPositionY = -6.0f;
	float level1Distance = 6.0f;
	float level2Distance = 5.0f;
	float level3Distance = 4.0f;

	float currentDistance;
	bool firstObstacle = true;
	GameObject currentObstacle;

	int randomNum;

	void Awake(){

	}

	void Start () {
		currentDistance = level1Distance;
		print ("Len: " + obstacles.Length);
	}
	
	void Update () {
		if (firstObstacle) {
			firstObstacle = false;

			randomNum = Random.Range(1,obstacles.Length + 1);
			print (randomNum);
			//currentObstacle = Instantiate (rotater, new Vector2 (rotaterLeftX, startingPositionY), Quaternion.identity);
			//obstacleList.Add (currentObstacle.name);
			//displayInstructions (currentObstacle);
			//++obstacleCount;
		}

		//if (currentObstacle.transform.position.y - currentDistance > Camera.main.transform.position.y - shiftHeight / 2) {
		//	randomNum = Random.Range(1,obstacles.Length);
		//	print (randomNum);
		//}
	}
}
