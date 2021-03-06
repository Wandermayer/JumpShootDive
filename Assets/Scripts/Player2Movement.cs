﻿using UnityEngine;
using System.Collections;

public class Player2Movement : MonoBehaviour {
	//integers
	public int characterIndexNumber;
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
	public Sprite[] character1;
	public Sprite[] character2;
	public Sprite[] character3;
	public Sprite dead;
	public Sprite idle;
	public Sprite jumping;
	public Sprite diving;
	//floats
	public float jumpHeight;
	public float diveHeight;

	
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
		characterIndexNumber = FindObjectOfType<CharacterPrefs>().player1Pref;
		if (characterIndexNumber == 1) {
			idle = character1 [0];
			jumping = character1 [1];
			diving = character1 [2];
			dead = character1 [3];
		} else if (characterIndexNumber == 2) {
			idle = character2 [0];
			jumping = character2 [1];
			diving = character2 [2];
			dead = character2 [3];
		} else {
			idle = character3[0];
			jumping = character3[1];
			diving = character3[2];
			dead = character3[3];
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (isDead) {
			StartCoroutine(deathSequence1());
			
		}
		if (!canPlay || isDead) {
				return;
			}

		
		
		
		if (Input.GetKeyDown (KeyCode.LeftShift) && !grounded) {
			if(ammo == true){
				Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
				FindObjectOfType<AudioManager>().gun1.Play();
				StartCoroutine(muzzleFlash());
				ammo = false;
			}
			else{
				if(!hasDived){
					hasDived = true;
					//GetComponentInChildren<SpriteRenderer>().sprite = diving;
					renderSprite.GetComponent<SpriteRenderer>().sprite = diving;
					FindObjectOfType<AudioManager>().dive2.Play();
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce(0, diveHeight,0);
				}
			}
			
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
					if(dive && !hasDived){
						hasDived = true;
						//GetComponentInChildren<SpriteRenderer>().sprite = diving;
						renderSprite.GetComponent<SpriteRenderer>().sprite = diving;
						FindObjectOfType<AudioManager>().dive2.Play();
						myRigidbody.velocity = Vector3.zero;
						myRigidbody.AddForce(0, diveHeight,0);
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