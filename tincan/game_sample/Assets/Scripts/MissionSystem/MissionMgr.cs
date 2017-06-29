using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionMgr : MonoBehaviour {

	public List<Mission> missions;
	public List<MissionToken> missionTokens = new List<MissionToken>();

	public void Add(MissionToken mt) {
		bool uniqueToken = true;
		if (missionTokens != null) {
			for (int i = 0; i < missionTokens.Count; i++) {
				if (missionTokens[i].id == mt.id) {
					// duplicate token found, so abort the insert
					uniqueToken = false;
					break;
				}
			}
		}
		if (uniqueToken){
			missionTokens.Add(mt);
			ValidateAll ();
		}

	}
	public bool Validate(Mission m){
		bool missionComplete = true;
		// a mission with no tokens is always false.
		if (m.tokens.Count <= 0) {
			missionComplete = false;
		}

		for (int i = 0; i < m.tokens.Count; i++) {
			bool tokenFound = false;
			for (int j = 0; j < missionTokens.Count ; j++){
				// if tokens match, tokenFound = true
				if (missionTokens[j] != null && (m.tokens [i].id == missionTokens[j].id)) {
					tokenFound = true;
					break;
				}
			}
			if (!tokenFound){
				missionComplete = false;
				break;
			}
		}

		/*
		if (missionComplete) {
			// get the playerData and add to score.
			GameObject go = GameObject.Find ("Player");
			if (go) {
				PlayerData pd = go.GetComponent<PlayerData>();
				if (pd) {
					pd.AddScore(m.points);
				}
			}
		}*/

		return missionComplete;
	}

	public void ValidateAll() {
		for (int i = 0; i < missions.Count; i++){
			Mission m = missions[i];
			// validate missions…
			if (m.status == Mission.MissionStatus.MS_ForceComplete) {
				m.InvokeReward();
				m.status = Mission.MissionStatus.MS_Invalid;
			}
			// if not completed and not invalid check it.
			if ((m.status != Mission.MissionStatus.MS_Completed)&& m.status != Mission.MissionStatus.MS_Invalid) {
				bool missionSuccess = Validate(m);
				if (missionSuccess) {
					m.InvokeReward();
				}
			}
		}
	}
		
}
