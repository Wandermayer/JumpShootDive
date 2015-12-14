using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool hasFinishedAnim;
	public Animator anim;
	public bool playing;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		FindObjectOfType<Player2Movement> ().canPlay = false;
		FindObjectOfType<Player1Movement> ().canPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Done")) {
			//Debug.Log ("Works!");
			hasFinishedAnim = true;
			canvas.SetActive (true);
		} else {
			//Debug.LogWarning ("Does't work!");
			hasFinishedAnim = false;
			canvas.SetActive (false);
		}
	}
	}

}