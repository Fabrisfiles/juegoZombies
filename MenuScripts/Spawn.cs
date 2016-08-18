using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public GameObject Zombie1;
	public GameObject Zombie2;

	// Use this for initialization
	void Start () {	
		Zombie1.SetActive (true);
		Zombie2.SetActive (false);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Zombie1") {			
			Zombie1.SetActive (false);
			Zombie2.transform.position = new Vector3 (-6.14f, 0.0f, 1.3f);
			Zombie2.SetActive (true);
		}
		if (other.gameObject.tag == "Zombie2") {
			Zombie1.transform.position = new Vector3 (6.14f, 0.0f, 1.3f);
			Zombie1.SetActive (true);
			Zombie2.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}