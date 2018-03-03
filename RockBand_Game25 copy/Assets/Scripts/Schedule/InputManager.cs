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
	TalkShow = 4,
	Songwriting = 5,
	StreetModeling = 6,
}


public class InputManager : MonoBehaviour
, IPointerDownHandler // 2
, IPointerUpHandler
, IPointerEnterHandler
, IPointerExitHandler
	// ... And many more available!
{
	Image sprite; //Get Referecne to Image Script
	Sprite savedSprite; //What sprite a unit will take on when hovered over.
	public UnitType JPeGame; //Tracks what activities the boyz are doing that day.
	public UnitType LeeGame;
	public static UnitType currentType = UnitType.None; //What a unit will become when we click it.
	private UnitType myType = UnitType.None; //The type current associated with with this unit block.
	public UnitType MyType{
		get{
			return myType;
		}
	}
	public static Sprite currentSprite = null; //What sprite a unit will take on when it's clicked.
	public static Color blank = new Color(0,0,0,0); //Alpha'd out color.
	public static bool mouseIsDown = false; //Returns true if the mouse is being held down.

	void Awake()
	{
		savedSprite = Resources.Load<Sprite> ("blank"); //Make sure we have no sprite to start.
		currentSprite = savedSprite;
		sprite = GetComponent<Image>();
	}

	void Update ()
	{

	}
		
	//When the unit is clicked.
	public void OnPointerDown(PointerEventData eventData) // 3
	{
		sprite.sprite = currentSprite; // Change sprite to whatever activity we have selected.
		setType(); //Set the units type to the correct type.
		mouseIsDown = true;
	}
		
	//When the mouse butto is released.
	public void OnPointerUp(PointerEventData eventData) // 3
	{
		mouseIsDown = false;
		sprite.sprite = currentSprite;
		setType();

	}

	//When the pointer hovers over the node.
	public void OnPointerEnter(PointerEventData eventData)
	{
		sprite.sprite = currentSprite; //Change the sprite to whatever activity we have selected.
		if(mouseIsDown){ //If the mouse is being held down, set its type to whatever type we have selected.
			setType();
		}
	}

	//Logic that sets type.
	void setType(){

		if(myType == UnitType.None && currentType != UnitType.None){
			GameObject.Find("GlobalStats").GetComponent<GlobalManager>().scheduleSettle();
		}
		savedSprite = currentSprite; //Changed the reference of the saved sprite to current sprite;
		myType = currentType; //Change this units type to the selected type.
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if(!mouseIsDown){
			sprite.sprite = savedSprite;
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
