using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum BulletClass2{
	Sword,
	Double,
	Fast
}

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
	bool doubleShot;
	bool shootDouble;
	//sprites
	public Sprite[] character1;
	public Sprite[] character2;
	public Sprite[] character3;
	public Sprite[] buttonImages;
	public Sprite dead;
	public Sprite idle;
	public Sprite jumping;
	public Sprite diving;
	//floats
	public float jumpHeight;
	public float diveHeight;

	
	//references
	public Button leftAction;
	public Rigidbody myRigidbody;
	public Transform projectileSpawn;
	public GameObject[] projectiles;
	public GameObject renderSprite;
	public GameObject flashMuzzle;
	public ParticleSystem deathEffect;
	public ParticleSystem landingEffect;
	GameObject projectile;
	//Camera
	public Camera mainCamera;

	//Enums lol
	public BulletClass2 typeOfBullet;

	// Use this for initialization
	void Start () {
		//canPlay = true;
		if (canPlay) {
			characterIndexNumber = FindObjectOfType<CharacterPrefs> ().player1Pref;
			if (characterIndexNumber == 1) {
				idle = character1 [0];
				jumping = character1 [1];
				diving = character1 [2];
				dead = character1 [3];
				typeOfBullet = BulletClass2.Fast;
				shootDouble = false;
				projectile = projectiles [0];
			} else if (characterIndexNumber == 2) {
				idle = character2 [0];
				jumping = character2 [1];
				diving = character2 [2];
				dead = character2 [3];
				typeOfBullet = BulletClass2.Double;
				shootDouble = true;
				projectile = projectiles [0];
			} else {
				idle = character3 [0];
				jumping = character3 [1];
				diving = character3 [2];
				dead = character3 [3];
				typeOfBullet = BulletClass2.Sword;
				shootDouble = false;
				projectile = projectiles [1];
			}
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
	//	if (Input.GetKeyDown (KeyCode.LeftShift) && !grounded) {
	//			Shoot ();
	//	}
		
	//	if (Input.GetKeyDown (KeyCode.Q) ) {
	//		Move ();
	//	}
		
		if (transform.position.y > 1) {
			grounded = false;
			if(!hasDived && !hasDoubleJumped){
				//GetComponentInChildren<SpriteRenderer>().sprite = jumping;
				renderSprite.GetComponent<SpriteRenderer>().sprite = jumping;
			}
		} else {
			grounded = true;
			renderSprite.GetComponent<SpriteRenderer>().sprite = idle;
			//Debug.Log("On za ground");
			//GetComponentInChildren<SpriteRenderer>().sprite = idle;
			if(!playedLandingEffect){
				StartCoroutine(landingAnim());
				FindObjectOfType<AudioManager>().idleMusic.Play();
			}
		//	leftAction.text = "Jump!";
			leftAction.GetComponent<Image>().sprite = buttonImages[0];

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

	public void Move(){
		if (canPlay) {
			playedLandingEffect = false;
			if (grounded) {
				FindObjectOfType<AudioManager> ().jump1.Play ();
			//	leftAction.text = "Jump Again!";
				leftAction.GetComponent<Image>().sprite = buttonImages[0];
				hasDoubleJumped = false;
				hasDived = false;
				myRigidbody.AddForce (0, jumpHeight, 0);
				doubleJump = true;
				ammo = true;
			} else {
				if (doubleJump) {
					StartCoroutine (doubleJumping ());
					leftAction.GetComponent<Image>().sprite = buttonImages[1];
				//	leftAction.text = "Dive!";
					hasDoubleJumped = true;
					dive = true;
					FindObjectOfType<AudioManager> ().jump2.Play ();
					doubleJump = false;
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce (0, jumpHeight, 0);

				} else {
					if (dive && !hasDived) {
						hasDived = true;
						//GetComponentInChildren<SpriteRenderer>().sprite = diving;
						renderSprite.GetComponent<SpriteRenderer> ().sprite = diving;
						FindObjectOfType<AudioManager> ().dive2.Play ();
						myRigidbody.velocity = Vector3.zero;
						myRigidbody.AddForce (0, diveHeight, 0);
					}
				}
			}
		}
	}

	public void Shoot(){

		if (doubleShot == true && !grounded && canPlay && typeOfBullet == BulletClass2.Double) {
			Debug.Log ("Fire2");
			StartCoroutine(muzzleFlash());
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			if (shootDouble) {
				FindObjectOfType<Projectile> ().velocity = -0.35f;
			} //else {
				//FindObjectOfType<Projectile> ().velocity = -1.5f;
			//}
			FindObjectOfType<AudioManager>().gun1.Play();
			doubleShot = false;
			ammo = false;
		}

		if(ammo == true && !grounded && canPlay){
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			FindObjectOfType<AudioManager>().gun1.Play();
			StartCoroutine(muzzleFlash());
			if (shootDouble) {
				FindObjectOfType<Projectile> ().velocity = -0.35f;
			}// else {
			//	FindObjectOfType<Projectile> ().velocity = -1.5f;
			//}
			ammo = false;
			doubleShot = true;
		}
	}
	
	
}