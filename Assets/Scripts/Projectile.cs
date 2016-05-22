using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public bool facingRight;
	public float velocity = -1.5f;
	float lifeTime = 3f;

	// Use this for initialization
	void Start () {
		if (facingRight) {
			velocity = -velocity;
		}
		Destroy (gameObject, lifeTime);



	}
	
	// Update is called once per frame
	void Update () {



		transform.position = new Vector3 (transform.position.x + velocity, transform.position.y, transform.position.z);

	}

	void OnTriggerEnter(Collider other){
		FindObjectOfType<AudioManager>().hit1.Play();
		if (other.tag == "player1Collider" ) {
			FindObjectOfType<Player2Movement>().isDead = true;
		//	Debug.Log("Player2 wins!");
			FindObjectOfType<GameController>().player2Score ++;
			FindObjectOfType<GameController> ().AddPlayer1DeathIcon (FindObjectOfType<GameController> ().player2Score);

		}
		else{
			if(other.tag != "Player1" && other.tag == "player2Collider"){
				FindObjectOfType<Player1Movement>().isDead = true;
//				Debug.Log ("Player1 Wins");
				FindObjectOfType<GameController>().player1Score ++;
				FindObjectOfType<GameController> ().AddPlayer2DeathIcon (FindObjectOfType<GameController> ().player1Score);
			}
		}
	}
}

