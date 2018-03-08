using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDownHandler : MonoBehaviour {

	public float speed = 10.0F;
	public GameObject line;
	//public float rotationSpeed = 100.0F;
	float highestPosY, lowestPosY;
	public int score = 0;
	public Text txtScore;

	GlobalManager globe;
	ScoreManager sm;

	void Start(){
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		highestPosY = line.GetComponent<SpriteRenderer>().bounds.max.y;
		lowestPosY = line.GetComponent<SpriteRenderer>().bounds.min.y;
//		Debug.Log(highestPosY+" "+lowestPosY);
	}


	void Update() {
		float translation = Input.GetAxis("Vertical") * speed;
		//float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		//rotation *= Time.deltaTime;
		if(transform.position.y + translation > highestPosY - 0.5f){
			translation = highestPosY - 0.5f - transform.position.y;
			//transform.position = new Vector3(transform.position.)

		}else if(transform.position.y + translation < lowestPosY + 0.5f){
			translation = lowestPosY + 0.5f -transform.position.y;
		}

		transform.Translate(0, translation, 0);
			

		//transform.Rotate(0, rotation, 0);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.GetComponent<SpriteRenderer>())
		{
			coll.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
//			if (!globe.performance) 
//			{
//				sm.scorePoints (true);
//			} 
//			else 
//			{
//				score++;
//				txtScore.text = score.ToString ();
//				if (globe) {
//					globe.VocalScore++;
//				}
//			}
		}
	}
}
