using UnityEngine;
using System.Collections;



public class ObstacleMoveL : MonoBehaviour {
	
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

		GameObject.Find ("PickUpL").transform.position = new Vector3 (gameObject.transform.position.x - speed * Time.deltaTime, 
		                                                              gameObject.transform.position.y, 
		                                                              gameObject.transform.position.z);


	}
}	
