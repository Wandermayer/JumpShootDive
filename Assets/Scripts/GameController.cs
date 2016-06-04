using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameController : MonoBehaviour {

	private Cloud[] clouds;
	public GameObject[] backdrops;
	public GameObject bg;
	public bool hasFinishedAnim;
	public Animator anim;
	public bool playing;
	//public GameObject canvas;
	public int player1Score;
	public int player2Score;


	public Text player1Text;
	public Text player2Text;
	public Text winText;
	public bool isInPlay;
	public GameObject charSelectButton;
	public GameObject restartButton;
	public GameObject mainMenuButton;

	private GameObject[] player1healths = new GameObject[3];
	private GameObject[] player2healths = new GameObject[3];
	private GameObject pauseMenu;
	private GameObject[] allSwords;
	private GameObject[] allbullets;


	//private Vector3 targetCloud;
	// Use this for initialization
	void Start () {
		
		//bg.GetComponent<Material> ().color = backdrops [Random.Range (0, 3)];
		//bg.GetComponent<Renderer>().material = backdrops[Random.Range(0,3)];
		backdrops[Random.Range(0, 2)].SetActive(true);
	
		winText.text = " ";
		anim = GetComponent<Animator> ();
		if (!isInPlay) {

			FindObjectOfType<Player2Movement> ().enabled = false;
			FindObjectOfType<Player1Movement> ().enabled = false;
		}

		player1healths = GameObject.FindGameObjectsWithTag ("Health1");

		player2healths = GameObject.FindGameObjectsWithTag ("Health2");

		foreach (GameObject i in player1healths) {
			i.SetActive (false);
		}

		foreach (GameObject i in player2healths) {
			i.SetActive (false);
		}

		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		pauseMenu.SetActive (false);
	//	targetCloud = cloud2Target.position;
		clouds = FindObjectsOfType<Cloud>();

	}


	// Update is called once per frame
	void Update () {
		allSwords = GameObject.FindGameObjectsWithTag ("Sword");
		allbullets = GameObject.FindGameObjectsWithTag ("Bullet");


		if (player1Score >= 3) {

			FindObjectOfType<Player2Movement>().enabled = false;
			FindObjectOfType<Player1Movement>().enabled = false;
			restartButton.SetActive (true);
			charSelectButton.SetActive(true);
			mainMenuButton.SetActive(true);
			winText.text = "Player 1 wins!";

		} else {
			if(player2Score >= 3){

				FindObjectOfType<Player2Movement>().enabled = false;
				FindObjectOfType<Player1Movement>().enabled = false;
				charSelectButton.SetActive(true);
				restartButton.SetActive (true);
				mainMenuButton.SetActive(true);
				winText.text = "Player 2 wins!";

			}
		}
		if (isInPlay) {
		//	player1Text.text = "Score: " + player1Score;
		//	player2Text.text = "Score: " + player2Score;
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



	public void charSelect(){
		FindObjectOfType<Player1Movement> ().canPlayed = true;
		FindObjectOfType<Player2Movement> ().canPlay = true;
		Time.timeScale = 1;
		SceneManager.LoadScene ("Scene_5");
	}

	public void restartgame(){
		//Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1;
		FindObjectOfType<Player1Movement> ().canPlayed = true;
		FindObjectOfType<Player2Movement> ().canPlay = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void AddPlayer1DeathIcon(int score){
		Debug.Log (score);
		//GameObject.Find ("Player1Health" + score).SetActive(true);
		player1healths[score -1].SetActive(true);
	}

	public void AddPlayer2DeathIcon(int score){
 		Debug.Log ("Player2Health" + score);
	//	GameObject.Find ("Player2Health" + score).SetActive(true);
		player2healths[score -1].SetActive(true);
	}

	public void mainMenu(){
		FindObjectOfType<Player1Movement> ().canPlayed = true;
		FindObjectOfType<Player2Movement> ().canPlay = true;
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}

	public void onPause(){
		Time.timeScale = 0;
		pauseMenu.SetActive (true);
		FindObjectOfType<Player1Movement> ().canPlayed = false;
		FindObjectOfType<Player2Movement> ().canPlay = false;
		FindObjectOfType<CanvasGroup> ().alpha = 1;
		FindObjectOfType<CanvasGroup> ().interactable = true;
		foreach (Cloud c in clouds) {
			c.enabled = false;
		}

		foreach (GameObject r in allSwords) {
			if (r != null) {
				r.GetComponent<SamuraiSword> ().enabled = false;
			}
		}

		foreach (GameObject r in allbullets) {
			if (r != null) {
				r.GetComponent<Projectile> ().enabled = false;
			}
		}
	}

	public void onExitPause(){
		

		FindObjectOfType<Player1Movement> ().canPlayed = true;
		FindObjectOfType<Player2Movement> ().canPlay = true;
		FindObjectOfType<CanvasGroup> ().alpha = 0;
		FindObjectOfType<CanvasGroup> ().interactable = false;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		foreach (Cloud c in clouds) {
			c.enabled = true;
		}

		foreach (GameObject r in allSwords) {
			if (r != null) {
				r.GetComponent<SamuraiSword> ().enabled = true;
			}
		}

		foreach (GameObject r in allbullets) {
			if (r != null) {
				r.GetComponent<Projectile> ().enabled = true;
			}
		}
	}



}