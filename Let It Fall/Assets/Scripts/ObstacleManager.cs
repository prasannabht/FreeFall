using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ObstacleManager : MonoBehaviour {

	public GameObject LifeCoin;
	GameManager GameManagerScript;

	public Obstacle[] obstacles;
	Obstacle[] sortedObstacles;
	Obstacle obstacle;


	float shiftHeight = 10.1f;
	float level1Distance = 6.0f;
	float level2Distance = 5.0f;
	float level3Distance = 4.0f;

	[HideInInspector]
	public float obstacleCount = 0;

	float currentDistance;
	bool firstObstacle = true;
	GameObject currentObstacle;
	List<string> obstacleList = new List<string> ();
	bool fadeOutAndDestroyObstacles = false;
	GameObject[] obstaclesToDestroy;
	float alphaLevel = 1;
	int randomNum;
	float nextLifeCoin;

	float checkpointDistance, checkpointObstacleCount;

	public void SaveCheckpointDetails(){
		print ("Saving checkpoint details");
		checkpointDistance = currentDistance;
		checkpointObstacleCount = obstacleCount;
	}

	public void ResetToCheckpoint(){
		print ("Distance: " + checkpointDistance);
		print ("Count: " + checkpointObstacleCount);
		currentDistance = checkpointDistance;
		obstacleCount = checkpointObstacleCount;
		firstObstacle = true;
		alphaLevel = 1;

		fadeOutAndDestroyObstacles = true;
		obstaclesToDestroy = GameObject.FindGameObjectsWithTag ("obstacle");
		foreach (GameObject obst in obstaclesToDestroy) {
			foreach (Collider2D col in obst.GetComponentsInChildren<Collider2D>()) {
				col.enabled = false;
			}
			obst.tag = "destroyedObstacle";
		}

	}

	public void ResetObstacles(){
		currentDistance = level1Distance;
		nextLifeCoin = GameManager.lifecoinInitScore;
		firstObstacle = true;
		obstacleCount = 0;
		alphaLevel = 1;
		fadeOutAndDestroyObstacles = true;
		obstaclesToDestroy = GameObject.FindGameObjectsWithTag ("obstacle");
		foreach (GameObject obst in obstaclesToDestroy) {
			//foreach (PolygonCollider2D col in obst.GetComponentsInChildren<PolygonCollider2D>()) {
			foreach (Collider2D col in obst.GetComponentsInChildren<Collider2D>()) {
				col.enabled = false;
			}
			obst.tag = "destroyedObstacle";
		}
//		foreach (GameObject obst in obstaclesToDestroy) {
//			Destroy (obst);
//		}
	}

	void Awake(){
		GameManagerScript = FindObjectOfType<GameManager>();

		sortedObstacles = obstacles.OrderBy(c => c.priority).ToArray();
	}

	void Start () {
		currentDistance = level1Distance;
		nextLifeCoin = GameManager.lifecoinInitScore;
	}
	
	void Update () {
		if (firstObstacle) {
			firstObstacle = false;

			//randomNum = Random.Range(0,4);
			obstacle = sortedObstacles [0];
			currentObstacle = Instantiate (obstacle.obstacleObj, new Vector2 (obstacle.xPos, GameManager.bottomY - 1), Quaternion.identity);
			currentObstacle.tag = "obstacle";
			obstacleList.Add (currentObstacle.name);
			if (!currentObstacle.name.Contains ("Fake"))
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

			//Randomly generate obstacles based on priority
			if (obstacleCount < 5)
				randomNum = Random.Range (0, 4);
			else if (obstacleCount < 10)
				randomNum = Random.Range (0, 6);
			else if (obstacleCount < 15)
				randomNum = Random.Range (0, 8);
			else if (obstacleCount < 20)
				randomNum = Random.Range (0, 12);
			else if (obstacleCount < 30)
				randomNum = Random.Range (0, 14);
			else if (obstacleCount < 40)
				randomNum = Random.Range (0, 15);
			else if (obstacleCount < 50)
				randomNum = Random.Range (0, 17);
			else if (obstacleCount < 60)
				randomNum = Random.Range (0, 21);
			else if (obstacleCount < 70)
				randomNum = Random.Range (0, 23);
			else if (obstacleCount < 80)
				randomNum = Random.Range (0, 25);
			else if (obstacleCount < 90)
				randomNum = Random.Range (0, 27);
			else if (obstacleCount < 100)
				randomNum = Random.Range (0, 29);
			else
				randomNum = Random.Range (0, 31);

			//randomNum = Random.Range(0,obstacles.Length);
			if (GameManagerScript.Testing) {
				print("Generate obstacle: " + Mathf.RoundToInt (obstacleCount % sortedObstacles.Length));
				obstacle = sortedObstacles [Mathf.RoundToInt (obstacleCount % sortedObstacles.Length)];
			} else {
				obstacle = sortedObstacles [randomNum];
			}
			currentObstacle = Instantiate (obstacle.obstacleObj, new Vector2 (obstacle.xPos, currentObstacle.transform.position.y - currentDistance), Quaternion.identity);
			currentObstacle.tag = "obstacle";

			if (obstacleList.Contains (currentObstacle.name) == false) {
				obstacleList.Add (currentObstacle.name);
				if (!currentObstacle.name.Contains ("Fake"))
					displayInstructions (currentObstacle);
			} else if (obstacleList.Contains (currentObstacle.name + "_1") == false) {
				obstacleList.Add (currentObstacle.name + "_1");
				if (!currentObstacle.name.Contains ("Fake"))
					displayInstructions (currentObstacle);
			}

			if (obstacleCount == nextLifeCoin) {
				nextLifeCoin = nextLifeCoin + GameManager.lifecoinRepeat;

				Instantiate (LifeCoin, new Vector3(Random.Range(GameManager.leftX, GameManager.rightX), currentObstacle.transform.position.y - 2, 0.1f), Quaternion.identity);

			}

			++obstacleCount;
		}

		if (fadeOutAndDestroyObstacles) {
			foreach (GameObject obst in obstaclesToDestroy) {
				
				if (alphaLevel > 0)
					alphaLevel -= Time.deltaTime * 2.5f;
				if (alphaLevel < 0)
					alphaLevel = 0;
				foreach (SpriteRenderer mySprite in obst.GetComponentsInChildren<SpriteRenderer>()) {
					mySprite.color = new Color (1f, 1f, 1f, alphaLevel);
				}
				//obst.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);

				if (alphaLevel == 0) {
					fadeOutAndDestroyObstacles = false;
					Destroy (obst);
				}
			}
		}

//		if (FindObjectOfType<UIManager> ().IsSuperSpeed) {
//			DisableColliders ();
//		}
	}

	void displayInstructions(GameObject myObstacle){
		//print (myObstacle.name);
		myObstacle.transform.root.FindChild ("Instruction").gameObject.SetActive (true);
	}

	public void DisableColliders(){
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("obstacle");
		foreach (GameObject obst in obstacles) {
			foreach (Collider2D col in obst.GetComponentsInChildren<Collider2D>()) {
				col.enabled = false;
				print ("Colliders disabled for: " + col.name);
			}
		}
	}

	void EnableColliders(){
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("obstacle");
		foreach (GameObject obst in obstacles) {
			foreach (Collider2D col in obst.GetComponentsInChildren<Collider2D>()) {
				col.enabled = true;
			}
		}
	}
		
}
