using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour{
	public Text HealthText;
	public int startingHealth = 100;                           
	public int currentHealth;                                  
	public Slider healthSlider;                                
	public Image damageImage;                                  
	public AudioClip deathClip;                               
	public float flashSpeed = 5f;                              
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     

	// AudioSource playerAudio;                                
	// PlayerMovement playerMovement;              
	PlayerShooting playerShooting;                           
	bool isDead;                                            
	bool damaged;                                            


	void Awake (){
		//playerMovement = GetComponent <PlayerMovement> ();
		currentHealth = startingHealth;
	}

	void Start () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
	}

	void Update (){
		if(damaged){
		    damageImage.color = flashColour;
		}
		else{
		    damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
				
		damaged = false;
	}


	public void TakeDamage (int amount) {
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		HealthText.text = currentHealth.ToString ();
		//    playerAudio.Play ();
			GetComponent<AudioSource> ().clip = deathClip;
			GetComponent<AudioSource> ().Play ();

		if(currentHealth <= 0 && !isDead){
		    Death ();
		}
	}


	void Death (){
		isDead = true;
		//  playerAudio.clip = deathClip;
		//  playerAudio.Play ();
		//playerMovement.enabled = false;
	}   

	public void Medicina(){
		this.currentHealth = this.currentHealth + 15;
		healthSlider.value = currentHealth;
		HealthText.text = currentHealth.ToString ();

	}

	void ApplyDamage(){
		Medicina ();
	}


}