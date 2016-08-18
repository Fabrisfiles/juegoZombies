using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	
	}

	void OnTriggerEnter(Collider Other){
		if (Other.gameObject.tag == "PlayTrigger") {
			SceneManager.LoadScene ("LoadScreen");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}