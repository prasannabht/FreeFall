using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRotaterBehaviour : MonoBehaviour {

	float initAng;
	float ang;

	[HideInInspector]
	public bool isRopeTouched = false;
	// Use this for initialization
	void Start () {
		initAng = transform.rotation.eulerAngles.z;
		if (initAng > 180f) {
			initAng = initAng - 360;
		}

	//	print (initAng);
	}
	
	// Update is called once per frame
	void Update () {
		if (isRopeTouched) {
			if (initAng > 0) {
				if (initAng < 75.0f)
					initAng += Time.deltaTime*200f;
			} else {
				if (initAng > -75.0f)	
					initAng -= Time.deltaTime*200f;
			}
			transform.rotation = Quaternion.AngleAxis (initAng, Vector3.forward);
		}
	}
}
