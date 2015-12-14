using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool hasFinishedAnim;
	public Animator anim;
	public bool playing;
	public GameObject canvas;
	public int player1Score;
	public int player2Score;

	public Text player1Text;
	public Text player2Text;
	public Text winText;
	public bool isInPlay;

	public GameObject restartButton;

	// Use this for initialization
	void Start () {
		winText.text = " ";
		anim = GetComponent<Animator> ();
		if (!isInPlay) {
			FindObjectOfType<Player2Movement> ().canPlay = false;
			FindObjectOfType<Player1Movement> ().canPlayed = false;
		}
	}
	
	// Update is called once per frame
	void Update () {


		if (player1Score >= 3) {
			FindObjectOfType<Player2Movement>().enabled = false;
			FindObjectOfType<Player1Movement>().enabled = false;
			restartButton.SetActive (true);
			winText.text = "Player 1 wins!";

		} else {
			if(player2Score >= 3){
				FindObjectOfType<Player2Movement>().enabled = false;
				FindObjectOfType<Player1Movement>().enabled = false;
				restartButton.SetActive (true);
				winText.text = "Player 2 wins!";

			}
		}
		if (isInPlay) {
			player1Text.text = "Score: " + player1Score;
			player2Text.text = "Score: " + player2Score;
		}
		if(playing){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Done")) {
			//Debug.Log ("Works!");
			hasFinishedAnim = true;
			//canvas.SetActive (true);
		} else {
			//Debug.LogWarning ("Does't work!");
			hasFinishedAnim = false;
			//canvas.SetActive (false);
			}
		}
	}

	public void restartgame(){
		Application.LoadLevel (Application.loadedLevel);
	}

}