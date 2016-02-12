using UnityEngine;
using System.Collections;

public class CreateCollectable : MonoBehaviour {

	private GameObject[] collectable;
	private Ground Ground;

	private int mid;
	private float xBound;
	private float yBound;
	private float zBound;

	void Start () {
		Ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();

		LoadCollectable ();

		mid = Ground.GetMid();
		xBound = Ground.GetXBound();
		yBound = Ground.GetYBound();
		zBound = Ground.GetZBound();
	}

	void Update () {
	
	}

	void LoadCollectable () /////// >>.............................................................................. buy it...buy it..buy it..buy it...buy it.
	{
		collectable = new GameObject[1];
		
		collectable[0] = (GameObject)Resources.Load("Collectable/Sphere"); 
	}
}
