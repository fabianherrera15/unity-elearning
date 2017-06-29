using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mission {

	public enum MissionStatus {
		MS_Invalid = -1,
		MS_Acquired = 0,
		MS_InProgress = 1,
		MS_Completed = 2,
		MS_ForceComplete = 3
	};
	public MissionStatus status;
	public List<MissionToken> tokens;
	public bool activated;
	public bool visible;
	public int points;
	public GameObject reward;
	public string displayName;
	public string description;

	public void InvokeReward() {
		// if the mission is finished, instatiate the reward callback
		if (reward != null) {
			GameObject.Instantiate(reward);
			this.activated = false;
			this.visible = false;
			this.status = MissionStatus.MS_Completed;
		}
	}
}
