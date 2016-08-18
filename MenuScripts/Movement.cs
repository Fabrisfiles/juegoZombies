using UnityEngine;
using System.Collections;
 
 [RequireComponent (typeof(Rigidbody))]
 public class Movement : MonoBehaviour {

	void Start(){    
	}

	void Update(){
		GetComponent<Rigidbody> ().velocity = transform.forward;
	}
 }