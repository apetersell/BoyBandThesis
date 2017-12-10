using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ClickAndGetColor : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler {
	[SerializeField]ClickAndGetColor[] buttons;
	bool isTweenForward, isTweenBack;
	public float hoveredSize;
	public float tweenTime;
	Vector3 originalScale;
	//public static

	public void OnPointerClick (PointerEventData eventData){
		string s_type = this.name.Substring(3);
		InputManager.currentType = (UnitType)System.Enum.Parse( typeof( UnitType ), s_type );

		foreach(ClickAndGetColor c in buttons){
			c.SendMessage("tweenBack");
		}
			
		InputManager.currentSprite = GetComponent<Image> ().sprite;
	} 

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(isTweenForward)
		{
			return;
		}
		isTweenForward = true;

		Vector3 bigger = new Vector3 (hoveredSize, hoveredSize, hoveredSize);
		GetComponent<RectTransform> ().DOScale (bigger, tweenTime).OnComplete(TweenForwardComplete);
	}

	void TweenForwardComplete()
	{
		isTweenForward = false;
	}

	public void OnPointerExit (PointerEventData eventData){
		if(!InputManager.currentType.ToString().Equals(this.name.Substring(3)))
		{
			tweenBack();
		}
	}


	void tweenBack()
	{
		if (isTweenBack || InputManager.currentType.ToString ().Equals (this.name.Substring (3))) {
			return;
		}
		GetComponent<RectTransform> ().DOScale (originalScale, tweenTime).OnComplete (TweenBackComplete);
		isTweenBack = true;
	}

	void TweenBackComplete()
	{
		isTweenBack = false;
	}

	// Use this for initialization
	void Start () {
		isTweenForward = isTweenBack = false;
		originalScale = transform.localScale;
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
