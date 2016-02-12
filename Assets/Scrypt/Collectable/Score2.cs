using UnityEngine;
using System.Collections;

public class Score2 : MonoBehaviour {

	private static Player Player;
	
	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void OnTriggerEnter (Collider other)
	{
		gameObject.SetActive(false);

		// make sound

		if(other.gameObject.tag.Equals("Player"))
			Player.IncreaseScore(2);
	}
}
