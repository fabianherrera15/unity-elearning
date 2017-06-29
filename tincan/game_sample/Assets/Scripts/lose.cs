using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class lose : MonoBehaviour {

	void OnGUI(){
		GUI.Label (new Rect (10, 10, 100, 40), "You lose!");
		
		if (GUI.Button (new Rect (10, 60, 90, 60), "Restart Game")) {
			SceneManager.LoadScene("sceneLevel1");
		}
		if (GUI.Button (new Rect (10, 1300, 90, 60), "Exit Game")) {
			Application.Quit();
		}
	}
}
