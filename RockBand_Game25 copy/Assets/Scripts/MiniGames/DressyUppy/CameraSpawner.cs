using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawner : MonoBehaviour {

	public Vector3 rightPos;
	public Vector3 leftPos; 
	public GameObject npc;
	public string [] sides;
	public int [] brands;
	public static ShuffleBag <string> sideBag;
	public static ShuffleBag <int> brandBag;
	public float spawnEvery_Frames;
	public float NPCMoveSpeed;
	float framesToNextSpawn;
	public float scoreZone;

	void Awake () {
		framesToNextSpawn = spawnEvery_Frames;
		if (!IsValidBag())  
		{
			sideBag = new ShuffleBag<string> ();
			brandBag = new ShuffleBag<int> ();
			for (int i = 0; i < sides.Length; i++) 
			{
				sideBag.Add (sides [i]);
			}
			for (int i = 0; i < brands.Length; i++) 
			{
				brandBag.Add (brands[i]);
			}
		}
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		spawning ();

	}

	void spawning ()
	{
		framesToNextSpawn--;
		if (framesToNextSpawn <= 0) 
		{
			makeNPC ();
			framesToNextSpawn = spawnEvery_Frames;
		}
	}

	void makeNPC ()
	{
		string whichSide = sideBag.Next ();
		int whichBrand = brandBag.Next ();
		GameObject newNpc = Instantiate (npc) as GameObject;
		CameraPeople cp = newNpc.GetComponent<CameraPeople> ();
		SpriteRenderer sr = newNpc.GetComponent<SpriteRenderer> ();
		if (whichSide == "RIGHT") 
		{
			newNpc.transform.position = rightPos;
			cp.endPos = leftPos.x;
			cp.spawnedRight = true;
			cp.scoreZone = scoreZone;
			sr.flipX = false;
		}
		if (whichSide == "LEFT") 
		{
			newNpc.transform.position = leftPos;
			cp.endPos = rightPos.x;
			cp.spawnedRight = false;
			cp.scoreZone = scoreZone * -1;
			sr.flipX = true;
		}
		cp.speed = NPCMoveSpeed;
		cp.brandIndex = whichBrand;
		Debug.Log (whichSide);
	}

	bool IsValidBag()
	{
		return sideBag != null; 
	}

}