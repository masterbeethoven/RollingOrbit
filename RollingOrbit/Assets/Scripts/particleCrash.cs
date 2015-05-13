using UnityEngine;
using System.Collections;

public class particleCrash : MonoBehaviour {
	
	//public ParticleSystem pSystem;
	public ParticleEmitter pSystem;
	
	// Use this for initialization
	void Start () {
		
		gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void OnTriggerEnter(){

		Debug.Log ("collider");
		gameObject.GetComponent<ParticleSystem>().enableEmission = true;


		
	}
}

