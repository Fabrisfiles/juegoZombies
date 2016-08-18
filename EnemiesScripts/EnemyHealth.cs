using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour{

	public GameObject PlayerHealth;
	public int health = 100;          
	public float sinkSpeed = 2.5f;
	bool isSinking;
	public GameObject Slider;

	public int scoreValue = 10;
	public int resta = 1;
	public Slider healthSlider;
	public AudioClip zombie;
	private Animator animator;
	CharacterController controller;
	Transform player;
	[SerializeField]
	float moveSpeed = 4.0f;
	[SerializeField]
	float gravity =2.0f;
	float yvelocity = 0.0f;
	public bool dead = false;

	GameObject Gun;
	GameObject ScoreText;

	PlayerShooting gunScript;
	ScoreManager scoreScript;

	void Awake(){
		animator = GetComponent<Animator> ();
	}

	void Start () {
		Slider.SetActive (true);
		GameObject playerGameobject = GameObject.FindGameObjectWithTag ("Player");
		player = playerGameobject.transform;
		controller = GetComponent<CharacterController> ();
		GetComponent<AudioSource> ().clip = zombie;
		GetComponent<AudioSource> ().Play ();
		Gun = GameObject.Find ("Gun");
		gunScript = Gun.GetComponent<PlayerShooting> ();
		ScoreText = GameObject.Find ("Scoretext");
		scoreScript = ScoreText.GetComponent<ScoreManager> ();
	}

	void Update () {
		if ((dead == false) && (health > 0)) {			
			Vector3 direction = player.position - transform.position;
			direction.Normalize ();
			Vector3 velocity = direction * moveSpeed;
			if (!controller.isGrounded) {
				yvelocity -= gravity;
			}
			velocity.y = yvelocity;
			direction.y = 0;
			transform.rotation = Quaternion.LookRotation (direction);
			controller.Move (velocity * Time.deltaTime);
		}
		if (health == 0) {			
			animator.SetBool ("isDead", true);
			dead = true;
			GetComponent <Seguir> ().enabled = false;
			GetComponent <NavMeshAgent> ().enabled = false;
			Slider.SetActive (false);

		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			animator.SetBool ("isPlayer", true);
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player"){
			animator.SetBool ("isPlayer", false);
		}
	}

	void EnemiesCount(){
		gunScript.enemigosRestantes -= resta;
	}

	void DestroyEnemy(){
		Destroy (gameObject);
	}

	void ScoreCount(){
		scoreScript.score += scoreValue;
	}

	public void Herida(){
		this.health = this.health - 20;
		healthSlider.value = health;
	}

	void ApplyDamage(){
		Herida ();
	}

}