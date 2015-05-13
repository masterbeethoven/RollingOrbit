using UnityEngine;
using System.Collections;

public class KatamariScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision other) {
		
		if(other.gameObject.tag != "Player")
		{
			Attach(other);
		}
		
	}
	
	void Attach (Collision other)
	{
		if(other.gameObject.GetComponent("Rigidbody") != null)
		{
			//Destroy(other.gameObject.rigidbody);
			//Destroy(GetComponent<Rigidbody>());
			Destroy(other.gameObject.GetComponent<Rigidbody>());
			other.transform.parent = transform;
		}
		
	}
}