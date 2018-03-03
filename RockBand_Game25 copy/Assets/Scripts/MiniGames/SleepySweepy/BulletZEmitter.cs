using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BulletZEmitter : MonoBehaviour {


	public Transform tLeft, tRight, tUp, tDown;
	Vector3 dirLeft, dirRight, dirUp, dirDown;
	public GameObject bulletZ;
	[SerializeField]float generategap = 1;
	float timer;
	Vector3 currentDir;// = dirUp;
	int goodDreamCount = 0,badDreamCount = 0;
	public Text stressText;
	float stressNum;
	float radius;
	float stressDecreaseEachGoodDream = 1;
	float stressDecreaseEachBadDream = 10;
	GlobalManager gm;
	//int currentAimIndex;
	// Use this for initialization
	void Start () {

      Beat.Clock.Instance.SetBPM(120);
		radius = GetComponent<CircleCollider2D>().radius + bulletZ.GetComponent<BoxCollider2D>().bounds.size.magnitude;

		gm = FindObjectOfType(typeof(GlobalManager)) as GlobalManager;
		stressNum = gm? gm.Stress:100;
		stressText.text = "Stress:"+stressNum.ToString() ;


		dirLeft = (tLeft.position - transform.position).normalized;
		dirRight = (tRight.position - transform.position).normalized;
		dirUp = (tUp.position - transform.position).normalized;
		dirDown = (tDown.position - transform.position).normalized;
		currentDir = dirUp;

		timer = generategap;

		stressDecreaseEachGoodDream = gm.timePerUnit*10f*16f/GetComponent<DreamRandomGenertator>().generateGap/3f;
		stressDecreaseEachBadDream = goodDreamCount*10f;
	}
	
	// Update is called once per frame
	void Update () {
		currentDir = Vector3.zero;

		if(Input.GetKey(KeyCode.UpArrow)){
			currentDir = dirUp;
		}else if(Input.GetKey(KeyCode.DownArrow)){
			currentDir = dirDown;
		}else if (Input.GetKey(KeyCode.RightArrow)){
			currentDir = dirRight;
		}else if(Input.GetKey(KeyCode.LeftArrow)){
			currentDir = dirLeft;
		}

		//Debug.Log(currentDir);
		if(currentDir != Vector3.zero){
			timer += Time.deltaTime;
			if(timer > generategap){
				timer = 0;
				GameObject newZBullet = Instantiate(bulletZ,transform.position+currentDir*radius,Quaternion.identity);
				newZBullet.name = "ZBullet";
				newZBullet.GetComponent<SleepZBullet>().dir = currentDir;
			}
		}else{
			timer = generategap;
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log(other);
		if (other.name.Equals ("goodDream")) {
			if (!gm.isStopped) {
				goodDreamCount++;
				if (stressNum - stressDecreaseEachGoodDream < 0) {
					stressNum = 0;
				} else {
					stressNum -= stressDecreaseEachGoodDream;
				}
			} else if (other.name.Equals ("badDream")) {
				badDreamCount++;
				stressNum += stressDecreaseEachBadDream;
			}
			if (gm) {
				gm.Stress = stressNum;
			}
			stressText.text = "Stress:" + Mathf.RoundToInt (stressNum).ToString ();
		}
	}

//	void OnCollisionEnter2D(Collision2D other) {
//		Debug.Log(other);
//		if(other.collider.name.Equals("goodDream")){
//			goodDreamCount++;
//			if(stressNum - 10 < 0){
//				stressNum -= 10;
//			}
//		}else if(other.collider.name.Equals("badDream")){
//			badDreamCount++;
//			stressNum += 10;
//		}
//		stressText.text = "Stress:"+stressNum.ToString();
//	}
}
