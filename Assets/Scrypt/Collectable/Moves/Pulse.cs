using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	public float rate;
	public float maxScale;
	public float minScale;

	void Update () {
		PulseObject ();
	}

	private void PulseObject ()
	{		
//		float rate = .5f;
//		float maxScale = 1.2f;
//		float minScale = .8f;

		float scale = (Mathf.Sin(Time.time * (rate * 2 * Mathf.PI)) + 1f)/2f;
		
		scale = Mathf.Lerp (minScale, maxScale, scale);
		
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
