using UnityEngine;
using System.Collections;

public class ZombieIntroScript : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetTrigger ("Intro");
	
	}
}
