using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

	public Vector3 rightPos;
	public Vector3 leftPos; 
	public GameObject npc;
	public string [] sides;
	public static ShuffleBag <string> sideBag;
	public float spawnEvery_Frames;
	public float NPCMoveSpeed;
	float framesToNextSpawn;

	void Awake () {
		framesToNextSpawn = spawnEvery_Frames;
		if (!IsValidBag())  
		{
			sideBag = new ShuffleBag<string> (); 
			for (int i = 0; i < sides.Length; i++) 
			{
				sideBag.Add (sides [i]);
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
		GameObject newNpc = Instantiate (npc) as GameObject;
		PassyNPC p = newNpc.GetComponent<PassyNPC> ();
		SpriteRenderer sr = newNpc.GetComponent<SpriteRenderer> ();
		if (whichSide == "LEFT") 
		{
			p.startPos = leftPos;
			p.endPos = rightPos;
			sr.flipX = true;
		}
		if (whichSide == "RIGHT") 
		{
			p.startPos = rightPos;
			p.endPos = leftPos;
			sr.flipX = false;
		}
		p.speed = NPCMoveSpeed;
	}

	bool IsValidBag()
	{
		return sideBag != null; 
	}

}
