using UnityEngine;
using System.Collections;

public class ObjectInteraction : MonoBehaviour {

	public enum InteractionAction {
		Invalid = -1,
		PutInInventory = 0,
		Use = 1,
	}

	public enum InteractionType {
		Invalid = -1,
		Unique = 0,
		Accumulate = 1,
	}

	public InteractionAction interaction;
	public InteractionType interactionType;
	public Texture tex;
	private InventoryMgr iMgr;

	public void HandleInteraction(GameObject player){
		if (player) {
			iMgr = player.GetComponent<InventoryMgr> ();
		}
		if (interaction == InteractionAction.PutInInventory) {
			if (iMgr)
				iMgr.Add(this.gameObject.GetComponent<InteractiveObj>());
		}
	}
}
