using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	//Array
	public Sprite[] character1;
	public Sprite[] character2;
	public Sprite[] character3;
	//integers
	int currentCharacterIndex;
	int arrayNumber;
	//Booleans
	bool has1Jumped;
	bool has2Jumped;
	public bool player1SelectionDone;
	public bool player2SelectoinDone;
	//Camera
	public Camera mainCamera;
	//Transform
	public Transform position1;
	public Transform position2;
	// Use this for initialization
	void Start () {
		infoText.text = "";
		mainCamera.transform.position = position1.position;
		player1Renderer = player1GO.GetComponentInChildren<SpriteRenderer> ();
		player2Renderer = player2GO.GetComponentInChildren<SpriteRenderer> ();
		player1 = player1GO.GetComponent<Rigidbody> ();
		player2 = player2GO.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
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
		if (!player1SelectionDone) {
			if(Input.GetKeyDown(KeyCode.Q) && !has1Jumped){
				has1Jumped = true;
				currentCharacterIndex ++;
				arrayNumber++;
				StartCoroutine(changeCharacter1());
				player1.velocity = Vector3.zero;
				infoText.text = "Player 1: Summon your character!";
				player1.AddForce(0, 550, 0);
			}
		}

		if (player1SelectionDone && !player2SelectoinDone) {
			if(Input.GetKeyDown(KeyCode.UpArrow) && !has2Jumped){
				has2Jumped = true;
				StartCoroutine(changeCharacter2());
				arrayNumber++;
				infoText.text = "Player 2: Choose you warrior!";
				player2.velocity = Vector3.zero;
				player2.AddForce(0, 550, 0);
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			mainCamera.transform.position = position2.position;
			player1SelectionDone = true;
			FindObjectOfType<CharacterPrefs>().player1Pref = arrayNumber;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			player2SelectoinDone = true;
			FindObjectOfType<CharacterPrefs>().player2Pref = arrayNumber;
			Application.LoadLevel(2);
		}
	}

	IEnumerator changeCharacter1(){

		yield return new WaitForSeconds (0.6f);{
			if(arrayNumber == 1){
				player1Renderer.sprite = character1[0];
			}else if(arrayNumber == 2){
				player1Renderer.sprite = character2[0];

			}else{
				player1Renderer.sprite = character3[0];
			}

		}
	}

	IEnumerator changeCharacter2(){
		yield return new WaitForSeconds (0.6f);{
			if(arrayNumber == 1){
				player2Renderer.sprite = character1[0];
			}else if(arrayNumber == 2){
				player2Renderer.sprite = character2[0];
				
			}else{
				player2Renderer.sprite = character3[0];
			}
			
		}
	}
}
