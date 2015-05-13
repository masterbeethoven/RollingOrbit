using UnityEngine;
using System.Collections;

public class destroyObj : MonoBehaviour {
	public Renderer rend; 
	bool collided = false; 
	public static int count = 0; 
	
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = true; 
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(){
		//Destroy(gameObject);

		if (collided ==false){

			count++;
			// play a randomized pickup sound: use either this or event:/pickup-single
			FMOD_StudioSystem.instance.PlayOneShot("event:/pickup-ran", new Vector3(0,0,0));
			collided =true; 
			if(rend != null){rend.enabled = false;}
		}

		
	}

	/*void OnTriggerEnter(Collider other){
		//Destroy(gameObject);
		rend.enabled = false;
		other.gameObject.SetActive(false);
	}*/
}
