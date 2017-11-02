using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum UnitType{
	None = -1,
	Sleep = 0,
	Dance = 1,
	Vocal = 2,
	PR = 3,
}


public class InputManager : MonoBehaviour
, IPointerDownHandler // 2
, IPointerUpHandler
, IPointerEnterHandler
, IPointerExitHandler
	// ... And many more available!
{
	Image sprite;
	Color savedColor;
	public static UnitType currentType = UnitType.None;
	private UnitType myType = UnitType.None;
	public UnitType MyType{
		get{
			return myType;
		}
	}
	public static Color currentColor = Color.white;
	public static bool isIncluding = false;

	void Awake()
	{
		savedColor = Color.white;
		sprite = GetComponent<Image>();
	}
		

	public void OnPointerDown(PointerEventData eventData) // 3
	{
		//print("I was clicked");
		sprite.color = currentColor;
		setType();
		isIncluding = true;
	}

	public void OnPointerUp(PointerEventData eventData) // 3
	{
		//print("I was clicked");
		isIncluding = false;
		sprite.color = currentColor;
		setType();

	}
		
	public void OnPointerEnter(PointerEventData eventData)
	{
		sprite.color = currentColor;
		if(isIncluding){
			setType();
		}
	}

	void setType(){

		if(myType == UnitType.None && currentType != UnitType.None){
//			Debug.Log("in");
			GameObject.Find("GlobalStats").GetComponent<GlobalManager>().scheduleSettle();
		}
		savedColor = currentColor;
		myType = currentType;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if(!isIncluding){
			sprite.color = savedColor;
		}else{
			setType();
		}
	}

	void checkDefaultToSleep(){
		if(myType == UnitType.None){
			myType = UnitType.Sleep;
			//sprite.color = GameObject.Find("BtnSleep").GetComponent<Image>().color;
			//savedColor = sprite.color;
		}
	}


}
