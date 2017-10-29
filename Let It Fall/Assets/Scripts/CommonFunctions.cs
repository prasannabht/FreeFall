using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunctions : MonoBehaviour {

	//Find next angle where obstacle should rotate to
	public static int FindNextStop(float initAngle, float currAngle){
		int floorVal = Mathf.FloorToInt(currAngle / 90) * 90;

		if (initAngle == 0) {
			if (180f > currAngle && currAngle > 90f)
				initAngle = 180;
			else if (-90f > currAngle && currAngle > -180f)
				initAngle = -180;
		}
		if (currAngle > initAngle)
			floorVal = floorVal + 90;
//		print ("Init Ang: " + initAngle + "  Curr Ang: " + currAngle);
//		print ("Next Stop: " + floorVal);
		return floorVal;
	}

	//get angle based on positon of click
	public static float GetAngle(Vector3 pos){
		return Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;
	}

}
