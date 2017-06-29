using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	public Color[] shapeColor = new Color[0];
	public float respawnWaitTime = 2.0f;
	public int numberOfCLicks = 2;
	public Transform explosion;
	private int storeClicks = 0;
	public int enemyPoints = 1;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		storeClicks = numberOfCLicks;
		Vector3 position = new Vector3(Random.Range(-6, 6), Random.Range(-4, 4), 0);
		transform.position = position;
		RandomColor ();
		audioSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (numberOfCLicks <= 0) {
			if(explosion){
				Instantiate(explosion, transform.position, transform.rotation);
			}
			if(audioSource.clip){
				audioSource.Play();
			}
			Vector3 position = new Vector3(Random.Range(-6, 6), Random.Range(-4, 4), 0);
			StartCoroutine(RespawnWaitTime());
			transform.position = position;
			numberOfCLicks = storeClicks;
		}
	
	}

	IEnumerator RespawnWaitTime(){
		Renderer renderer = GetComponent<Renderer> ();
		renderer.enabled = false;
		RandomColor ();
		yield return new WaitForSeconds(respawnWaitTime);
		renderer.enabled = true;
	}

	void RandomColor(){
		if (shapeColor.Length > 0){
			int newColor = Random.Range (0, shapeColor.Length);
			GetComponent<Renderer> ().material.color = shapeColor[newColor];
		}
	}
}
