using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RelationShipStatManager : MonoBehaviour 
{
	public float value;

	[YarnCommand("statUp")]
	public void increaseFriendship (string name)
	{
		GlobalManager globe = GameObject.Find ("GlobalStats").GetComponent<GlobalManager> ();
		if (name == "Lee") {
			globe.leeRelationship += value;
			GameObject.Find ("Lee").GetComponent<CharacterSpriteController> ().doFriendEffect (true);
		} else {
			globe.jPeRelationship += value;
			GameObject.Find ("Jaypee").GetComponent<CharacterSpriteController> ().doFriendEffect (false);
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
			GameObject.Find ("Jaypee").GetComponent<CharacterSpriteController> ().doFriendEffect (false);
		}
	}
}
