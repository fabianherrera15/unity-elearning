
using UnityEngine;
using System.Collections;

public class DeathTriggerC : MonoBehaviour {
    public int deathcount = 0; 
    public bool accepting = true;
    public void OnTriggerEnter (Collider other  ) {
	    other.gameObject.SendMessage ("OnDeath", SendMessageOptions.DontRequireReceiver);
	    var sbd = new submitDataBlock("Character Died!", "Died","<test></test>");
        if (accepting)
        {
            if(deathcount > 0)
                GameObject.Find("EleContainer").SendMessage("AddInteraction", sbd, SendMessageOptions.DontRequireReceiver);
            else
                GameObject.Find("EleContainer").SendMessage("AddInteraction", new submitDataBlock("Game Start!", "Start", "<test></test>"), SendMessageOptions.DontRequireReceiver);

        }
        deathcount++;
        accepting = false;
    }

    // Helper function: Draw an icon in the sceneview so this object gets easier to pick
    void OnDrawGizmos () {
	    Gizmos.DrawIcon (transform.position, "Skull And Crossbones Icon.tif");
    }
    void BeginAcceptingNewDeath()
    {
        accepting = true;
    }
}