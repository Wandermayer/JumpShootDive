using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuBehaviour : MonoBehaviour {

	private bool helpFadeIn = false;
	private float helpTransition;
	public GameObject helpCanvas;
	// Use this for initialization
	void Start () {
		//helpCanvas.SetActive (false);
		helpCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		helpCanvas.GetComponent<CanvasGroup> ().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		helpTransition = Mathf.Clamp (helpTransition, 0f, 1f);

		if (helpFadeIn) {
			helpTransition += Time.deltaTime;
			helpCanvas.GetComponent<CanvasGroup> ().alpha = helpTransition;
		} else {
			helpTransition -= Time.deltaTime;
			helpCanvas.GetComponent<CanvasGroup> ().alpha = helpTransition;
		}

		if (helpCanvas.GetComponent<CanvasGroup> ().alpha == 0) {
			helpCanvas.SetActive (false);
		}


	}

	public void onPlay(){
		SceneManager.LoadScene ("Scene_5");
	}

	public void onHelp(){
		helpCanvas.SetActive (true);
		helpCanvas.GetComponent<CanvasGroup> ().interactable = true;
		helpFadeIn = true;
	}

	public void exitHelp(){
		helpFadeIn = false;
		helpCanvas.GetComponent<CanvasGroup> ().interactable = false;
	}
}
