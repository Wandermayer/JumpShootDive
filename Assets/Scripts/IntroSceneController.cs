using UnityEngine;
using System.Collections;

public class IntroSceneController : MonoBehaviour {


	public bool isInPlay;

	// Use this for initialization
	void Start () {
	
		if (!isInPlay) {

			FindObjectOfType<Player2Movement> ().enabled = false;
			FindObjectOfType<Player1Movement> ().enabled = false;
		} else {
			Debug.Log("WIERD!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
