using UnityEngine;
using System.Collections;

public class Level2Destroy : MonoBehaviour {

	public static int count = 0; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
			
			count++;
			// play a randomized pickup sound: use either this or event:/pickup-single
			FMOD_StudioSystem.instance.PlayOneShot("event:/pickup-ran", new Vector3(0,0,0));
			Destroy(gameObject);
		
		
		
	}
}
