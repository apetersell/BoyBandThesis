using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class RelationShipStatManager : MonoBehaviour 
{
	public float value;
	public Text namey;
	public CharacterSpriteController[] characters;

	[YarnCommand("statUp")]
	public void increaseFriendship (string name)
	{
		GlobalManager globe = GameObject.Find ("GlobalStats").GetComponent<GlobalManager> ();
		if (name == "Lee") {
			globe.leeRelationship += value;
			GameObject.Find ("Lee").GetComponent<CharacterSpriteController> ().doFriendEffect (true);
		} else {
			globe.jPeRelationship += value;
			GameObject.Find ("J-Pe").GetComponent<CharacterSpriteController> ().doFriendEffect (true);
		}
	}

	[YarnCommand("statDown")]
	public void decreaseFriendship (string name)
	{
		GlobalManager globe = GameObject.Find ("GlobalStats").GetComponent<GlobalManager> ();
		if (name == "Lee") {
			globe.leeRelationship -= value;
			GameObject.Find ("Lee").GetComponent<CharacterSpriteController> ().doFriendEffect (false);
		} else {
			globe.jPeRelationship -= value;
			GameObject.Find ("J-Pe").GetComponent<CharacterSpriteController> ().doFriendEffect (false);
		}
	}

	[YarnCommand("name")]
	public void changeName (string name)
	{
		for (int i = 0; i < characters.Length; i++) 
		{
			characters [i].speaking = false;
		}
		if (name != "...") {
			namey.text = name;
			CharacterSpriteController csc = GameObject.Find (name).GetComponent<CharacterSpriteController> ();
			if (csc != null) {
				csc.speaking = true;
			}
		} else {
			namey.text = "";
		}
	}
}
