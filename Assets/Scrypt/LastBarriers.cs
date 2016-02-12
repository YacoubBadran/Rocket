using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LastBarriers : MonoBehaviour {

	private GameObject Player;
	private GameObject Barrier;
	private Ground Ground;
	private List<GameObject> bars;

	private GameObject[] boxes;
	private int mid;
	private float xBound;
	private float yBound;
	private float zBound;
	private float finalHight;

	private int[] barriersNum;
	private int[] noBarriersNum;

	private int hightOfBarrier; // "hightOfBarrier > hightOfBarrier_atLeast"
	private int hightOfBarrier_atLeast; // fixed every time

	public int dificultyBar;
	public float timeToIncreaseBarrierDeffeculty;

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		Barrier = new GameObject("Barrier");
		Barrier.tag = "Barrier";
		Ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();

		boxes = Ground.GetBoxes();
		mid = Ground.GetMid();
		xBound = Ground.GetXBound();
		yBound = Ground.GetYBound();
		zBound = Ground.GetZBound();

		hightOfBarrier_atLeast = Ground.hight * 3 + 20;
		finalHight = 0;

		bars = new List<GameObject>();

		InstantiateBoxesForBarriers();

		barriersNum = new int[] {10, 10, 1, 1, 2, 2, 3, 3, 3, 4, 5, 6, 7, 8}; // 10 means difference in length of rows
		noBarriersNum = new int[] {1, 1, 2, 3, 3, 3, 4, 5, 5, 5, 6, 6, 7, 8, 9, 10, 14}; // if appear 10

		StartCoroutine(WaitTime(timeToIncreaseBarrierDeffeculty)); // increase difficulty for barrier
	}

	void Update () {

		if(hightOfBarrier < hightOfBarrier_atLeast && Time.time > 2)
			InstatiateBarriers ();
	}

	IEnumerator WaitTime (float time)
	{	
		Debug.Log(time);
//		if(dificultyBar < 0)
//		{
//			time = (int)(time * 1.6);
//			yield return new WaitForSeconds(time);
//			dificultyBar += 1;
//			StartCoroutine(WaitTime(time));
//		}
//		else
		if(dificultyBar <= -10)
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultyBar += 2;
			StartCoroutine(WaitTime(time));
		}
		else
		if(dificultyBar <= -30) 
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultyBar += 3;
			StartCoroutine(WaitTime(time));
		}
		else
		if(dificultyBar <= -40)
		{
			time = (int)(time * 1.6);
			yield return new WaitForSeconds(time);
			dificultyBar += 4;
			StartCoroutine(WaitTime(time));
		}

	}



	void InstatiateBarriers ()
	{
		/*	Shapes:
		 * 1. x
		 * 
		 * 2. x x
		 * 
		 * 3. x
		 *     x
		 *      x
		 * 
		 * 4.   x
		 *     x
		 *    x
		 **/

		int randLengthOfRow = Random.Range(dificultyBar, barriersNum.GetLength(0) - 1);
		int shapes = Random.Range(1, 4);

		if(randLengthOfRow >= 0)
		{
			int lengthOfRow = barriersNum[randLengthOfRow];
			switch(shapes)    // i need levels of defeculty and affect to rach of these
			{
			case 1:
				int column = Random.Range(1, 3);
				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(column);
					finalHight++;
					hightOfBarrier++;
				}

				finalHight++;
				hightOfBarrier++;
				break;

			case 2: 
				int locationOfSpace = Random.Range(1, 3);

				for(int i = 0; i < lengthOfRow; i++)
					{
					if(locationOfSpace == 1)
					{
						InstantiateBarrier(2);
						InstantiateBarrier(3);
					}
					else if(locationOfSpace == 2)
					{
						InstantiateBarrier(1);
						InstantiateBarrier(3);
					}
					else {
						InstantiateBarrier(1);
						InstantiateBarrier(2);
					}
					finalHight++;
					hightOfBarrier++;
				}

				finalHight++;
				hightOfBarrier++;
				break;

			case 3: 
				int numBlank11 = Random.Range(1, 3);

				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(3);
					finalHight++;// make blank line
					hightOfBarrier++;
				}

				if(numBlank11 == 1 || numBlank11 == 2 || numBlank11 == 3)
				{
					finalHight++;// make blank line
					hightOfBarrier++;
				}
				
				
				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(2);
					finalHight++;
					hightOfBarrier++;
				}

				if(numBlank11 == 1 || numBlank11 == 2 || numBlank11 == 4)
				{
					finalHight++;
					hightOfBarrier++;
				}
				
				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(1);
					finalHight++;
					hightOfBarrier++;
				}

				finalHight++;
				hightOfBarrier++;
				break;

			case 4: 
				int numBlank1 = Random.Range(1, 3);

				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(1);
					finalHight++;
					hightOfBarrier++;
				}

				if(numBlank1 == 1 || numBlank1 == 2 || numBlank1 == 3)
				{
					finalHight++;
					hightOfBarrier++;
				}

				
				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(1);
					finalHight++;
					hightOfBarrier++;
				}

				if(numBlank1 == 1 || numBlank1 == 2 || numBlank1 == 4)
				{
					finalHight++;
					hightOfBarrier++;
				}
				
				for(int i = 0; i < lengthOfRow; i++)
				{
					InstantiateBarrier(3);
					finalHight++;
					hightOfBarrier++;
				}

				finalHight++;
				hightOfBarrier++;
				break;

			default:
				break;
			}
		}
		else // dont create Barriers
		{
			int randLengthOfNon = Random.Range(0, noBarriersNum.GetLength(0) - 1);
			finalHight += randLengthOfNon;
			hightOfBarrier += randLengthOfNon;
		}
	}

	void InstantiateBarrier (int x) // transfer from last to next
	{
		for(int i = 0; i < bars.Count; i++)
		{
			if(!bars[i].activeInHierarchy)
			{
				bars[i].transform.position = new Vector3((mid + x - 2) * xBound, yBound, finalHight * zBound);
				bars[i].SetActive(true);
				return;
			}
		}

		GameObject obj = Instantiate(boxes[0], new Vector3 ((mid + x - 2) * xBound, yBound, finalHight * zBound), Quaternion.identity) as GameObject;
		bars.Add (obj);
		obj.transform.SetParent(Barrier.transform, true);
	}

	void InstantiateBoxesForBarriers ()
	{
		int num = 2 * Ground.hight * 3 + 20; // 2 (right and left barrier)
		for(int i = 0; i < num; i++)
		{
			GameObject obj = Instantiate(boxes[0], new Vector3 (-15.0f, -15.0f, -15.0f), Quaternion.identity) as GameObject;
			obj.SetActive(false);
			bars.Add (obj);
			obj.transform.SetParent(Barrier.transform, false);
		}
	}

	public void MinimiseHightOfBarrier (int minus)
	{
		hightOfBarrier -= minus;
	}
}
