using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	//Booleans
	public bool canPlaySound;
	//Audio
	public AudioSource [] audios;
	public AudioSource idleMusic;
	public AudioSource jump1;
	public AudioSource jump2;
	public AudioSource dive1;
	public AudioSource dive2;
	public AudioSource hit1;
	public AudioSource gun1;
	// Use this for initialization
	void Start () {
		audios = GetComponents<AudioSource> ();
		idleMusic = audios [0];
		jump1 = audios [1];
		jump2 = audios [2];
		dive1 = audios [3];
		dive2 = audios [4];
		hit1 = audios [5];
		gun1 = audios [6];
	}

	public void playSound(AudioSource clip){
		if (canPlaySound) {
			clip.Play ();
		}

	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i <=6; i++) {
			if(audios[i].isPlaying == true){
				canPlaySound = false;
			}else{
				canPlaySound = true;
			}
		}
	}
}
