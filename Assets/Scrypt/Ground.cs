using UnityEngine;
using System.Collections;


// ask yahya about "obj1.transform.SetParent(Boxes.transform, false);"  when "true" or "false" ????
public class Ground : MonoBehaviour {

	private GameObject Player;
	private Player player;

	private GameObject Last, Inside, Next;
	private GameObject[] boxes;
	private Barriers Barriers;


	private int[] style;
	private int numOfFullStyles;

	private int numBoxesCollection;
	private float hightBoxesCollection;

	public float timeToExchangeStyle;
	private float dTimeToExchangeStyle;

	public int hight, width;  // width must be odd and at least 5 number because (3(road) + 2(wall) + 2 * n(another))
	private float zFinal;
	private float xBound;
	private float yBound;
	private float zBound;

	void Awake ()
	{

		LoadBoxes();
	}

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player") ;
		Barriers = this.GetComponent<Barriers>();

		Last = new GameObject("Last");
		Inside = new GameObject("Inside");
		Next = new GameObject("Next");

		GameObject TempObj = Instantiate(boxes[0], new Vector3 (-15, -15, -15), Quaternion.identity) as GameObject;
		xBound = TempObj.GetComponent<MeshRenderer>().bounds.size.x;
		yBound = TempObj.GetComponent<MeshRenderer>().bounds.size.y;
		zBound = TempObj.GetComponent<MeshRenderer>().bounds.size.z;
		Destroy(TempObj);

		zFinal = 0.0f;
		hightBoxesCollection = hight * zBound;
		dTimeToExchangeStyle = Time.time;

		style = new int[]{0, 1, 2, 3, 4, 1, 0};
		SetStyle (style, false);
	}


	void Update () 
	{
//		try{
			ExpandRoad ();
			CheckToExchangeStyle ();
//		} catch(System.Exception e)
//		{
//			Debug.Log("bug in Update method in Ground class.");
//		}
	}



	void CheckToExchangeStyle ()
	{
		if(timeToExchangeStyle < Time.time - dTimeToExchangeStyle)
		{
			ExchangeStyle ();
			dTimeToExchangeStyle = Time.time;
		}
	}

	void ExchangeStyle ()
	{
		if(numOfFullStyles > 0)
		{	
			for(int i = 0; i < style.GetLength(0); i++)
				style[i] += 5;

			numOfFullStyles--;

			SetStyle(style, true);
		}
		else
		{
			// tidy = 1 very tidy & = 2 natural tidy & = 3 never tidy
			int tidy = (int)(Random.Range(1, 2));
			int lastIndex = boxes.GetLength(0) - 1;
			int[] newStyle = style;

			int outside1 = Random.Range(0, lastIndex);
			int outside2 = Random.Range(0, lastIndex);
			int wall1 = Random.Range(0, lastIndex);
			int wall2 = Random.Range(0, lastIndex);
			int way1 = Random.Range(0, lastIndex);
			int way2 = Random.Range(0, lastIndex);
			int way3 = Random.Range(0, lastIndex);

			switch(tidy)
			{
			case 1: // very tidy
				newStyle = new int[]{outside1, wall1, way1, way2, way3, wall1, outside1};
				break;

			case 2: // natural tidy
				newStyle = new int[]{outside1, wall1, way1, way2, way3, wall2, outside2};
				break;

//			case 3: // never tidy
//				int[] newStyle = new int[]{outside1, wall1, way1, way2, way3, wall2, outside2};
//				break;

			default:
					break;
			}

			SetStyle(newStyle, false);
		}
	}

	void SetStyle (int[] newStyle, bool fullStyle)
	{
		if(fullStyle)
			style = newStyle;
		else
		{
			style = new int[width];

			for(int i = 0; i < width / 2 - 2; i++) // outside
				style[i] = newStyle[0];

			int j = 1;
			for(int i = width / 2 - 2; i < width / 2 + 3; i++) // walls and ways
				style[i] = newStyle[j++];

			
				int lastIndex = newStyle.GetLength(0) - 1;
			for(int i = width / 2 + 3; i < width; i++) // outside
				style[i] = style[i] = newStyle[lastIndex];
		}

		Instantiate_last_inside_next_collections (); /////////////////          i want to exchange style not to exchane evry thing>>>>>>>>   i want to make pooling.. if i instantiate new one i cant make pooling
	}

	void ExpandRoad ()
	{
		if(Player.transform.position.z > Inside.transform.position.z)
		{
			Last.transform.position = new Vector3(0.0f, 0.0f, hightBoxesCollection * numBoxesCollection);
			numBoxesCollection++;
			
			GameObject Temp = Last;
			Last = Inside;
			Inside = Temp;
			
			Temp = Inside;
			Inside = Next;
			Next = Temp;

			Last.name = "Last";
			Inside.name = "Inside";
			Next.name = "Next";

			zFinal += hightBoxesCollection;

			Barriers.MinimiseHightOfBarrier(hight);
		}
	}

	void Instantiate_last_inside_next_collections ()
	{
		Instantiate_collection_of_ground(Last, zFinal);
		zFinal += hightBoxesCollection;
		numBoxesCollection++;
		
		Instantiate_collection_of_ground(Inside, zFinal);
		zFinal += hightBoxesCollection;
		numBoxesCollection++;
		
		Instantiate_collection_of_ground(Next, zFinal);
		zFinal += hightBoxesCollection;
		numBoxesCollection++;
	}
	
	void Instantiate_collection_of_ground (GameObject Boxes, float z)
	{	// Boxes = last | inside | next

//		Destroy(Boxes, 5.0f);
		Boxes.transform.DetachChildren();
		Boxes.SetActive(false);

		for(int h = 0; h < hight; h++)
		{
			for(int w = 0; w < width; w++)
			{
				GameObject obj = Instantiate(boxes[style[w]], new Vector3 (xBound * w, 0.0f, zBound * h), Quaternion.identity) as GameObject;
				obj.transform.SetParent(Boxes.transform, false);
				obj.tag = "GroundBox";
			}

			int mid = width / 2;
			GameObject obj1 = Instantiate(boxes[style[mid - 2]], new Vector3 (xBound * (mid - 2), yBound, zBound * h), Quaternion.identity) as GameObject;
			obj1.transform.SetParent(Boxes.transform, false);
			GameObject obj2 = Instantiate(boxes[style[mid + 2]], new Vector3 (xBound * (mid + 2), yBound, zBound * h), Quaternion.identity) as GameObject;
			obj2.transform.SetParent(Boxes.transform, false);

			obj1.tag = "Barrier";
			obj2.tag = "Barrier";
		}

		Boxes.transform.position = new Vector3(0.0f, 0.0f, z);
		Boxes.SetActive(true);
	}


	void LoadBoxes () /////// >>.............................................................................. buy it...buy it..buy it..buy it...buy it.
	{
		boxes = new GameObject[5];
		
		boxes[0] = (GameObject)Resources.Load("Boxes/CardboardCupeBox"); 
		boxes[1] = (GameObject)Resources.Load("Boxes/CardboardCupeBox"); 
		boxes[2] = (GameObject)Resources.Load("Boxes/CardboardCupeBox"); 
		boxes[3] = (GameObject)Resources.Load("Boxes/CardboardCupeBox"); 
		boxes[4] = (GameObject)Resources.Load("Boxes/CardboardCupeBox"); 
//		boxes[1] = (GameObject)Resources.Load("Boxes/CardboardOpenBox");
//		boxes[2] = (GameObject)Resources.Load("Boxes/CardboardRectBox");
//		boxes[3] = (GameObject)Resources.Load("Boxes/OldBigBox");
//		boxes[4] = (GameObject)Resources.Load("Boxes/OldSmallBox");

		numOfFullStyles = boxes.GetLength(0) / 5 - 1; // - 1 because we consume one style
	}


	public GameObject[] GetBoxes ()
	{
		return boxes;
	}

	public int GetMid ()
	{
		return width / 2;
	}

	public float GetXBound ()
	{
		return xBound;
	}

	public float GetYBound ()
	{
		return yBound;
	}

	public float GetZBound ()
	{
		return zBound;
	}

}
