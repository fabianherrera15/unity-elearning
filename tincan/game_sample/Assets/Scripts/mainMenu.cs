using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {
	
	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 90, 50), "Start Game")) {
			SceneManager.LoadScene("sceneLevel1");
		}
		if (GUI.Button (new Rect (10, 70, 90, 50), "Exit Game")) {
			Application.Quit();
		}
	}
}
