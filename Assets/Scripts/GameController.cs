using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool hasFinishedAnim;
	public Animator anim;

	public GameObject canvas;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Done")) {
			//Debug.Log ("Works!");
			hasFinishedAnim = true;
			canvas.SetActive(true);
		} else {
			//Debug.LogWarning ("Does't work!");
			hasFinishedAnim = false;
			canvas.SetActive(false);
		}
	}

}