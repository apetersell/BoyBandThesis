using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickAndGetColor : MonoBehaviour,IPointerClickHandler {
	
	public void OnPointerClick (PointerEventData eventData){
		InputManager.currentColor = GetComponent<Image>().color;

	} 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
