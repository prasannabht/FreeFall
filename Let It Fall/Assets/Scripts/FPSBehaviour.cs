using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSBehaviour : MonoBehaviour {

	public Text fpsText;

	private int FramesPerSec;
	private float frequency = 1.0f;
	private string fps;

	// Use this for initialization
	void Start () {
		StartCoroutine(FPS());
	}

	private IEnumerator FPS() {
		for(;;){
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;

			fps = string.Format("FPS: {0}" , Mathf.RoundToInt(frameCount / timeSpan));
			fpsText.text = fps;
		}
	}


}
