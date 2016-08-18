using UnityEngine;
using System.Collections;

public class FlashLightScript : MonoBehaviour {
	public Light Linterna;
	public bool On = false;

	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			On = !On;
			if (On)
				Linterna.enabled = true;
			else if (!On)
				Linterna.enabled = false;
		}	
	}
}
