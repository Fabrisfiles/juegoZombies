using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MetaFinalScript : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			SceneManager.LoadScene ("Menu");
		}
	}
}
