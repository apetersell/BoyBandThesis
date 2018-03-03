using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepZBullet : MonoBehaviour {
	float destroyTime = 1.5f;
	float destroyTimer = 0;
	public Vector3 dir = Vector3.zero;
	public static float speed = 3;

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one*0.5f;
	   destroyTime = Beat.Clock.Instance.MeasureLength() + Beat.Clock.Instance.HalfLength();
	}
	
	// Update is called once per frame
	void Update () {
		destroyTimer += Time.deltaTime;
		if(destroyTimer > destroyTime){
			Destroy(gameObject);
		}
		//Debug.Log(transform.forward);
		transform.Translate(dir*speed*Time.deltaTime);
		transform.localScale += Vector3.one*Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name.Contains("Dream")){
			Destroy(gameObject);
			
		}
	}
}
