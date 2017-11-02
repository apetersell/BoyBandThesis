using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalBlockManager : Singleton<VocalBlockManager> {
	protected VocalBlockManager(){}

	public float MinBlockLength,
	MaxBlockLength, 
	MinGapLength,
	MaxGapLength, 
	MaxBlockHeight,MinBlockHeight, 
	BlockThickness;

	public float BeginPosX, EndPosX;//Transform beginTransform, EndTransfrom;
	public float speed = 4f;
	public GameObject blockUnit;

	public int unitNumOneTime = 5;

	// Use this for initialization
	void Start () {
		BeginPosX = GameObject.Find("lineRight").transform.position.x;
		EndPosX = GameObject.Find("lineLeft").transform.position.x;
		generateBlocks();
	}


	void generateBlocks(){

		float unitBeginPosX = BeginPosX;
		float unitLength = blockUnit.transform.localScale.x;
//		Debug.Log(unitLength);

		for(int i = 0; i < unitNumOneTime; i++)
		{
			GameObject line = new GameObject ("Line");
			line.AddComponent<LineUnit> ();
			float length = MinBlockLength + (MaxBlockLength - MinBlockLength) * Random.value;
			int num = Mathf.FloorToInt(length / unitLength) ;
//			Debug.Log(num);

			float posY = MinBlockHeight + (MaxBlockHeight - MinBlockHeight) * Random.value;

			for(int j = 0; j < num; j++){
				float posX = unitBeginPosX + j*0.5f;
				GameObject newUnit = Instantiate(blockUnit) as GameObject;
				newUnit.transform.parent = line.transform;
				line.GetComponent<LineUnit> ().blocks.Add (newUnit.GetComponent<SpriteRenderer> ());
				newUnit.transform.position = new Vector3(posX,posY,0);
				newUnit.AddComponent<BlockUnit>();
			}
			float gap = MinGapLength + (MaxGapLength - MinGapLength)*Random.value;
			unitBeginPosX += num*0.5f + gap;
		}

		float time = (unitBeginPosX - BeginPosX)/speed;
		StartCoroutine(waitForNextGenerate(time));
	}

	IEnumerator waitForNextGenerate(float time){
		yield return new WaitForSeconds(time);
		generateBlocks();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
