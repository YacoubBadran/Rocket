using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// error in collectable position that make the collectable disappear and increase score
// make array of barrier
public class Barriers : MonoBehaviour {
	
	private GameObject Player;
	private GameObject Barrier; // the parent of all barrier
	private GameObject Collectables; // the parent of all collectable
	private GameObject[] collectable;
	private Ground Ground;
	private List<GameObject> bars1;
	private List<GameObject> bars2;
	private List<GameObject> bars3;

	private GameObject[] boxes;
	private int mid;
	private float xBound;
	private float yBound;
	private float zBound;
	private int finalHight;
	
	private int[] barriersNum;
	private int[] collectableNumIfNoBarrier;
	private int[] style;
	
	private int hightOfBarrier; // "hightOfBarrier > hightOfBarrier_atLeast"
	private int hightOfBarrier_atLeast; // fixed every time
	
	public int dificultyBar;
	public float timeToIncreaseBarrierDeffeculty;

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		Barrier = new GameObject("Barrier");
		Barrier.tag = "Barrier";
		Collectables = new GameObject("Collectables");
		Collectables.tag = "Collectable";
		Ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();

		LoadCollectable ();

		boxes = Ground.GetBoxes();
		mid = Ground.GetMid();
		xBound = Ground.GetXBound();
		yBound = Ground.GetYBound();
		zBound = Ground.GetZBound();
		
		hightOfBarrier_atLeast = Ground.hight * 3 + 20;
		finalHight = 0;
		
		int lastIndex = boxes.GetLength(0) - 1;
		style = new int[] {lastIndex - 2, lastIndex - 1, lastIndex};
		bars1 = new List<GameObject>();
		bars2 = new List<GameObject>();
		bars3 = new List<GameObject>();

		InstantiateBoxesForBarriers();
		
		barriersNum = new int[] {10, 10, 1, 1, 2, 2, 3, 3, 3, 4, 5, 6, 7, 8};
		collectableNumIfNoBarrier = new int[] {1, 2, 3, 3, 3, 4, 5, 5, 5, 6, 6, 7, 8, 9, 10};
		
		StartCoroutine(WaitTime(timeToIncreaseBarrierDeffeculty)); // increase difficulty for barrier
	}
	
	void Update () {
		if(hightOfBarrier < hightOfBarrier_atLeast) // && Time.time > 2.0f
			PutBarriers ();
	}
	
	IEnumerator WaitTime (float time)
	{	
		if(dificultyBar < -50)
		{
		yield return new WaitForSeconds(time);
		dificultyBar /= 2;
		StartCoroutine(WaitTime(time));
		}
//		//		if(dificultyBar < 0)
//		//		{
//		//			time = (int)(time * 1.6);
//		//			yield return new WaitForSeconds(time);
//		//			dificultyBar += 1;
//		//			StartCoroutine(WaitTime(time));
//		//		}
//		//		else
//		if(dificultyBar <= -10)
//		{
//			time = (int)(time * 1.6);
//			yield return new WaitForSeconds(time);
//			dificultyBar += 2;
//			StartCoroutine(WaitTime(time));
//		}
//		else
//			if(dificultyBar <= -30) 
//		{
//			time = (int)(time * 1.6);
//			yield return new WaitForSeconds(time);
//			dificultyBar += 3;
//			StartCoroutine(WaitTime(time));
//		}
//		else
//			if(dificultyBar <= -40)
//		{
//			time = (int)(time * 1.6);
//			yield return new WaitForSeconds(time);
//			dificultyBar += 4;
//			StartCoroutine(WaitTime(time));
//		}
	}
	
	void PutBarriers ()
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
		
		int randIndexLengthOfRow = Random.Range(dificultyBar, barriersNum.GetLength(0) - 1);
		int shapes = (int)(Random.Range(1, 5)); // 5 not 4, because the last num difficult to reach it
		
		if(randIndexLengthOfRow >= 0)
		{
			int lengthOfRow = barriersNum[randIndexLengthOfRow];
			switch(shapes)    // i need levels of defeculty and affect to rach of these
			{
			case 0:
				int shape = (int)(Random.Range(2, 4));
				int randIndexLengthOfRow2 = Random.Range(dificultyBar, barriersNum.GetLength(0) - 1);
				int lengthOfRow2 = barriersNum[randIndexLengthOfRow2];
				int randIndexLengthOfRow3 = Random.Range(dificultyBar, barriersNum.GetLength(0) - 1);
				int lengthOfRow3 = barriersNum[randIndexLengthOfRow3];

				if(shape == 2)
					shape_2(lengthOfRow, lengthOfRow2);
				else
					if(shape == 3)
						shape_3(lengthOfRow, lengthOfRow2, lengthOfRow3);
				else
						shape_4(lengthOfRow, lengthOfRow2, lengthOfRow3);
				break;
			
			case 1:
				shape_1(lengthOfRow);
				break;
				
			case 2: 
				shape_2(lengthOfRow, lengthOfRow);
				break;
				
			case 3: 
				shape_3(lengthOfRow, lengthOfRow, lengthOfRow);
				break;
				
			case 4: 
				shape_4(lengthOfRow, lengthOfRow, lengthOfRow);
				break;

			default:
				break;
			}
		}
		else // dont create Barriers & may create collectable
		{
			int creatOrNot_collectable = (int)(Random.Range(0, 2)); // 2 not 1, because the last num difficult to reach it
			int randLengthOfNon = Random.Range(0, collectableNumIfNoBarrier.GetLength(0) - 1);
			int collectableNum = collectableNumIfNoBarrier[randLengthOfNon];

			if(creatOrNot_collectable == 1)
			{
				CreateCollectable(new int[]{0}, collectableNum);
				finalHight += collectableNum;
				hightOfBarrier += collectableNum;
			}
			else
			{
				finalHight += collectableNum;
				hightOfBarrier += collectableNum;
			}
		}
	}
	
	void PutBarriers (int way) // transfer from last to next
	{
		List<GameObject> bars = bars1;
		switch(way)
		{
		case 2: bars = bars2; break;
		case 3: bars = bars3; break;
		}

		for(int i = 0; i < bars.Count; i++)
		{
			if(!bars[i].activeInHierarchy)
			{
				bars[i].transform.position = new Vector3((mid + way - 2) * xBound, yBound, finalHight * zBound);
				bars[i].SetActive(true);
				return;
			}
		}

		Debug.Log("Make a new obj. for style" + way);
		GameObject obj = Instantiate(boxes[style[way - 1]], new Vector3 ((mid + way - 2) * xBound, yBound, finalHight * zBound), Quaternion.identity) as GameObject;
		bars.Add (obj);
		obj.tag = "Barrier";
		obj.transform.SetParent(Barrier.transform, true);
	}

	private void shape_1 (int lengthOfRow)
	{
		int column = Random.Range(1, 3);
		CreateCollectable(new int[]{column}, lengthOfRow);
		for(int i = 0; i < lengthOfRow; i++)
		{
			PutBarriers(column);
			finalHight++;
			hightOfBarrier++;
		}
		
		finalHight++;
		hightOfBarrier++;
	}

	private void shape_2 (int lengthOfRow, int lengthOfRow2)
	{
		int locationOfSpace = Random.Range(1, 3);

		if(locationOfSpace == 1)
		{
			CreateCollectable(new int[]{2, 3}, lengthOfRow);
		}
		else if(locationOfSpace == 2)
		{
			CreateCollectable(new int[]{1, 3}, lengthOfRow);	
		}
		else {
			CreateCollectable(new int[]{1, 2}, lengthOfRow2);
		}
		
		for(int i = 0; i < lengthOfRow; i++)
		{
			if(locationOfSpace == 1)
			{
				PutBarriers(2);
				PutBarriers(3);
			}
			else if(locationOfSpace == 2)
			{
				PutBarriers(1);
				PutBarriers(3);
			}
			else {
				PutBarriers(1);
				PutBarriers(2);
			}
			finalHight++;
			hightOfBarrier++;
		}
		
		finalHight++;
		hightOfBarrier++;
	}

	private void shape_3 (int lengthOfRow, int lengthOfRow2, int lengthOfRow3)
	{
		int numBlank11 = Random.Range(1, 3);
		CreateCollectable(new int[]{3}, lengthOfRow);
		
		for(int i = 0; i < lengthOfRow; i++)
		{
			PutBarriers(3);
			finalHight++;// make blank line
			hightOfBarrier++;
		}
		
		if(numBlank11 == 1 || numBlank11 == 2 || numBlank11 == 3)
		{
			CreateCollectable(new int[]{0}, 1);
			finalHight++;// make blank line
			hightOfBarrier++;
		}
		
		CreateCollectable(new int[]{2}, lengthOfRow);
		for(int i = 0; i < lengthOfRow2; i++)
		{
			PutBarriers(2);
			finalHight++;
			hightOfBarrier++;
		}
		
		if(numBlank11 == 1 || numBlank11 == 2 || numBlank11 == 4)
		{
			CreateCollectable(new int[]{0}, 1);
			finalHight++;
			hightOfBarrier++;
		}
		
		CreateCollectable(new int[]{1}, lengthOfRow);
		for(int i = 0; i < lengthOfRow3; i++)
		{
			PutBarriers(1);
			finalHight++;
			hightOfBarrier++;
		}
		
		finalHight++;
		hightOfBarrier++;
	}

	private void shape_4 (int lengthOfRow, int lengthOfRow2, int lengthOfRow3)
	{
		int numBlank1 = Random.Range(1, 3);
		
		CreateCollectable(new int[]{1}, lengthOfRow);
		for(int i = 0; i < lengthOfRow; i++)
		{
			PutBarriers(1);
			finalHight++;
			hightOfBarrier++;
		}
		
		if(numBlank1 == 1 || numBlank1 == 2 || numBlank1 == 3)
		{
			CreateCollectable(new int[]{0}, 1);
			finalHight++;
			hightOfBarrier++;
		}
		
		CreateCollectable(new int[]{2}, lengthOfRow);
		for(int i = 0; i < lengthOfRow2; i++)
		{
			PutBarriers(2);
			finalHight++;
			hightOfBarrier++;
		}
		
		if(numBlank1 == 1 || numBlank1 == 2 || numBlank1 == 4)
		{
			CreateCollectable(new int[]{0}, 1);
			finalHight++;
			hightOfBarrier++;
		}
		
		CreateCollectable(new int[]{3}, lengthOfRow);
		for(int i = 0; i < lengthOfRow3; i++)
		{
			PutBarriers(3);
			finalHight++;
			hightOfBarrier++;
		}
		
		finalHight++;
		hightOfBarrier++;
	}

	void InstantiateBoxesForBarriers ()
	{
		InstantiateBoxesForBarriers(bars1, 0);
		InstantiateBoxesForBarriers(bars2, 1);
		InstantiateBoxesForBarriers(bars3, 2);
	}

	void InstantiateBoxesForBarriers (List<GameObject> bars, int way) // 3 ways to move
	{
// num of barriar for each style and way
		int num = Ground.hight * 2;
		for(int i = 0; i < num; i++)
		{
			GameObject obj = Instantiate(boxes[style[way]], new Vector3 (-15.0f, -15.0f, -15.0f), Quaternion.identity) as GameObject;
			obj.SetActive(false);
			obj.tag = "Barrier";
			bars.Add (obj);
			obj.transform.SetParent(Barrier.transform, false);
		}
	}

	public void MinimiseHightOfBarrier (int minus)
	{
		hightOfBarrier -= minus;
	}



	void CreateCollectable (int[] fullWays, int length)
	{
		int num = Random.Range(1, 2);
		bool instantiate = (num == 1? true : false);

		if(instantiate)
		{
			if(fullWays.GetLength(0) == 2) // 2 way full
			{
				int blankWay = 1;
				if((fullWays[0] == 1 && fullWays[1] == 3) || (fullWays[0] == 3 && fullWays[1] == 1))
					blankWay = 2;
				else
					if((fullWays[0] == 1 && fullWays[1] == 2) || (fullWays[0] == 2 && fullWays[1] == 1))
						blankWay = 3;
					
					float x = (mid + blankWay - 2) * xBound;

					InstantiateCollectable(x, yBound, finalHight, length, collectable[0]);
			}
			else 
				if(fullWays[0] == 0) // 0 way full
			{
				int[] ways = new int[]{Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2)};
				for(int i = 0; i < ways.GetLength(0); i++)
				{
					if(ways[i] >= 1)
					{
						float x = (mid + i - 1) * xBound;
						InstantiateCollectable(x, yBound, finalHight, length, collectable[0]);
					}
				}
			}
//***what is that?????
			else // 1 way full
			{
				float x1 = 2;
				float x2 = 3;

				if(fullWays[0] == 2)
					x1 = 1;
				else
					if(fullWays[0] == 3)
					{
						x1 = 1;
						x2 = 2;
					}

				int ways = Random.Range(1, 3); // if 1 then x1 & if 2 then x2 & if 3 then x1 + x2
				if(ways == 1)
				{
					x1 = (mid + x1 - 2) * xBound;
					InstantiateCollectable(x1, yBound, finalHight, length, collectable[0]);
				}
				else
					if(ways == 2)
					{
						x2 = (mid + x2 - 2) * xBound;
						InstantiateCollectable(x2, yBound, finalHight, length, collectable[0]);
					}
				else
					if(ways == 3)
					{
						x1 = (mid + x1 - 2) * xBound;
						x2 = (mid + x2 - 2) * xBound;
						InstantiateCollectable(x1, yBound, finalHight, length, collectable[0]);
						InstantiateCollectable(x2, yBound, finalHight, length, collectable[0]);
					}
			}
		}
	}

	void InstantiateCollectable (float x, float y, int zNum, int collectableNum, GameObject Collectable)
	{
		for(int i = 0; i < collectableNum; i++)
		{
			GameObject obj = Instantiate(Collectable, new Vector3(x, y * 2, zNum * zBound), Quaternion.identity) as GameObject; //// ********** must be "y" not "y * 2" why ?????
			obj.tag = "Collectable";
			obj.transform.SetParent(Collectables.transform, false);
			zNum++;

			Pulse p = obj.AddComponent<Pulse>();
			p.rate = 1.5f;
			p.maxScale = 1.0f;
			p.minScale = 0.8f;
			Rotate r = obj.AddComponent<Rotate>();
			r.speed = 8.0f;
			obj.AddComponent<Score2>();
		}
	}

	void LoadCollectable ()
	{
		collectable = new GameObject[1];
		
		collectable[0] = (GameObject)Resources.Load("Collectable/Sphere"); 
	}
}
