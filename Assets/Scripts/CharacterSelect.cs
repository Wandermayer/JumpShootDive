using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {
	//UI
	public Text infoText;

	//References
	Rigidbody player1;
	Rigidbody player2;
	SpriteRenderer player1Renderer;
	SpriteRenderer player2Renderer;
	public GameObject player2GO;
	public GameObject player1GO;
	private GameObject curInfoPanel;
	//Array
	public Sprite[] character1;
	public Sprite[] character2;
	public Sprite[] character3;
	private Sprite[] curSpriteArray;
	public GameObject[] infoPanels;
	//integers
	public int currentCharacterIndex;
	public int arrayNumber = 1;
	private float transition;
	//Booleans
	bool fadeIn = true;
	bool has1Jumped;
	bool has2Jumped;
	bool moveCamera = false;
	public bool player1SelectionDone;
	public bool player2SelectoinDone;
	//Camera
	public Camera mainCamera;
	//Transform
	public Transform position1;
	public Transform position2;
	// Use this for initialization
	void Start () {
//		Debug.Log (arrayNumber);
		curSpriteArray = character1;
		infoText.text = "Player 1: Summon your character!";
		mainCamera.transform.position = position1.position;
		player1Renderer = player1GO.GetComponentInChildren<SpriteRenderer> ();
		player2Renderer = player2GO.GetComponentInChildren<SpriteRenderer> ();
		player1 = player1GO.GetComponent<Rigidbody> ();
		player2 = player2GO.GetComponent<Rigidbody> ();
		player1Renderer.sprite = curSpriteArray [0];
		curInfoPanel = infoPanels [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (moveCamera) {
			mainCamera.transform.position = new Vector3(Mathf.Lerp(mainCamera.transform.position.x, position2.position.x, 0.1f), position2.position.y, position2.position.z);

		}
		if (player1GO.transform.position.y > 1) {
			has1Jumped = true;
		} else {
			has1Jumped = false;
		}
		if (player2GO.transform.position.y > 1) {
			has2Jumped = true;
		} else {
			has2Jumped = false;
		}
		if (currentCharacterIndex > 2) {
			currentCharacterIndex = 0;
		}
		if (arrayNumber > 3) {
			arrayNumber = 1;
		}
	

		transition = Mathf.Clamp (transition, 0f, 1f);

		if (fadeIn) {
			transition += Time.deltaTime;
		} else {	
			curInfoPanel.GetComponent<CanvasGroup> ().alpha -= Time.deltaTime;
			transition -= Time.deltaTime;
		}

		curInfoPanel.GetComponent<CanvasGroup> ().alpha = transition;


	}

	IEnumerator changeCharacter1(){

		//Debug.Log (arrayNumber);
		yield return new WaitForSeconds (0.6f);{
			if(arrayNumber == 1){
				player1Renderer.sprite = character1[0];
				curSpriteArray = character1;
				StartCoroutine (swapInfoPanels (curInfoPanel, infoPanels [0]));
				curInfoPanel = infoPanels [0];

				}else if(arrayNumber == 2){
				player1Renderer.sprite = character2[0];
				curSpriteArray = character2;
				StartCoroutine (swapInfoPanels (curInfoPanel, infoPanels [1]));
				curInfoPanel = infoPanels [1];

			}else{
				player1Renderer.sprite = character3[0];
				curSpriteArray = character3;
				StartCoroutine (swapInfoPanels (curInfoPanel, infoPanels [2]));
				curInfoPanel = infoPanels [2];
			}

		}
	}
		

	IEnumerator changeCharacter2(){
		yield return new WaitForSeconds (0.6f);{
			if(arrayNumber == 1){
				player2Renderer.sprite = character1[0];
				curSpriteArray = character1;
				StartCoroutine (swapInfoPanels (curInfoPanel, infoPanels [0]));
				curInfoPanel = infoPanels [0];

			}else if(arrayNumber == 2){
				player2Renderer.sprite = character2[0];
				curSpriteArray = character2;
				StartCoroutine (swapInfoPanels (curInfoPanel, infoPanels [1]));
				curInfoPanel = infoPanels [1];

				
			}else{
				player2Renderer.sprite = character3[0];
				curSpriteArray = character3;
				StartCoroutine (swapInfoPanels (curInfoPanel, infoPanels [2]));
				curInfoPanel = infoPanels [2];
			}
			
		}
	}

	IEnumerator swapInfoPanels(GameObject curInfo, GameObject newInfo){
		fadeIn = false;
	
		//curInfo.SetActive (false);
		yield return new WaitForSeconds (1f);

		fadeIn = true;
		newInfo.SetActive (true);
		curInfo.SetActive (false);
	}

	public void leftPlayerChange(){
		if (!player1SelectionDone) {
			if(!has1Jumped){
				has1Jumped = true;
				currentCharacterIndex ++;
				arrayNumber++;
				StartCoroutine(changeCharacter1());
				player1.velocity = Vector3.zero;
				player1.AddForce(0, 550, 0);
				FindObjectOfType<AudioManager>().tambo.Play();
				player1Renderer.sprite = curSpriteArray [1];
			}
		}
	}

	public void leftPlayerSelect(){
		moveCamera = true;
		player1SelectionDone = true;
		FindObjectOfType<CharacterPrefs>().player1Pref = arrayNumber;
		infoText.text = "Player 2: Choose you warrior!";
		player2Renderer.sprite = curSpriteArray [0];
	}

	public void rightPlayerChange(){
		if (player1SelectionDone && !player2SelectoinDone) {
			if(!has2Jumped){
				has2Jumped = true;
				StartCoroutine(changeCharacter2());
				arrayNumber++;
				player2.velocity = Vector3.zero;
				player2.AddForce(0, 550, 0);
				FindObjectOfType<AudioManager>().tambo.Play();
				player2Renderer.sprite = curSpriteArray [1];
			}
		}
	}

	public void rightPlayerSelect(){
		if (player1SelectionDone) {
			player2SelectoinDone = true;
			FindObjectOfType<CharacterPrefs>().player2Pref = arrayNumber;
			SceneManager.LoadScene("Scene_6");
		}
	}
		
}
