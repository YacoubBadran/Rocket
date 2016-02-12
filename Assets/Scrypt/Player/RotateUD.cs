using UnityEngine;
using System.Collections;

public class RotateUD : MonoBehaviour {

	private GameObject Player;
	private Rigidbody rb;

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		rb = Player.GetComponent<Rigidbody>();
	}

//	void Update () {
//		rb.AddTorque(transform.up * 100.0f);
//		if(Input.GetKey("r"))
//		{
////			for(float i = 0.0f; i <= 1.0f; i += 0.001f)
////				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(45.0f, 0.0f, 0.0f), i);
//
//			this.transform.Rotate(transform.right, 7f, Space.Self);
////			rb.AddTorque(transform.up * 100.0f);
//		}
//	}
}
