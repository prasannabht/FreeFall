using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RandomTest : MonoBehaviour {
	public void GenerateRandon(){

		using (StreamWriter sw = new StreamWriter("D:\\Unity Works\\Random.txt")) {
			for (int i = 0; i < 500; i++) {
				int rn = Random.Range (0, 51) + Random.Range (0, 51);
				if (rn > 50)
					rn = 100 - rn;

//				int rn = Random.Range (0, 31);
				sw.WriteLine (rn);
			}
		}
		print ("done");

	}

}
