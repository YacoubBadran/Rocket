using UnityEngine;
using System.Collections;

public class LastGround : MonoBehaviour {

	private GameObject Player;
	private Rigidbody rb;
	private GameObject[] boxes;

	private GameObject[] groundBoxes;
	private GameObject[] wallBoxes;
	private GameObject[] externalBoxes;
	private GameObject FCollection;
	// H = huge
	private GameObject HCollection;
	private GameObject Barrier;

	private int barriersNum;

	// F = final & B = Boxes & G = ground & W = wall & E = external
	private GameObject FGB1;
	private GameObject FGB2;
	private GameObject FGB3;
	private GameObject FWB1;
	private GameObject FWB2;
	private GameObject FEB1;
	private GameObject FEB2;

	private int FHight = 1;
	private int randNum;
	private float timeToIncreaseSpead = 1.0f;
	private float dTimeToIncreaseSpead;
	private float timeToIncreaseGenerateBarrier = 1.0f;
	private float dTimeToIncreaseGenerateBarrier;

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		rb = Player.GetComponent<Rigidbody>();

		boxes = new GameObject[30];  // 30 is the number of boxes i have
		groundBoxes = new GameObject[3]; // because of the 3 way movement
		wallBoxes = new GameObject[2]; // 2 walls at the right and left
		externalBoxes = new GameObject[2]; // 2 shapes of bexes at the right and left
		FCollection = new GameObject("CollectionOfBoxes");
		HCollection = new GameObject("HugeCollection");
		Barrier = new GameObject("Barrier");

		barriersNum = 1;
		randNum = -100;

		dTimeToIncreaseSpead = Time.time;
		dTimeToIncreaseGenerateBarrier = Time.time;

		InstantiateBoxes();
	}


	void Update () 
	{
		CreateGround ();
		UpdateSpead ();
	}

	// im sure that theres error here
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

	void Speed (float speed)
	{
		rb.velocity = rb.velocity + new Vector3(0.0f, 0.0f, speed);
		dTimeToIncreaseSpead = Time.time;
	}

	// instantiate the boxes i have and put it in "boxes[]"
	void InstantiateBoxes ()
	{
		boxes[0] = (GameObject)Resources.Load("Boxes/CardboardCupeBox"); 
		boxes[1] = (GameObject)Resources.Load("Boxes/CardboardOpenBox");
		boxes[2] = (GameObject)Resources.Load("Boxes/CardboardRectBox");
		boxes[3] = (GameObject)Resources.Load("Boxes/OldBigBox");
		boxes[4] = (GameObject)Resources.Load("Boxes/OldSmallBox");

		groundBoxes[0] = boxes[0];
		groundBoxes[1] = boxes[0];
		groundBoxes[2] = boxes[0];

		wallBoxes[0] = boxes[0];
		wallBoxes[1] = boxes[0];

		externalBoxes[0] = boxes[1];
		externalBoxes[1] = boxes[0];



		// instantiate the first collection of boxes
		FGB1 = Instantiate(groundBoxes[0], new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		FGB2 = Instantiate(groundBoxes[1], new Vector3 (FGB1.GetComponent<MeshRenderer>().bounds.size.x, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		FGB3 = Instantiate(groundBoxes[2], new Vector3 (FGB2.GetComponent<MeshRenderer>().bounds.size.x * 2, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		FGB1.transform.SetParent(FCollection.transform, true);
		FGB2.transform.SetParent(FCollection.transform, true);
		FGB3.transform.SetParent(FCollection.transform, true);

		FWB1 = Instantiate(wallBoxes[0], new Vector3 (-FGB1.GetComponent<MeshRenderer>().bounds.size.x, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		GameObject FWB11 = Instantiate(wallBoxes[0], new Vector3 (-FGB1.GetComponent<MeshRenderer>().bounds.size.x, FWB1.GetComponent<MeshRenderer>().bounds.size.y , 0.0f), Quaternion.identity) as GameObject;
		FWB2 = Instantiate(wallBoxes[1], new Vector3 (FGB3.GetComponent<MeshRenderer>().bounds.size.x * 3, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		GameObject FWB22 = Instantiate(wallBoxes[1], new Vector3 (FGB3.GetComponent<MeshRenderer>().bounds.size.x * 3, FGB3.GetComponent<MeshRenderer>().bounds.size.y , 0.0f), Quaternion.identity) as GameObject;
		FWB1.transform.SetParent(FCollection.transform, true);
		FWB11.transform.SetParent(FCollection.transform, true);
		FWB2.transform.SetParent(FCollection.transform, true);
		FWB22.transform.SetParent(FCollection.transform, true);

		for(int i = 0; i < 10; i++)
		{
			FEB1 = Instantiate(externalBoxes[0], new Vector3 (-FWB1.GetComponent<MeshRenderer>().bounds.size.x * (2 + i), 0.0f, 0.0f), Quaternion.identity) as GameObject;
			FEB2 = Instantiate(externalBoxes[1], new Vector3 (FWB1.GetComponent<MeshRenderer>().bounds.size.x * (4 + i), 0.0f, 0.0f), Quaternion.identity) as GameObject;
			FEB1.transform.SetParent(FCollection.transform, true);
			FEB2.transform.SetParent(FCollection.transform, true);
		}
		// end of instatiation


//		Destroy(FCollection, 3);


//		CreateRowOfGround(FCollection);
//		CreateRowOfGround(FGB1, FGB2, FGB3, FWB1, FWB2, FEB1, FEB2);
	
		CreateCollectionOfRows();


	}


	void CreateGround ()
	{	
		if(FCollection.transform.position.z < Player.transform.position.z + 50)
		{	
//			CreateRowOfGround(FCollection);
//			CreateCollectionOfRows();
			CreateRowOfGround(FGB1, FGB2, FGB3, FWB1, FWB2, FEB1, FEB2);
		}
	}


	void CreateRowOfGround (GameObject GB1, GameObject GB2, GameObject GB3, GameObject WB1, GameObject WB2, GameObject EB1, GameObject EB2)
	{
		float z = FGB1.GetComponent<MeshRenderer>().bounds.size.z * FHight++;

		FCollection.transform.DetachChildren();
		FCollection.transform.position = new Vector3(FGB1.transform.position.x, 0.0f, z);  /// no need to FGB1.transform.position.x just 0.0f

		// X & Y & Z the size in that axis
		float FGB1X = FGB1.GetComponent<MeshRenderer>().bounds.size.x;
		float FGB1Y = FGB1.GetComponent<MeshRenderer>().bounds.size.y;
		float FGB1Z = FGB1.GetComponent<MeshRenderer>().bounds.size.z;

		FGB1 = Instantiate(GB1, new Vector3 (0.0f, 0.0f, z), Quaternion.identity) as GameObject;
		FGB2 = Instantiate(GB2, new Vector3 (FGB1X, 0.0f, z), Quaternion.identity) as GameObject;
		FGB3 = Instantiate(GB3, new Vector3 (FGB1X * 2, 0.0f, z), Quaternion.identity) as GameObject;
		FGB1.transform.SetParent(FCollection.transform, true);
		FGB2.transform.SetParent(FCollection.transform, true);
		FGB3.transform.SetParent(FCollection.transform, true);

		float FWB1Y = FWB1.GetComponent<MeshRenderer>().bounds.size.y;
		FWB1 = Instantiate(WB1, new Vector3 (-FGB1X, 0.0f, z), Quaternion.identity) as GameObject;
		GameObject FWB11 = Instantiate(WB1, new Vector3 (-FGB1X, FWB1Y , z), Quaternion.identity) as GameObject;
		FWB2 = Instantiate(WB2, new Vector3 (FGB1X * 3, 0.0f, z), Quaternion.identity) as GameObject;
		GameObject FWB22 = Instantiate(WB2, new Vector3 (FGB1X * 3, FWB1Y , z), Quaternion.identity) as GameObject;
		FWB1.transform.SetParent(FCollection.transform, true);
		FWB11.transform.SetParent(FCollection.transform, true);
		FWB2.transform.SetParent(FCollection.transform, true);
		FWB22.transform.SetParent(FCollection.transform, true);
		
		for(int i = 0; i < 10; i++)
		{
			FEB1 = Instantiate(EB1, new Vector3 (-FGB1X * (2 + i), 0.0f, z), Quaternion.identity) as GameObject;
			FEB2 = Instantiate(EB2, new Vector3 (FGB1X * (4 + i), 0.0f, z), Quaternion.identity) as GameObject;
			FEB1.transform.SetParent(FCollection.transform, true);
			FEB2.transform.SetParent(FCollection.transform, true);
		}

		barriersNum--;


		//Instantiate Barriers

		// random from 1 to 100 if the number appear is "1" the create barriers 
		//and every "yield 20 second" decrease the randome range by 1 so that from 1 to 99



		if((int)Time.time % 3 == 0 && randNum != 0) //every 3 second increase the dificulty
		{
//"4" very defferent from "3" why ??????????
			randNum += 3; 
			dTimeToIncreaseGenerateBarrier = Time.time;
		}


//			if(randNum != 0 && timeToIncreaseGenerateBarrier < Time.time - dTimeToIncreaseGenerateBarrier) // constant function like(Velocity += 1)
//			{
//				randNum += 10;  // write a function versus time 
//				dTimeToIncreaseGenerateBarrier = Time.time;
//			}

		if(barriersNum == 0)
		{	
			int[,] barrierArr = new int[0,0];

			Barrier.transform.DetachChildren();
			Barrier.transform.position = new Vector3(FGB1.transform.position.x, 0.0f, z); /// no need to FGB1.transform.position.x just 0.0f
			int barrier = Random.Range(randNum, 4);

			switch(barrier)
			{
			case 1: 
				// line of Barriers
				int num = (int)Random.Range(1, 7);
				int location = (int)Random.Range(1, 3);

				barrierArr = new int[num, 3];

					Debug.Log("num: " + num + " location: " + location);
				


				for(int i =0; i < num; i++)
				{	
					Debug.Log("i: " + i + " location: " + location);
					barrierArr[i,location - 1] = 1;
				}
				barriersNum = num + 1; // add "1" to avoid concatination between two rows
				break;

			case 2:
				// line of Barriers
				int num1 = Random.Range(1, 4);
				int num2 = Random.Range(1, 4);
				int num3 = Random.Range(1, 4);
				barrierArr = new int[num1 + num2 + num3 + 2, 3];

				for(int i =0; i < num1; i++)
				{
					barrierArr[i,0] = 1;
				}
				
				for(int i = num1 + 1; i < num2 + num1 + 1; i++)
				{
					barrierArr[i,1] = 1;
				}
				
				for(int i = num2 + num1 + 2; i < num3 + num2 + num1 + 2; i++)
				{
					barrierArr[i,2] = 1;
				}
				
				barriersNum = num1 + num2 + num3 + 2 + 1;
				break;
				
			case 3:
				// line of Barriers
				int num11 = Random.Range(1, 4);
				int num22 = Random.Range(1, 4);
				int num33 = Random.Range(1, 4);

				barrierArr = new int[num11 + num22 + num33 + 2, 3];

				for(int i =0; i < num11; i++)
				{
					barrierArr[i, 2] = 1;
				}
				
				for(int i = num11 + 1; i < num22 + num11 + 1; i++)
				{
					barrierArr[i, 1] = 1;
				}
				
				for(int i = num22 + num11 + 2; i < num11 + num22 + num33 + 2; i++)
				{
					barrierArr[i, 0] = 1;
				}
				
				barriersNum = num11 + num22 + num33 + 2 + 1;
				break;

			case 4:
				int locationOfSpace = Random.Range(1, 3);
				barrierArr = new int[1, 3];

				if(locationOfSpace == 1)
				{
					barrierArr[0,1] = 1;
					barrierArr[0,2] = 1;
				}
				else if(locationOfSpace == 2)
				{
					barrierArr[0,0] = 1;
					barrierArr[0,2] = 1;
				}
				else {
					barrierArr[0,0] = 1;
					barrierArr[0,1] = 1;
				}

				barriersNum = 1 + 1;
				break;

			default:
				// no Barriers
				barriersNum = 1;
				break;
			}


			for(int row = 0; row < barrierArr.GetLength(0); row++) //barrierArr.GetLength(0) == arr.length
			{
				for(int column = 0; column < barrierArr.GetLength(1) ;column++) // barrierArr.GetLength(1) == 3
				{
					if(barrierArr[row, column] == 1)
					{
						GameObject Bar = Instantiate(boxes[1], new Vector3 (FGB1X * (column), FGB1Y , z + (row * FGB1Z)), Quaternion.identity) as GameObject;
						Bar.transform.SetParent(Barrier.transform, true);
					}
				}
			}

			barrierArr = new int[0,0];
			
		}

		//End of Barriers

//		Destroy(FCollection, 3);
	}

	IEnumerator TimeToCreate ()
	{
		// 20 second wait and then increase
		yield return new WaitForSeconds(20);
	}

	void CreateRowOfGround (GameObject Collection)
	{
		FCollection = Instantiate(Collection, new Vector3 (0.0f, 0.0f,  FGB1.GetComponent<MeshRenderer>().bounds.size.z * FHight++), Quaternion.identity) as GameObject;
//		Destroy(FCollection, 3);
	}

	void CreateCollectionOfRows()
	{
		float z = FGB1.GetComponent<MeshRenderer>().bounds.size.z * (FHight + 1);
		HCollection.transform.DetachChildren();
		HCollection.transform.position = new Vector3(FGB1.transform.position.x, 0.0f, z);

		for(int i = 1; i <= 10; i++)
		{
			CreateRowOfGround(FCollection);
			FCollection.transform.SetParent(HCollection.transform, true);
		}
	}











	/*
	void SetCollection (GameObject GB1, GameObject GB2, GameObject GB3, GameObject WB1, GameObject WB2, GameObject EB1, GameObject EB2)
	{
		FCollection.transform.DetachChildren();

		GB1.transform.SetParent(FCollection.transform, true);
		GB2.transform.SetParent(FCollection.transform, true);
		GB3.transform.SetParent(FCollection.transform, true);
		WB1.transform.SetParent(FCollection.transform, true);
		WB2.transform.SetParent(FCollection.transform, true);
		EB1.transform.SetParent(FCollection.transform, true);
		EB2.transform.SetParent(FCollection.transform, true);
	}
	*/
}
