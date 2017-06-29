using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryMgr : MonoBehaviour {

	public List<InventoryItem> inventoryObjects = new List<InventoryItem>();
	public int numCells;
	public float height;
	public float width;
	public float yPosition;
	private MissionMgr missionMgr;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("missionMgr");
		if (go)
			missionMgr = go.GetComponent<MissionMgr>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Add(InteractiveObj iObj) {
		ObjectInteraction oi = iObj.OnCloseEnough;

		switch(oi.interactionType) {
		case (ObjectInteraction.InteractionType.Unique):
			Insert(iObj);
			break;

		case (ObjectInteraction.InteractionType.Accumulate):
			bool inserted = false;
			// find object of same type to increase the count.
			CustomGameObject cgo = iObj.gameObject.GetComponent<CustomGameObject>();
			CustomGameObject.CustomObjectType ot = CustomGameObject.CustomObjectType.Invalid;

			if (cgo != null) {
				ot = cgo.objectType;
			}

			if(inventoryObjects.Count == 0){
				Insert(iObj);
			}else{
				for (int i = 0; i < inventoryObjects.Count; i++) {
					CustomGameObject cgoi = inventoryObjects[i].item.GetComponent<CustomGameObject>();
					CustomGameObject.CustomObjectType io = CustomGameObject.CustomObjectType.Invalid;
					if (cgoi != null) {
						io = cgoi.objectType;
					}
					if (ot == io) {
						inventoryObjects[i].quantity++;
						// add token from this object to mission Manager to track it
						MissionToken mt = iObj.gameObject.GetComponent<MissionToken>();
						if (mt != null) {
							missionMgr.Add(mt);
						}
						iObj.gameObject.SetActive(false);
						inserted = true;
					}
				}
			}
			break;
		}
	}
	void Insert(InteractiveObj iObj){
		ObjectInteraction oi = iObj.OnCloseEnough;
		InventoryItem ii = new InventoryItem();
		ii.item = iObj.gameObject;
		ii.quantity = 1;
		ii.displayTexture = oi.tex;
		ii.item.SetActive (false);
		inventoryObjects.Add (ii);
		MissionToken mt = ii.item.GetComponent<MissionToken>();
		if (mt != null)
			missionMgr.Add(mt);
	}

	void DisplayInventory() {
		float sw = Screen.width;
		float sh = Screen.height;
		Texture buttonTexture = null;
		int totalCellsToDisplay = inventoryObjects.Count;

		for (int i = 0; i < totalCellsToDisplay; i++) {
			InventoryItem ii = inventoryObjects [i];
			buttonTexture = ii.displayTexture;
			int quantity = ii.quantity;
			float totalCellLength = sw - (numCells * width);
			float xPosition = totalCellLength - 0.5f * (totalCellLength) + (width * i);
			Rect r = new Rect (xPosition, yPosition * sh, width, height);
			if (GUI.Button (r, buttonTexture)) {
				// to do – handle clicks there
			}
			Rect r2 = new Rect (r.x, r.y, r.width * 0.5f, r.height * 0.5f); 
			string s = quantity.ToString ();
			GUI.Label (r2, s);
		}
	}
	void OnGUI() {
		DisplayInventory();
	}
	
}
