using UnityEngine;
using System.Collections;

public class Player2Movement : MonoBehaviour {
	
	
	//booleans 
	public bool isInPlay;
	bool grounded = true;
	bool doubleJump = false;
	bool ammo;
	bool dive = false;
	bool playedLandingEffect;
	bool hasDived;
	bool hasDoubleJumped;
	public bool canPlay;
	public bool isDead;
	//sprites
	public Sprite dead;
	public Sprite idle;
	public Sprite jumping;
	public Sprite diving;
	//floats
	public float jumpHeight;
	
	//references
	public Rigidbody myRigidbody;
	public Transform projectileSpawn;
	public GameObject projectile;
	public GameObject renderSprite;
	public GameObject flashMuzzle;
	public ParticleSystem deathEffect;
	public ParticleSystem landingEffect;
	//Camera
	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		canPlay = true;

	}
	
	// Update is called once per frame
	void Update () {

		if (isDead) {
			StartCoroutine(deathSequence1());
			
		}
		if (!canPlay || isDead) {
				return;
			}

		
		
		
		if (Input.GetKeyDown (KeyCode.LeftShift) && ammo && !grounded) {
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			FindObjectOfType<AudioManager>().gun1.Play();
			StartCoroutine(muzzleFlash());
			ammo = false;
		}
		
		if (Input.GetKeyDown (KeyCode.Q) ) {
			playedLandingEffect = false;
			if(grounded){
				FindObjectOfType<AudioManager>().jump1.Play();
				hasDoubleJumped = false;
				hasDived = false;
				myRigidbody.AddForce(0, jumpHeight, 0);
				doubleJump = true;
				ammo = true;
			}else{
				if(doubleJump){
					StartCoroutine(doubleJumping());
					hasDoubleJumped = true;
					dive = true;
					FindObjectOfType<AudioManager>().jump2.Play();
					doubleJump = false;
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce(0, jumpHeight, 0);
					
				}
				else{
					if(dive){
						hasDived = true;
						//GetComponentInChildren<SpriteRenderer>().sprite = diving;
						renderSprite.GetComponent<SpriteRenderer>().sprite = diving;
						FindObjectOfType<AudioManager>().dive2.Play();
						myRigidbody.velocity = Vector3.zero;
						myRigidbody.AddForce(0, -jumpHeight,0);
					}
				}
			}
		}
		
		if (transform.position.y > 1) {
			grounded = false;
			if(!hasDived && !hasDoubleJumped){
				//GetComponentInChildren<SpriteRenderer>().sprite = jumping;
				renderSprite.GetComponent<SpriteRenderer>().sprite = jumping;
			}
		} else {
			//Debug.Log("On za ground");
			//GetComponentInChildren<SpriteRenderer>().sprite = idle;
			if(!playedLandingEffect){
				StartCoroutine(landingAnim());
				FindObjectOfType<AudioManager>().idleMusic.Play();
			}
			renderSprite.GetComponent<SpriteRenderer>().sprite = idle;

			grounded = true;
		}
		
	}
	IEnumerator landingAnim(){
		
		yield return new WaitForSeconds (0.05f);
		if (transform.position.y < 1) {
			Destroy (Instantiate (landingEffect, transform.position, Quaternion.identity) as GameObject, 0.1f);
			playedLandingEffect = true;
		} else {
			playedLandingEffect = false;
		}
	}

	IEnumerator deathSequence1(){
		renderSprite.GetComponent<SpriteRenderer> ().sprite = dead;
		Destroy (Instantiate (deathEffect.gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity)as GameObject, 1f);
		isDead = false;
		canPlay = false;
		yield return new WaitForSeconds (1.5f);{
		
			canPlay = true;
		}
	}




	public void onPlayed(){

		StartCoroutine (startAnim ());
	}

	IEnumerator muzzleFlash(){
		flashMuzzle.SetActive (true);
		yield return new WaitForSeconds (0.1f);{
			flashMuzzle.SetActive(false);
		}
	}

	IEnumerator startAnim(){
		GetComponent<Animator> ().SetBool ("JumpedNow", true);
		yield return new WaitForSeconds (0.2f);{
			GetComponent<Animator> ().SetBool ("JumpedNow", false);
		}
	}
	IEnumerator doubleJumping(){
		//GetComponentInChildren<SpriteRenderer> ().sprite = idle;
		renderSprite.GetComponent<SpriteRenderer>().sprite = idle;
		yield return new WaitForSeconds(0.2f);{
			renderSprite.GetComponent<SpriteRenderer>().sprite = jumping;
		//	GetComponentInChildren<SpriteRenderer>().sprite = jumping;
		}
	}
	
	
}