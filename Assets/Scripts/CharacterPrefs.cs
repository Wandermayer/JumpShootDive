using UnityEngine;
using System.Collections;

public class CharacterPrefs : MonoBehaviour {
	public int player1Pref;
	public int player2Pref;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
