using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {
	public string tagName = "";
	public float rayDistance = 0f; 
	public int score = 0;
	public float gameTime = 20.0f;
	public float loadWaitTime = 3.0f;
	public int numberOfPointsToWin = 5;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Countdown", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, rayDistance)){
				if(hit.transform.tag == tagName){
					enemy scriptEnemy = hit.transform.GetComponent<enemy>();
					scriptEnemy.numberOfCLicks--;
					if(scriptEnemy.numberOfCLicks == 0){
						score += scriptEnemy.enemyPoints;
					}
				}
				if (hit.transform.tag == "coin") {
					hit.transform.GetComponent<InteractiveObj> ().OnCloseEnough.HandleInteraction(gameObject);
				}
			}
		}
	}

	void Countdown(){
		if (gameTime != 0) {
			gameTime--;
		} else {
			CancelInvoke("Countdown");
			//yield return new WaitForSeconds(loadWaitTime);
			if(score >= numberOfPointsToWin){
				SceneManager.LoadScene("sceneWin");
			}else{
				SceneManager.LoadScene("sceneLose");
			}

		}

	}
	void OnGUI(){
		GUI.Label (new Rect (10, 10, 100, 20), "Score: " + score);
		GUI.Label (new Rect (10, 25, 100, 35), "Time: " + gameTime);
	}
}
