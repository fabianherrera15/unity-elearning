using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {

	void OnGUI(){
		GUI.Label (new Rect (10, 10, 100, 40), "You win!");

		if (GUI.Button (new Rect (10, 60, 90, 50), "Start Game")) {
			SceneManager.LoadScene("sceneLevel1");
		}
		if (GUI.Button (new Rect (10, 130, 90, 50), "Exit Game")) {
			Application.Quit();
		}
	}
}
