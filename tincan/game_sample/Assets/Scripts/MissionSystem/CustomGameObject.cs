using UnityEngine;
using System.Collections;

public class CustomGameObject : MonoBehaviour {

	public CustomObjectType objectType;
	public string displayName;

	public enum CustomObjectType {
		Invalid = -1,
		Unique = 0,
		Coin = 1,
		Ruby = 2,
		Emerald = 3,
		Diamond = 4
	}

	public void validate() {
		if (displayName == "")
			displayName = "unnamed_object";
	}
}