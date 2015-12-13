using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour {


	public float jumpHeight;
	public Rigidbody myRigidbody;
	bool grounded = true;
	bool doubleJump = false;
	public Transform projectileSpawn;
	public GameObject projectile;
	bool ammo;
	bool dive = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && ammo && !grounded) {
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			ammo = false;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) ) {
			if(grounded){
			myRigidbody.AddForce(0, jumpHeight, 0);
				doubleJump = true;
				ammo = true;
			}else{
				if(doubleJump){
					dive = true;
					doubleJump = false;
					myRigidbody.velocity = Vector3.zero;
					myRigidbody.AddForce(0, jumpHeight, 0);

				}
				else{
					if(dive){
						myRigidbody.velocity = Vector3.zero;
						myRigidbody.AddForce(0, -jumpHeight, 0);
					}
				}
			}
		}

		if (transform.position.y > 1) {
			grounded = false;
		} else {
			grounded = true;
		}

	}


}
