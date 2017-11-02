using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dream : MonoBehaviour {
	float speed = 1f;
	Vector3 dir;
	bool isFadeIn = false;

	// Use this for initialization
	void Start () {
		dir = GameObject.Find("PlayerHead").transform.position - transform.position;
		dir.Normalize();
		transform.localScale = 0.1f*Vector3.one;
		//GetComponent<SpriteRenderer>().DOFade(0,0f);
		transform.DOScale(Vector3.one,0.5f);
		GetComponent<SpriteRenderer>().DOFade(1,1f).OnComplete(fadeInComplete);
	}

	void fadeInComplete(){
		isFadeIn = true;
	}

	// Update is called once per frame
	void Update () {
		if(isFadeIn){
			//speed += 0.1f*Time.deltaTime;
			transform.Translate(dir*speed*Time.deltaTime);
			
		}
	}

	void AbsorbComplete(){
		HitEffect he = (HitEffect)FindObjectOfType(typeof(HitEffect));
		he.active = true;
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log(other.name);
		if(other.name.Equals("PlayerHead")){
			//isFadeIn = false;
			speed*=1.5f;
			transform.DOScale(Vector3.one*0.3f,1f).OnComplete(AbsorbComplete);
			transform.GetComponent<SpriteRenderer>().DOFade(0.3f,0.8f);
		}else if(other.name.Equals("ZBullet")){
			Destroy(gameObject);
		}

	}
}
