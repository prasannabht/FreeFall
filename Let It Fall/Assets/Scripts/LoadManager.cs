using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour {

	public static void Load (string name){
		GameObject go = new GameObject ("LoadManager");
		LoadManager instance = go.AddComponent<LoadManager> ();
		instance.StartCoroutine (instance.InnerLoad (name));
	}

	IEnumerator InnerLoad (string name){
		Object.DontDestroyOnLoad (this.gameObject);
		Application.LoadLevel ("Loading Level");
		yield return null;
		print ("Loading");
		Application.LoadLevel (name);
		Destroy (this.gameObject);
	}
}
