using UnityEngine;
using System.Collections;



public class ObstacleMoveR : MonoBehaviour {

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
			
			GameObject.Find ("PickUpR").transform.position = new Vector3 (gameObject.transform.position.x + speed * Time.deltaTime, 
			                                                  gameObject.transform.position.y, 
			                                                  gameObject.transform.position.z);
			Debug.Log ("right");

			/*GameObject.Find ("PickUpL").transform.position = new Vector3 (gameObject.transform.position.x - speed * Time.deltaTime, 
			                                                              gameObject.transform.position.y, 
			                                                              gameObject.transform.position.z);*/
			
		}



	}
	
