using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

	//public ScoreBehaviour score;
	public BallBehaviour ballScript;

	public GameObject rotater;
	public GameObject sliderLeft;
	public GameObject sliderRight;
	public GameObject doorLeft;
	public GameObject doorRight;
	//public GameObject stopperLeft;
	public GameObject pullyLeft;
	public GameObject pullyRight;
	public GameObject bomb2;
	public GameObject bomb3;

	public float obstacleCount = 0;

	GameObject currentObstacle;
	GameObject firstObstacleObj;
	GameObject obstacle;

	float shiftHeight = 10.1f;
	float startingPositionY = -6.0f;
	float level1Distance = 6.0f;
	float level2Distance = 5.0f;
	float level3Distance = 4.0f;

	bool firstObstacle = true;
	public bool slowMo = false;

	float minY;

	//int currObstacleOccurance=0;
	List<string> obstacleList = new List<string> ();

	//float rotaterLeftX = -0.89f;
	//float rotateRightX = 0.89f;
	float rotaterLeftX = -1.2f;
	float rotateRightX = 1.2f;
	float sliderLeftX = -1.6f;
	float sliderRightX = 1.6f;
	float doorLeftX = 0.0f;
	float doorRightX = 0.0f;
	//float stopperLeftX = -1.9f; 
	float pullyX = 0.0f;
	float bombX = 0f;

	float currentDistance;
	float currentSide;
	float obstacleX;

	int sideNum;
	int typeNum;


	void Start(){

		currentDistance = level1Distance;
		slowMo = false;

		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;

	}


	void Update () {

		if (firstObstacle) {
			firstObstacle = false;
			currentObstacle = Instantiate (rotater, new Vector2 (rotaterLeftX, startingPositionY), Quaternion.identity);
			obstacleList.Add (currentObstacle.name);
			displayInstructions (currentObstacle);
			++obstacleCount;
		}

		if (currentObstacle.transform.position.y - currentDistance > Camera.main.transform.position.y - shiftHeight/2) {

			slowMo = false;

			//determine side of object placement and type of object
			sideNum = Random.Range(1,3);


			//typeNum = Random.Range (1, 5);
			//Load initial 3 types of Obstacles for first 15 counts
				
			if (obstacleCount < 10)
				typeNum = Random.Range (1, 3);
			else if (obstacleCount < 15)
				typeNum = Random.Range (1, 4);
			else if (obstacleCount < 20)
				typeNum = Random.Range (1, 5);
			else if (obstacleCount < 25)
				typeNum = Random.Range (1, 6);
			else
				typeNum = Random.Range (1, 7);


			string side = "Left";
			if(sideNum == 1)
				side = "Left";
			else if(sideNum == 2)
				side = "Right";



			switch (typeNum) {
			//typeNum 1 = slider
			case 1:
				if (side == "Left") {
					obstacle = sliderLeft;
					obstacleX = sliderLeftX;
				} else if (side == "Right") {
					obstacle = sliderRight;
					obstacleX = sliderRightX;
				}
				//slowMo = true;
				break;
			//typeNum 2 = Rotater
			case 2:
				if (side == "Left") {
					obstacle = rotater;
					obstacleX = rotaterLeftX;
				} else if (side == "Right") {
					obstacle = rotater;
					obstacleX = rotateRightX;
				}
				break;
			//typeNum 3 = Door
			case 3:
				if (side == "Left") {
					obstacle = doorLeft;
					obstacleX = doorLeftX;
				} else if (side == "Right") {
					obstacle = doorRight;
					obstacleX = doorRightX;
				}
				break;

			//typeNum 4 = Bomb2
			case 4:
				obstacle = bomb2;
				obstacleX = bombX;
				slowMo = true;
				break;
			
			//typeNum 5 = Bomb3
			case 5:
				obstacle = bomb3;
				obstacleX = bombX;
				slowMo = true;
				break;

			//typeNum 6 = Pully
			case 6:
				if (side == "Left") {
					obstacle = pullyLeft;
					obstacleX = pullyX;
				} else if (side == "Right") {
					obstacle = pullyRight;
					obstacleX = pullyX;
				}
				slowMo = true;
				break;
			}
				
			//determine level of game, as level increases, distance between objects decreases.
			//initial deployment with level1Distance
			currentDistance = level1Distance;

			if (obstacleCount > 5) {
				currentDistance = level2Distance;
			} else if (obstacleCount > 10) {
				currentDistance = level3Distance;
			}
				
			currentObstacle = Instantiate (obstacle, new Vector2 (obstacleX, currentObstacle.transform.position.y - currentDistance), Quaternion.identity);

			if (obstacleList.Contains (currentObstacle.name) == false) {

				//slow down
				if (slowMo)
					ballScript.slowDown = true;

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
