using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;

public class CharacterSpriteController : MonoBehaviour {
	public Transform fadeOutTransfrom;
	public Transform OffStageLeft; 
	public Transform OffStageRight;
	public Transform centerStage;
	public Transform stageRight1;
	public Transform stageRight2;
	public Transform stageLeft1;
	public Transform stageleft2;
	public GameObject friendEffect;
	private Vector3 fadeInPos;

	// Use this for initialization
	void Start () {
		fadeInPos = transform.position;
		transform.position = fadeOutTransfrom.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[YarnCommand("move")]
	public void MoveCharacter(string destinationName) {
//		Debug.Log(destinationName);
		// move to the destination
		if (destinationName.Equals ("in")) {
			transform.DOMove (fadeInPos, 1f);
			
		} else if (destinationName.Equals ("out")) {
			transform.DOMove (fadeOutTransfrom.position, 1f);
		} else if (destinationName.Equals ("OffStageLeft")) 
		{
			transform.DOMove (OffStageLeft.position, 1f);
		}
		else if (destinationName.Equals ("OffStageRight")) 
		{
			transform.DOMove (OffStageRight.position, 1f);
		}
		else if (destinationName.Equals ("CenterStage")) 
		{
			transform.DOMove (centerStage.position, 1f);
		}
		else if (destinationName.Equals ("StageLeft1")) 
		{
			transform.DOMove (stageLeft1.position, 1f);
		}
		else if (destinationName.Equals ("StageLeft2")) 
		{
			transform.DOMove (stageleft2.position, 1f);
		} 
		else if (destinationName.Equals ("StageRight1")) 
		{
			transform.DOMove (stageRight1.position, 1f);
		}
		else if (destinationName.Equals ("StageRight2")) 
		{
			transform.DOMove (stageRight2.position, 1f);
		}
	}

	[YarnCommand("flip")] 
	public void FlipCharacter()
	{
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	public void doFriendEffect (bool isHappy)
	{
		GameObject effect = Instantiate (friendEffect) as GameObject;
		effect.transform.SetParent (GameObject.Find ("Canvas (1)").transform);
		effect.GetComponent<RectTransform> ().localPosition = GetComponent<RectTransform> ().localPosition;
		effect.GetComponent<FriendEffect> ().happy = isHappy;
	}
}
