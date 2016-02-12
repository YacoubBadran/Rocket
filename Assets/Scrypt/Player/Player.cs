using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private int score;

	public AudioSource explosionPlayer;

	private Text scoreText;

 	void Start()
	{
		score = PlayerPrefs.GetInt("score", 0);

		scoreText = GameObject.Find("Score").GetComponent<Text>();
	}

	void FixedUpdate ()
	{	// ask yahya about rotation, torque, world space, self space, local space, and the question pellow
		this.transform.Rotate(transform.forward, 8f, Space.World); // why "world" is better  >>> i claim to be "self" is better
	}

	public void IncreaseScore(int amount)
	{
		score += amount;
		PlayerPrefs.SetInt("score", score);

		scoreText.text = "Score : " + score;

		Debug.Log("score: " + PlayerPrefs.GetInt("score", 0));
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag.Equals("Barrier"))
		{
			explosionPlayer.Play(); // Sound with explosion... the problem is the destroy of the player, that make bugs and the game will be currupted
//     		Destroy(this.gameObject);

			StartCoroutine(Wait(0.5f));
		}
	}

	IEnumerator Wait (float time)
	{
		yield return new WaitForSeconds(time);

		Application.LoadLevel("MainMenue");
	}
	/*

	randCaseSpeed = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};

	IEnumerator WaitTime (float time)
	{
		yield return new WaitForSeconds(time);

		int randCase = (int)(Random.Range(0, randCaseSpeed.GetLength(0) - 1));

		time += 2 * randCase + 65;

		switch(randCase) // make function versus time is better
		{
		case 1: Speed(0.1f);
			break;
		case 2: Speed(0.15f);
			break;
		case 3: Speed(0.1f);
			break;
		case 4: Speed(0.2f);
			break;
		case 5: Speed(0.15f);
			break;
		case 6: Speed(0.15f);
			break;
		case 7: Speed(0.1f);
			break;
		case 8: Speed(0.15f);
			break;
		case 9: Speed(0.2f);
			break;
		case 10: Speed(0.1f);
			break;
		case 11: Speed(0.1f);
			break;
		case 12: Speed(0.15f);
			break;
		case 13: Speed(0.2f); // the final(max) player speed
			break;
			
		default:
			break;
		}

		StartCoroutine(WaitTime(3));
	}
	*/
	
	
	
	/*
	IEnumerator WaitTime (float time)
	{	
		if(dificultySpeed < 0)
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultySpeed += 1;
			StartCoroutine(WaitTime(time));
		}
		else
			if(dificultySpeed <= -10)
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultySpeed += 2;
			StartCoroutine(WaitTime(time));
		}
		else
			if(dificultySpeed <= -30) 
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultySpeed += 3;
			StartCoroutine(WaitTime(time));
		}
		else
			if(dificultySpeed <= -40)
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultySpeed += 4;
			StartCoroutine(WaitTime(time));
		}
	}
	*/


	/*
	void UpdateSpead ()
	{
		if(timeToIncreaseSpead < Time.time - dTimeToIncreaseSpead)
		{
			switch((int)(Time.time)) // make function versus time is better
			{
			case 7: Speed(0.1f);
				break;
			case 15: Speed(0.15f);
				break;
			case 30: Speed(0.1f);
				break;
			case 55: Speed(0.2f);
				break;
			case 95: Speed(0.15f);
				break;
			case 160: Speed(0.15f);
				break;
			case 230: Speed(0.1f);
				break;
			case 300: Speed(0.15f);
				break;
			case 390: Speed(0.2f);
				break;
			case 500: Speed(0.1f);
				break;
			case 600: Speed(0.1f);
				break;
			case 700: Speed(0.15f);
				break;
			case 900: Speed(0.2f); // the final(max) player speed
				break;
				
			default:
				break;
			}

		}

	}
*/






}
