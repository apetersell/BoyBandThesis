using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamRandomGenertator : MonoBehaviour {
	public float generateGap = 5f;
	float timer = 0;
	public GameObject dream;
	public Sprite badDream, goodDream;
	Transform[] dreamTransfroms;
	int lastBadDreamIndex = -1;

	// Use this for initialization
	void Start () {
		BulletZEmitter bzE = GetComponent<BulletZEmitter>();
		dreamTransfroms = new Transform[4]{bzE.tLeft,bzE.tRight,bzE.tUp,bzE.tDown};

		generateDreams();
	}

	void generateDreams(){
		int newBadDreamIndex;// = Random.Range(0,3);
		do{
			newBadDreamIndex = Random.Range(0,3);
		}while(newBadDreamIndex == lastBadDreamIndex);

		for(int i =0; i< dreamTransfroms.Length;i++){
			GameObject newDream = Instantiate(dream,dreamTransfroms[i].position,Quaternion.identity,dreamTransfroms[i]) as GameObject;
			newDream.GetComponent<SpriteRenderer>().sprite = i == newBadDreamIndex?badDream:goodDream;
			newDream.name = i == newBadDreamIndex?"badDream":"goodDream";
		}

		lastBadDreamIndex = newBadDreamIndex;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > generateGap){
			generateDreams();
			timer = 0;
		}
		//if(transform.childCount <)
	}
}
