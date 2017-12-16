using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateManager : MonoBehaviour {

	public string rateURL = "market://details?id=com.ThirdState.SineLine";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RateGame(){
		print ("Rate game");
		Application.OpenURL (rateURL);
	}
}
