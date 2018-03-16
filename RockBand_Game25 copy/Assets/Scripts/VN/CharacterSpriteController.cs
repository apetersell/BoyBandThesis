using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;

public class CharacterSpriteController : MonoBehaviour 
{
	public string spriteIndexName;
	public string outfitString; 
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
	public Sprite [] outfitBases;
	public Image[] faceParts;
	protected Image img;
	public bool speaking;
	public Vector3 speakingScale;
	public Vector3 normalScale;

	public virtual void Awake ()
	{
		for (int i = 0; i < 3; i++) 
		{
			faceParts [i] = transform.GetChild (i).GetComponent<Image>();
		}
	}

	// Use this for initialization
	void Start () 
	{
		fadeInPos = transform.position;
		transform.position = fadeOutTransfrom.position;
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (speaking) {
			transform.localScale = speakingScale;
		} else {
			transform.localScale = normalScale;
		}
	}

	[YarnCommand("move")]
	public void MoveCharacter(string destinationName) {
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
		speakingScale = new Vector3 (speakingScale.x * -1, speakingScale.y, speakingScale.y);
		normalScale = new Vector3 (normalScale.x * -1, normalScale.y, normalScale.z);
	}

	public void doFriendEffect (bool isHappy)
	{
		GameObject effect = Instantiate (friendEffect) as GameObject;
		effect.transform.SetParent (GameObject.Find ("Canvas (1)").transform);
		effect.GetComponent<RectTransform> ().localPosition = GetComponent<RectTransform> ().localPosition;
		effect.GetComponent<FriendEffect> ().happy = isHappy;
	}

	[YarnCommand("outfit")]
	public virtual void changeOutfit (string outfit)
	{
		if (outfit == "Regular") 
		{
			img.sprite = outfitBases [0];
			outfitString = "everyday";
		} 
		else if (outfit == "Casual") 
		{
			img.sprite = outfitBases [1];
			outfitString = "casual";
		} 
		else if (outfit == "Stage") 
		{
			img.sprite = outfitBases [2];
			outfitString = "stage";
		}
			
	}

	[YarnCommand("expression")]
	public virtual void changeExpression (string sent)
	{
		string _brows = sent.Substring (0, 1);
		string _mouth = sent.Substring (1, 1);
		string _eyes = sent.Substring (2, 1); 
		string _eyePos = sent.Substring (3, 1);
		string brows = null;
		string mouth = null;
		string eyes = null;
		string eyePos = null;

		switch (_brows) 
		{
		case "N":
			brows = "1";
			break;
		case "A":
			brows = "2";
			break;
		case "R":
			brows = "3";
			break;
		}

		switch (_mouth) 
		{
		case "N":
			mouth = "1";
			break;
		case "H":
			mouth = "2";
			break;
		case "S":
			mouth = "3";
			break;
		case "L":
			mouth = "4";
			break;
		case "O":
			if (this.gameObject.name == "Kent") {
				mouth = "4";
			} else {
				mouth = "5";
			}
			break;
		}
		switch (_eyes) 
		{
		case "N":
			eyes = "1";
			break;
		case "S":
			eyes = "2";
			break;
		case "W":
			eyes = "3";
			break;
		}
		switch (_eyePos) 
		{
		case "F":
			eyePos = "-1";
			break;
		case "B":
			eyePos = "-2";
			break;
		case "U":
			eyePos = "-3";
			break;
		}

		faceParts [0].sprite = findFacePart ("brows", brows);
		faceParts [1].sprite = findFacePart ("eyes", eyes + eyePos);
		faceParts [2].sprite = findFacePart ("mouth", mouth);

	}

	public Sprite findFacePart (string piece, string lookingFor)
	{
		string filePath = null;
		string initFilePath = "Sprites/visualnovelly/MainCharacters/";
		if (this.gameObject.name == "Kent") 
		{
			filePath = initFilePath + this.gameObject.name + "/" + spriteIndexName + "-" + piece + "-"  + lookingFor;
		} 
		else 
		{
			if (piece == "mouth") 
			{
				filePath = initFilePath + this.gameObject.name + "/" + this.gameObject.name + "_everyday/" + spriteIndexName + "-mouth-" + lookingFor;
			} 
			else 
			{
				filePath = initFilePath + this.gameObject.name + "/" + this.gameObject.name + "_" + outfitString + "/" + spriteIndexName + "-" + outfitString + "-" + piece + "-" + lookingFor;
			}
		}
		Sprite s = Resources.Load<Sprite> (filePath);
		Debug.Assert (s != null, "Couldn't find " + piece + " sprite!");
		return s;
	}
}
