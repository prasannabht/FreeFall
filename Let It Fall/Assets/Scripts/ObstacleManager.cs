using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	public Obstacle[] obstacles;
	Obstacle obstacle;

	float shiftHeight = 10.1f;
	float startingPositionY = -6.0f;
	float level1Distance = 6.0f;
	float level2Distance = 5.0f;
	float level3Distance = 4.0f;
	float obstacleCount = 0;

	float currentDistance;
	bool firstObstacle = true;
	GameObject currentObstacle;
	List<string> obstacleList = new List<string> ();

	int randomNum;

	void Awake(){

	}

	void Start () {
		currentDistance = level1Distance;
	}
	
	void Update () {
		if (firstObstacle) {
			firstObstacle = false;

			randomNum = Random.Range(0,obstacles.Length);
			obstacle = obstacles [randomNum];
			currentObstacle = Instantiate (obstacle.obstacleObj, new Vector2 (obstacle.xPos, startingPositionY), Quaternion.identity);
			obstacleList.Add (currentObstacle.name);
			displayInstructions (currentObstacle);
			++obstacleCount;
		}

		if (currentObstacle.transform.position.y - currentDistance > Camera.main.transform.position.y - shiftHeight / 2) {

			//determine level of game, as level increases, distance between objects decreases.
			if (obstacleCount > 5) {
				currentDistance = level2Distance;
			} else if (obstacleCount > 10) {
				currentDistance = level3Distance;
			}

			randomNum = Random.Range(0,obstacles.Length);
			obstacle = obstacles [randomNum];
			currentObstacle = Instantiate (obstacle.obstacleObj, new Vector2 (obstacle.xPos, currentObstacle.transform.position.y - currentDistance), Quaternion.identity);

			if (obstacleList.Contains (currentObstacle.name) == false) {
				obstacleList.Add (currentObstacle.name);
				displayInstructions (currentObstacle);
			} else if (obstacleList.Contains (currentObstacle.name + "_1") == false) {
				obstacleList.Add (currentObstacle.name + "_1");
				displayInstructions (currentObstacle);
			}

			++obstacleCount;
		}
	}

	void displayInstructions(GameObject myObstacle){
		//print (myObstacle.name);
		myObstacle.transform.root.FindChild ("Instruction").gameObject.SetActive (true);

	}
}
