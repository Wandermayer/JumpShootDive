using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	bool hasFinshedAnim;
	public Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Test2_Anim")) {
			Debug.Log ("Works!");
		} else {
			Debug.LogWarning ("Does't work!");
		}
	}

}