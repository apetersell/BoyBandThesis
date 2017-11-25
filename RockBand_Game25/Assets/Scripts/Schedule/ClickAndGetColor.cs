using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ClickAndGetColor : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler {
	public Color danceColor, vocalColor, RestColor, PRColor;
	public float initPosX;
	[SerializeField]ClickAndGetColor[] buttons;
	bool isTweenForward, isTweenBack;
	//public static

	public void OnPointerClick (PointerEventData eventData){
		string s_type = this.name.Substring(3);
		InputManager.currentType = (UnitType)System.Enum.Parse( typeof( UnitType ), s_type );

		foreach(ClickAndGetColor c in buttons){
			c.SendMessage("tweenBack");
		}

		switch(InputManager.currentType){
		case UnitType.Dance:InputManager.currentColor = danceColor;break;
		case UnitType.Vocal:InputManager.currentColor = vocalColor;break;
		case UnitType.PR:InputManager.currentColor = PRColor;break;
		case UnitType.Sleep:InputManager.currentColor = RestColor;break;
		}


	} 

	public void OnPointerEnter(PointerEventData eventData){
		//Debug.Log(this.name + " enter");
		//Debug.Log(transform.position);
		if(isTweenForward){
			return;
		}
		//transform.DOMoveX(initPosX+100,1f);
		isTweenForward = true;
		float currentPosX = GetComponent<RectTransform>().anchoredPosition.x;
		GetComponent<RectTransform>().DOAnchorPosX(initPosX-200,Mathf.Abs(currentPosX-initPosX+200)/200f*0.5f).OnComplete(TweenForwardComplete);
		foreach(ClickAndGetColor c in buttons){
			c.SendMessage("tweenBack");
		}
	}

	void TweenForwardComplete(){
		isTweenForward = false;
		//string s_type = this.name.Substring(3);
		//if(!InputManager.currentType.ToString().Equals(this.name.Substring(3))){
		tweenBack();
		//}
	}

	public void OnPointerExit (PointerEventData eventData){
		//if(!InputManager.currentType.ToString().Equals(this.name.Substring(3))){
			tweenBack();
		//}
	}


	void tweenBack(){
		if(isTweenBack || InputManager.currentType.ToString().Equals(this.name.Substring(3)))
		{
			return;
		}
			
		float currentPosX = GetComponent<RectTransform>().anchoredPosition.x;
		isTweenBack = true;
		GetComponent<RectTransform>().DOAnchorPosX(initPosX,Mathf.Abs(currentPosX-initPosX)/200f*0.5f).OnComplete(TweenBackComplete);
	}

	void TweenBackComplete(){
		isTweenBack = false;
	}

	// Use this for initialization
	void Start () {
		isTweenForward = isTweenBack = false;
		initPosX = GetComponent<RectTransform>().anchoredPosition.x;
		buttons = new ClickAndGetColor[3];
		ClickAndGetColor[] cagc = FindObjectsOfType(typeof(ClickAndGetColor)) as ClickAndGetColor[];
		int i = 0;
		foreach(ClickAndGetColor c in cagc){
			if(c!= this){
				buttons[i] = c;
				i++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


}
