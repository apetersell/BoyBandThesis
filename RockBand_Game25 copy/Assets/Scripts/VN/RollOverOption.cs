using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RollOverOption : MonoBehaviour // 2
, IPointerEnterHandler
, IPointerExitHandler  
{
	public Sprite rollover;
	public Sprite neutral; 
	public int myIndex;
	Image img;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (KeyboardControls.currentChoice == myIndex) {
			img.sprite = rollover;
		} else {
			img.sprite = neutral;
		}
	}

	//When the pointer hovers over the node.
	public void OnPointerEnter(PointerEventData eventData)
	{
		KeyboardControls.currentChoice = myIndex;
	}
		
	public void OnPointerExit(PointerEventData eventData)
	{
//		img.sprite = neutral;
	}
}
