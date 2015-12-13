using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public bool facingRight;
	float velocity = -1;
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
		if (other.tag == "Player1") {
			Debug.Log("Player2 wins!");
		}
		else{
			if(other.tag == "Player2"){
				Debug.Log ("Player1 Wins");
			}
		}
	}
}

