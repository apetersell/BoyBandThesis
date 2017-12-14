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
	Image img;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//When the pointer hovers over the node.
	public void OnPointerEnter(PointerEventData eventData)
	{
		img.sprite = rollover;
	}
		
	public void OnPointerExit(PointerEventData eventData)
	{
		img.sprite = neutral;
	}
}
