using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	private float cloudYmin = 7.5f;
	private float cloudYmax = 12.5f;
	private float cloudSpeed = 1;
	public Transform cloud1Target;
	public Transform cloud2Target;
	// Use this for initialization
	void Start () {
		cloudSpeed = Random.Range (0.005f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (cloudSpeed, 0, 0);

		if (transform.position.x < cloud1Target.position.x) {
			swapCloud ();
			cloudSpeed = Mathf.Abs (cloudSpeed);

		} else if (transform.position.x > cloud2Target.position.x) {
			swapCloud ();
			cloudSpeed = -1 * (Mathf.Abs (cloudSpeed));

		}
	}

	public void swapCloud(){
		transform.position = new Vector3 (transform.position.x ,Random.Range (cloudYmin, cloudYmax), 0);
		cloudSpeed = Random.Range (0.005f, 0.01f);


	}
}
