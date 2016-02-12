using UnityEngine;
using System.Collections;

public class MainMenueGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUILayout.BeginArea( new Rect( Screen.width / 2 - 100, Screen.height / 2 * 100, 200, 200));
		GUILayout.FlexibleSpace ();

		if(GUILayout.Button("Play")) {
			StartGame ();
		}

		GUILayout.FlexibleSpace ();
		GUILayout.EndArea();
	}

	void StartGame () {
		Application.LoadLevel("Main");
	}
}
