using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPlay(){
		SceneManager.LoadScene ("Scene_5");
	}

	public void onHelp(){
		
	}
}
