using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShareManager : MonoBehaviour {

	public GameObject CanvasShare;

	private bool isProcessing = false;
	private bool isFocus = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActivateShareCanvas(){
		if (!isProcessing) {
			CanvasShare.SetActive (true);
			CanvasShare.transform.FindChild ("HighScore").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("highscore").ToString();
		}
	}

	public void ShareHighScore(){
		CanvasShare.transform.FindChild ("ShareButton").gameObject.SetActive (false);
		CanvasShare.transform.FindChild ("CancelButton").gameObject.SetActive (false);
		StartCoroutine (ShareScreenShot ());
	}
		
	public void CancelShare(){
		CanvasShare.SetActive(false);
	}

	IEnumerator ShareScreenShot(){
		isProcessing = true;

		yield return new WaitForEndOfFrame ();

		Application.CaptureScreenshot ("ShareScreenshot.png", 2);
		string ScreenshotPath = Path.Combine (Application.persistentDataPath, "ShareScreenshot.png");

		yield return new WaitForSecondsRealtime (0.3f);

		if (!Application.isEditor) {
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + ScreenshotPath);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic <string> ("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic <string> ("EXTRA_TEXT"), "Check out this awesome game! FREEFALL");
			intentObject.Call<AndroidJavaObject> ("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject, "Share your High Score");
			currentActivity.Call ("startActivity", chooser);

			yield return new WaitForSecondsRealtime (1);
		}

		yield return new WaitUntil (() => isFocus);

		CanvasShare.transform.FindChild ("ShareButton").gameObject.SetActive (true);
		CanvasShare.SetActive (false);
		isProcessing = false;
	}

	private void OnApplicationFocus(bool focus){
		isFocus = focus;
	}
}
