﻿using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour {
	//booleans 
	public bool isInPlayed;
	bool grounded = true;
	bool doubleJump = false;
	bool ammo;
	bool dive = false;
	bool hasDived;
	bool hasDoubleJumped;
	public bool canPlayed;
	public bool isDead;
	bool playedLandingEffect;
	//sprites
	public Sprite dead;
	public Sprite idle;
	public Sprite jumping;
	public Sprite diving;
	public GameObject muzzleFlash;
	public GameObject renderSprite;
	public ParticleSystem deathEffect;
	public ParticleSystem landingEffect;
	//floats
	public float jumpHeight;

	//references
	public Rigidbody myRigidbody;
	public Transform projectileSpawn;
	public GameObject projectile;

	//Camera
	public Camera mainCamera;
	// Use this for initialization
	void Start () {
		canPlayed = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			StartCoroutine(deathSequence());
		}
		if (!canPlayed || isDead) {
			return;
		}
		if (!isInPlayed) {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)){
				Application.LoadLevel(1);
			}

		}

		if (Input.GetKeyDown (KeyCode.Space) && ammo && !grounded) {
			StartCoroutine(MuzzleFlash());
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			FindObjectOfType<AudioManager>().gun1.Play();
			ammo = false;
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow) ) {
			playedLandingEffect = false;
			if(grounded){
				playedLandingEffect = false;
				hasDoubleJumped = false;
				FindObjectOfType<AudioManager>().jump1.Play();
				hasDived = false;
				myRigidbody.AddForce(0, jumpHeight, 0);
				doubleJump = true;
				ammo = true;
			}else{
				if(doubleJump){
					StartCoroutine(doubleJumping());
					hasDoubleJumped = true;
					FindObjectOfType<AudioManager>().jump1.Play();
					dive = true;
					doubleJump = false;
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce(0, jumpHeight, 0);
					
				}
				else{
					if(dive){
						hasDived = true;
						//GetComponentInChildren<SpriteRenderer>().sprite = diving;
						renderSprite.GetComponent<SpriteRenderer>().sprite = diving;
						FindObjectOfType<AudioManager>().dive1.Play();
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
			
			//Destroy(Instantiate(landingEffect, transform.position, Quaternion.identity) as GameObject, 0.5f);
			
			renderSprite.GetComponent<SpriteRenderer>().sprite = idle;

			grounded = true;
		}
		
	}
	public void onPlay(){
		StartCoroutine (startAnim ());
		FindObjectOfType<Player2Movement> ().onPlayed ();
		StartCoroutine (loadScene ());
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

	IEnumerator MuzzleFlash(){
		muzzleFlash.SetActive (true);
		yield return new WaitForSeconds (0.1f);{
			muzzleFlash.SetActive(false);

		}
	}

	IEnumerator loadScene(){
		yield return new WaitForSeconds (0.8f);{
			Application.LoadLevel(1);
		}
	}
	IEnumerator startAnim(){
		GetComponent<Animator> ().SetBool ("JumpNow", true);
		yield return new WaitForSeconds (0.2f);{
			GetComponent<Animator> ().SetBool ("JumpNow", false);
		}
	}

	IEnumerator doubleJumping(){
		//GetComponentInChildren<SpriteRenderer> ().sprite = idle;
		renderSprite.GetComponent<SpriteRenderer>().sprite = idle;
		yield return new WaitForSeconds(0.2f);{
			renderSprite.GetComponent<SpriteRenderer>().sprite = jumping;
		//GetComponentInChildren<SpriteRenderer>().sprite = jumping;
		}
	}

	IEnumerator deathSequence(){
		isDead = false;
		renderSprite.GetComponent<SpriteRenderer>().sprite = dead;
		canPlayed = false;
		Destroy (Instantiate (deathEffect,new Vector3(transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity) as GameObject, 1f);
		yield return new WaitForSeconds(2f);{
			canPlayed = true;

		}
	}

}
