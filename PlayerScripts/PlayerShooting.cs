using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour
{
	public Text balastexto;
	public Text cargadorestexto;
	//Variables publicas
	public float power;
	public float RangoAtaque;
	public float shotDelay;
	public float reloadDelay;
	public int BalasPorCargador = 9;
	public int Cargadores;
	public float hitPoints;
	public bool isPaused;
	public bool objetivoCumplido;

	public int enemigosRestantes;

	public bool key1Destroy;
	public bool key2Destroy;
	public bool key3Destroy;

	public AudioClip shotSound;
	public AudioClip sinBalasSound;
	public AudioClip reloadSound;
	public GameObject bulletHole;
	public GameObject MetalParticle;
	public GameObject WoodParticle;
	public GameObject ConcreteParticle;
	public GameObject SangreParticle;
	public GameObject DefaultParticle;
	public GameObject ExitTrigger;
	public int cicles;

	//Variables privadas
	private int bullets;
	//private int cicles;
	private float nextShot;
	private GameObject Particula;
	private bool isFlesh = false;

	public GameObject tachado1;
	public GameObject tachado2;
	public GameObject objetivo3;

	public bool objetivo1Cumplido;
	public bool objetivo2Cumplido;

	void Start () {
		isPaused = false;
		Time.timeScale = 1;
		bullets = BalasPorCargador;
		cicles = Cargadores;
		ExitTrigger.SetActive (false);
		objetivoCumplido = false;
		key1Destroy = false;
		key2Destroy = false;
		key3Destroy = false;
		enemigosRestantes = 28;

		objetivo1Cumplido = false;
		objetivo2Cumplido = false;
		tachado1.SetActive (false);
		tachado2.SetActive (false);
		objetivo3.SetActive (false);
	}

	void Shot(){
		if (Time.time > nextShot){
			nextShot = Time.time + shotDelay;
			if (BalasPorCargador > 0){
				GetComponent<AudioSource>().clip = shotSound;
				GetComponent<AudioSource>().Play();
				BalasPorCargador--;
				balastexto.text = BalasPorCargador.ToString ();
				RaycastHit hit;
				Vector3 direccion = transform.TransformDirection (Vector3.forward);
				if (Physics.Raycast (transform.position, direccion, out hit, RangoAtaque)){
					Vector3 posicion = hit.point;
					Quaternion rotacion = Quaternion.FromToRotation (Vector3.up ,hit.normal);
					hit.collider.SendMessageUpwards ("ApplyDamage", hitPoints, SendMessageOptions.DontRequireReceiver);
					switch(hit.collider.tag){
					case "metal":
						Particula = MetalParticle;
						Instantiate (Particula, posicion, rotacion);
						isFlesh = false;
						break;
					case "wood":
						Particula = WoodParticle;
						Instantiate (Particula, posicion, rotacion);
						break;		
					case "concrete":
						Particula = ConcreteParticle;
						Instantiate (Particula, posicion, rotacion);
						break;	
					case "Enemy":
						Particula = SangreParticle;
						Instantiate (Particula, posicion, rotacion);
						isFlesh = true;
						break;							
					default:
						Particula = DefaultParticle;
						Instantiate (Particula, posicion, rotacion);
						break;
					}
					if (isFlesh == false) {
						GameObject hole; 
						hole = Instantiate (bulletHole, posicion, rotacion) as GameObject;
						hole.transform.parent = hit.collider.transform;

						if (hit.rigidbody) {
							hit.rigidbody.AddForceAtPosition (direccion * power, posicion);

						}
					}
				}


			}
			else{
				GetComponent<AudioSource>().clip = sinBalasSound;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	void Reloading(){
		//if (cicles > 0  & BalasPorCargador != bullets){
		if (BalasPorCargador == 0 && cicles > 0 ){
			GetComponent<AudioSource>().clip = reloadSound;
			GetComponent<AudioSource>().Play();
			nextShot = reloadDelay + Time.time;
			cicles--;
			Cargadores = cicles;
			cargadorestexto.text = Cargadores.ToString ();
			BalasPorCargador = bullets;
			balastexto.text = BalasPorCargador.ToString ();
		}
		else{
			return;
		}
	}

	void Keys(){
		GameObject Key1 = GameObject.FindGameObjectWithTag ("Llave1");
		GameObject Key2 = GameObject.FindGameObjectWithTag ("Llave2");
		GameObject Key3 = GameObject.FindGameObjectWithTag ("Llave3");

		if (Key1 == null){
			key1Destroy = true;
		}
		if (Key2 == null){
			key2Destroy = true;
		}
		if (Key3 == null){
			key3Destroy = true;
		}
	}

	void ObjetivoCumplido(){
		if ((keyStatusCheck()) && (enemigosRestantes == 0)){
			objetivoCumplido = true;
			ExitTrigger.SetActive (true);
		}		
	}

	public bool keyStatusCheck(){
		return key1Destroy && key2Destroy && key3Destroy;
	}


	// -------------------------------------------------------------------------


	public void Objetivo1Cumplido(){
		if (keyStatusCheck()) {
			tachado1.SetActive (true);
			objetivo1Cumplido = true;
		}
	}

	public void Objetivo2Cumplido(){
		if (enemigosRestantes == 0) {
			tachado2.SetActive (true);
			objetivo2Cumplido = true;
		}
	}

	public void MostrarObjetivo3(){
		if ((objetivo1Cumplido == true) && (objetivo2Cumplido == true)) {
			objetivo3.SetActive (true);
			ExitTrigger.SetActive (true);
		}
	}


	// ------------------------------------------------------------------------



	// Update is called once per frame
	void Update (){
		Cargadores = cicles;
		cargadorestexto.text = Cargadores.ToString ();
		if ((Input.GetButtonDown ("Fire1")) && (isPaused == false)){
			Shot ();
		}
		if (Input.GetKeyDown (KeyCode.R)||Input.GetButtonDown ("Fire2")){
			Reloading();
		}
		if (Input.GetButtonDown ("Cancel")) {
			isPaused = true;
		}
		if (Input.GetKeyDown(KeyCode.Tab)){
			isPaused = true;
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			isPaused = false;
		}

		Keys ();
		ObjetivoCumplido ();
		Objetivo1Cumplido ();
		Objetivo2Cumplido ();
		MostrarObjetivo3 ();
	}
}

