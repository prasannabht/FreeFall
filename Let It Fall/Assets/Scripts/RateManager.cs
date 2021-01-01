using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateManager : MonoBehaviour {

	string rateURL = "market://details?id=com.PB.LetItFall";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RateGame(){
		print ("Rate game - " + rateURL);
		Application.OpenURL (rateURL);
	}
}
