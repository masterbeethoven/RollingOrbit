﻿using UnityEngine;
using System.Collections;



public class ObstacleMoveD : MonoBehaviour {
	
	public float speed = .1f;
	public bool isMoving = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Movement();
	}
	
	void Movement(){
		//if(GameObject.Find("PickUpR")){
		
		GameObject.Find ("PickUpD").transform.position = new Vector3 (gameObject.transform.position.x, 
		                                                              gameObject.transform.position.y, 
		                                                              gameObject.transform.position.z-speed * Time.deltaTime);

		
		/*GameObject.Find ("PickUpL").transform.position = new Vector3 (gameObject.transform.position.x - speed * Time.deltaTime, 
			                                                              gameObject.transform.position.y, 
			                                                              gameObject.transform.position.z);*/
		
	}

	void OnTriggerEnter(){
		//Destroy(gameObject);
		
		/*if (collided ==false){
			
			count++;
			// play a randomized pickup sound: use either this or event:/pickup-single
			FMOD_StudioSystem.instance.PlayOneShot("event:/pickup-ran", new Vector3(0,0,0));
			collided =true; 
			if(rend != null){rend.enabled = false;}
		}*/

		Application.LoadLevel("minigame");

	
	}

}
