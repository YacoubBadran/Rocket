using UnityEngine;
using System.Collections;

// keep the speed at limit max, to be sure that boxes have been set inActive

public class Destroy : MonoBehaviour {
	
	void OnTriggerEnter (Collider other)
	{

		other.gameObject.SetActive(false);


//		GameObject GO = other.gameObject;
//		if(GO.tag.Equals("Barrier"))
//		{
//			Debug.Log("Barrier");
//			GO.SetActive(false);
//		}
//		else
//		{
//			Debug.Log("Not Barrier");
//			Destroy(GO);
//		}
	}
}
