﻿using UnityEngine;
using System.Collections;

public class SamuraiSword : MonoBehaviour {
	public bool facingRight;
	public float velocity = 5f;
	float lifetime = 3f;
	Vector3 arc;
	Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		Debug.Log (velocity);
	//	if (facingRight) {
	//	velocity = -velocity;
	//	}
		Destroy (gameObject, lifetime);


		myRigid = GetComponent<Rigidbody> ();
		//arc = Vector3.up + Vector3.right;
		//Debug.Log (arc);
		if (facingRight) {
			arc = Vector3.up * velocity + Vector3.right * velocity;
		} else {
			arc = Vector3.up * velocity + Vector3.left * velocity;
		}
		//Debug.Log (arc);
		myRigid.AddForce (arc);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, -750 * Time.deltaTime));
		//GameObject.FindGameObjectWithTag("SwordSprite").transform.Rotate (new Vector3 (0, 0, -750 * Time.deltaTime));
	}


	void OnTriggerEnter(Collider other){
		FindObjectOfType<AudioManager>().hit1.Play();
		if (other.tag == "player1Collider") {

			if (FindObjectOfType<Player2Movement> ().canPlay) {
				FindObjectOfType<Player2Movement> ().isDead = true;
				//	Debug.Log("Player2 wins!");
				FindObjectOfType<GameController> ().player2Score++;
				FindObjectOfType<GameController> ().AddPlayer1DeathIcon (FindObjectOfType<GameController> ().player2Score);

			}
		}
		else{
			if (other.tag != "Player1" && other.tag == "player2Collider") {

				if (FindObjectOfType<Player1Movement> ().canPlayed) {
					FindObjectOfType<Player1Movement> ().isDead = true;
					//				Debug.Log ("Player1 Wins");
					FindObjectOfType<GameController> ().player1Score++;
					FindObjectOfType<GameController> ().AddPlayer2DeathIcon (FindObjectOfType<GameController> ().player1Score);
				}
			}
		}
	}

}
