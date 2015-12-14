using UnityEngine;
using System.Collections;

public class Player2Movement : MonoBehaviour {
	
	
	//booleans 
	public bool isInPlay;
	bool grounded = true;
	bool doubleJump = false;
	bool ammo;
	bool dive = false;
	bool hasDived;
	bool hasDoubleJumped;
	public bool canPlay;
	//sprites
	public Sprite idle;
	public Sprite jumping;
	public Sprite diving;
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

	}
	
	// Update is called once per frame
	void Update () {
		if (!isInPlay) {
			if (!canPlay) {
				return;
			}
		}
		
		
		if (Input.GetKeyDown (KeyCode.LeftShift) && ammo && !grounded) {
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			ammo = false;
		}
		
		if (Input.GetKeyDown (KeyCode.Q) ) {
			if(grounded){
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
					doubleJump = false;
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce(0, jumpHeight, 0);
					
				}
				else{
					if(dive){
						hasDived = true;
						GetComponentInChildren<SpriteRenderer>().sprite = diving;
						myRigidbody.velocity = Vector3.zero;
						myRigidbody.AddForce(0, -jumpHeight,0);
					}
				}
			}
		}
		
		if (transform.position.y > 1) {
			grounded = false;
			if(!hasDived && !hasDoubleJumped){
				GetComponentInChildren<SpriteRenderer>().sprite = jumping;
			}
		} else {
			//Debug.Log("On za ground");
			GetComponentInChildren<SpriteRenderer>().sprite = idle;
			grounded = true;
		}
		
	}

	public void onPlayed(){

		StartCoroutine (startAnim ());
	}
	IEnumerator startAnim(){
		GetComponent<Animator> ().SetBool ("JumpedNow", true);
		yield return new WaitForSeconds (0.2f);{
			GetComponent<Animator> ().SetBool ("JumpedNow", false);
		}
	}
	IEnumerator doubleJumping(){
		GetComponentInChildren<SpriteRenderer> ().sprite = idle;
		
		yield return new WaitForSeconds(0.2f);{
			
			GetComponentInChildren<SpriteRenderer>().sprite = jumping;
		}
	}
	
	
}