using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float speed;
	
	void Update () {
		this.transform.Rotate(transform.up, speed);
	}
}
