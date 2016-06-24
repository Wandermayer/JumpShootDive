using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum BulletClass{
	Sword,
	Double,
	Fast
}

public class Player1Movement : MonoBehaviour {
	//Integers
	public int characterIndexNumber;
	//booleans 
	public bool isInPlayed;
	public bool canPlayed;
	public bool isDead;
	bool grounded = true;
	bool doubleJump = false;
	bool ammo;
	bool dive = false;
	bool hasDived;
	bool hasDoubleJumped;
	bool playedLandingEffect;
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
	public Button rightAction;
	public Rigidbody myRigidbody;
	public Transform projectileSpawn;
	public GameObject[] projectiles;
	GameObject projectile;
	public GameObject muzzleFlash;
	public GameObject renderSprite;
	public ParticleSystem deathEffect;
	public ParticleSystem landingEffect;

	//Camera
	public Camera mainCamera;

	//Enums lol
	public BulletClass typeOfBullet;
	// Use this for initialization
	void Start () {
		//canPlayed = true;
		doubleShot = false;
		if (canPlayed) {
			characterIndexNumber = FindObjectOfType<CharacterPrefs> ().player2Pref;
			if (characterIndexNumber == 1) {
				idle = character1 [0];
				jumping = character1 [1];
				diving = character1 [2];
				dead = character1 [3];
				typeOfBullet = BulletClass.Fast;
				shootDouble = false;
				projectile = projectiles [0];
			} else if (characterIndexNumber == 2) {
				idle = character2 [0];
				jumping = character2 [1];
				diving = character2 [2];
				dead = character2 [3];
				typeOfBullet = BulletClass.Double;
				shootDouble = true;
				projectile = projectiles [0];
			//	Debug.Log ("DOUBLE!");
			} else {
				idle = character3 [0];
				jumping = character3 [1];
				diving = character3 [2];
				dead = character3 [3];
				typeOfBullet = BulletClass.Sword;
				shootDouble = false;
				projectile = projectiles [1];
			}
		
		}
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
				SceneManager.LoadScene ("Scene_5");
			}

		}

		//if (Input.GetKeyDown (KeyCode.Space) && !grounded) {
		//	Shoot ();

		//}
		
		//if (Input.GetKeyDown (KeyCode.UpArrow) ) {
	//		Move ();
		//}
		
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
			//rightAction.image. = "Jump!";
			rightAction.GetComponent<Image>().sprite = buttonImages[0];
		
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
			SceneManager.LoadScene("MainMenu");
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

	public void Move(){
		if (canPlayed) {
			playedLandingEffect = false;
			if (grounded) {
			//	rightAction.text = "Jump Again!";
				rightAction.GetComponent<Image>().sprite = buttonImages[0];
				playedLandingEffect = false;
				hasDoubleJumped = false;
				FindObjectOfType<AudioManager> ().jump1.Play ();
				hasDived = false;
				myRigidbody.AddForce (0, jumpHeight, 0);
				doubleJump = true;
				ammo = true;
			} else {
				if (doubleJump) {
			//		rightAction.text = "Dive!";
					rightAction.GetComponent<Image>().sprite = buttonImages[1];
					StartCoroutine (doubleJumping ());
					hasDoubleJumped = true;
					FindObjectOfType<AudioManager> ().jump1.Play ();
					dive = true;
					doubleJump = false;
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce (0, jumpHeight, 0);

				} else {
					if (dive && !hasDived) {
						hasDived = true;
						//GetComponentInChildren<SpriteRenderer>().sprite = diving;
						renderSprite.GetComponent<SpriteRenderer> ().sprite = diving;
						FindObjectOfType<AudioManager> ().dive1.Play ();
						myRigidbody.velocity = Vector3.zero;
						myRigidbody.AddForce (0, diveHeight, 0);
					}
				}
			}
		}
	}

	public void Shoot(){

		if (doubleShot == true && !grounded && canPlayed && typeOfBullet == BulletClass.Double) {
		//	Debug.Log ("Fire2");
			StartCoroutine(MuzzleFlash());
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			if (shootDouble) {
				FindObjectOfType<Projectile> ().velocity = -0.35f;
			}// else {
			//	FindObjectOfType<Projectile> ().velocity = -1.5f;
			//}
			FindObjectOfType<AudioManager>().gun1.Play();
			doubleShot = false;
			ammo = false;
		}
		
		if(ammo == true && !grounded && canPlayed){
//			Debug.Log ("Fire1");
			StartCoroutine(MuzzleFlash());
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			if (shootDouble) {
				FindObjectOfType<Projectile> ().velocity = -0.35f;
			}// else {
			//	FindObjectOfType<Projectile> ().velocity = -1.5f;
			//}
			FindObjectOfType<AudioManager>().gun1.Play();
			ammo = false;
			doubleShot = true;
		}


	}

		

}
