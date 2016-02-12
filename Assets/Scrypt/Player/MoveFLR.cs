using UnityEngine;
using System.Collections;

public class MoveFLR : MonoBehaviour {

	private Ground Ground;
	private Rigidbody rb;

	private float xBound;
	private float yBound;
	private float mid;

	public float timeToIncreaseSpead;
	private float dTimeToIncreaseSpead;
	private int nextIncrease = 1;
	private int pos; // Be sure that Player in the way, and not slide>>> -1, 0, 1

	private int dificultySpeed; //       ..............       in player class

	private Vector2 startPosTouch;
	private bool direction; // true = right, false = left
	public bool directionChosen; // true = move, false = don't move

	void Start () 
	{
		rb = this.GetComponent<Rigidbody>();

		Ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();
		xBound = Ground.GetXBound();
		yBound = Ground.GetYBound();
		mid = Ground.GetMid();
		pos = 0;

		this.transform.position = new Vector3(xBound * mid, yBound + 0.5f, 3.0f); // delete "yBound + 4" and put "yBound"

		dTimeToIncreaseSpead = Time.time;

		rb.velocity = new Vector3(0.0f, 0.0f, 5.0f);

		directionChosen = false;
	}

	void Update () 
	{
		/* touch right and left screen */

//		if (Input.touchCount == 1)
//		{
//			var touch = Input.touches[0];
//			if (touch.position.x < Screen.width/2)
//			{
//							pos--;
//							float min = (mid + pos + 1) * xBound;
//							float max = (mid + pos) * xBound;
//							float y = this.transform.position.y; 
//							float z = this.transform.position.z;
//				
//							for(float i = 0.0f; i <= 1.0f; i += 0.1f)
//							{
//								this.transform.position = new Vector3(Mathf.Lerp(min, max, i), y, z);
//							}
//			}
//			else if (touch.position.x > Screen.width/2)
//			{
//							pos++;
//							float min = (mid + pos - 1) * xBound;
//							float max = (mid + pos) * xBound;
//							float y = this.transform.position.y; 
//							float z = this.transform.position.z;
//				
//							for(float i = 0.0f; i <= 1.0f; i += 0.1f)
//								this.transform.position = new Vector3(Mathf.Lerp(min, max, i), y, z);
//			}
//		}


		/* touch to right and left */

//		if (Input.touchCount > 0) {
//			var touch = Input.GetTouch(0);
//
//			switch (touch.phase) {
//				// Record initial touch position.
//			case TouchPhase.Began:
//				startPosTouch = touch.position;
//				break;
//				
//				// Determine direction by comparing the current touch position with the initial one.
//				// direction is ( true = right, false = left )
//			case TouchPhase.Moved:
//				if(touch.position.x > startPosTouch.x)
//					direction = true;
//				else
//					direction = false;
//				break;
//				
//				// Report that a direction has been chosen when the finger is lifted.
//			case TouchPhase.Ended:
//				directionChosen = true;
//				break;
//			}
//		}
//
//		if(directionChosen)
//		{
//			directionChosen = false;
//
//			if(direction) // true = right
//			{
//				pos++;
//				float min = (mid + pos - 1) * xBound;
//				float max = (mid + pos) * xBound;
//				float y = this.transform.position.y; 
//				float z = this.transform.position.z;
//				
//				for(float i = 0.0f; i <= 1.0f; i += 0.1f)
//					this.transform.position = new Vector3(Mathf.Lerp(min, max, i), y, z);
//			}
//			else{ // false = left
//				pos--;
//				float min = (mid + pos + 1) * xBound;
//				float max = (mid + pos) * xBound;
//				float y = this.transform.position.y; 
//				float z = this.transform.position.z;
//				
//				for(float i = 0.0f; i <= 1.0f; i += 0.1f)
//				{
//					this.transform.position = new Vector3(Mathf.Lerp(min, max, i), y, z);
//				}
//				
//
//			}
//		}

		if(Input.GetKeyDown("w"))
		{
			rb.AddForce(new Vector3(0.0f, 0.0f, 1000.0f) * Time.deltaTime);
//			this.transform.Rotate(transform.right, 10f, Space.Self);
		}

		if(Input.GetKeyDown("a"))
		{
			pos--;
			float min = (mid + pos + 1) * xBound;
			float max = (mid + pos) * xBound;
			float y = this.transform.position.y; 
			           /*	make it soft in the future... 
			            * 	. 
			            *   . .
			            * 	  .
			            * */
			float z = this.transform.position.z;

			for(float i = 0.0f; i <= 1.0f; i += 0.1f)
			{
				this.transform.position = new Vector3(Mathf.Lerp(min, max, i), y, z);
//				this.transform.rotation = new Vector3();
//				this.transform.Rotate
			}

		 }

		if(Input.GetKeyDown("d"))
		{
			pos++;
			float min = (mid + pos - 1) * xBound;
			float max = (mid + pos) * xBound;
			float y = this.transform.position.y; 
				       /*	make it soft in the future... 
			            * 	. 
			            *   . .
			            * 	  .
			            * */
			float z = this.transform.position.z;

			for(float i = 0.0f; i <= 1.0f; i += 0.1f)
				this.transform.position = new Vector3(Mathf.Lerp(min, max, i), y, z);
//			rb.AddForce(new Vector3(500.0f, 0.0f, 0.0f) * Time.deltaTime);
		}

		UpdateSpead ();
	}

	void UpdateSpead ()
	{
		if(timeToIncreaseSpead < Time.time - dTimeToIncreaseSpead && Time.time < 400)
		{
			switch(nextIncrease) // make function versus time is better
			{
			case 1: case 3: case 7: case 10: case 11: SetSpeed(1f);
				nextIncrease++;
				break;
				
			case 2: case 5: case 6: case 8: case 12: SetSpeed(1.5f);
				nextIncrease++;
				break;
				
			case 4: case 9: SetSpeed(2f);
				nextIncrease++;
				break;
				
			case 13: SetSpeed(1f);
				nextIncrease = 1;
				break;
				
			default:
				Debug.Log("bug: nextIncrease = " + nextIncrease + "     and must be between 1-13");
				break;
			}
		}	
	}
	
	void SetSpeed (float speed)
	{
		rb.velocity = rb.velocity + new Vector3(0.0f, 0.0f, speed);
		dTimeToIncreaseSpead = Time.time;
	}

}
