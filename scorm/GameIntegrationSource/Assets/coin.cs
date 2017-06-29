using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {

    GameObject character;
    public AudioClip impact;
	// Use this for initialization
	void Start () {
       
	}
    void Stop()
    {
       
    }
	// Update is called once per frame
	void Update () {
        this.transform.rotation *= new Quaternion(1f,0f,0f,.5f * Time.deltaTime);        
	}
    void OnTriggerEnter(Collider other)
    {
       
        AudioSource.PlayClipAtPoint(impact,other.transform.position,1500);
        this.collider.active = false;
        
       // audio.PlayOneShot(impact);
        Destroy(this.gameObject);
        GameObject.Find("EleContainer").SendMessage("AddInteraction", new submitDataBlock("Character got a coin!", "Coin", ""),SendMessageOptions.DontRequireReceiver);
    }
}
