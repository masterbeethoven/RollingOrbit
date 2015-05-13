	using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour 
{	
	// these code objects are paired with GUI objects in the Player's Inspector
	// click Player in the Hierarchy, open the Inspector, and voila!
	public GUIText countText;
	public GUIText winText;
	public GUIText velText;
	public GUIText velRollingText;
	public GUIText mhText;
	public GUIText mvText;
	public GUIText level1Text;
	//private float TextTime = 5f;
	public bool obstacle = false; 
	public bool levelCondition = true;

	public GameObject Level1;
	public GameObject Level2; 


	// public variable will appear in Unity inspector for all objects with this attached script
	public float speed;

	// velocity of the ball
	private float pVel;
	private float intensity;
	
	// Unity event for wall impacts
	private FMOD.Studio.EventInstance wallHit;
	// parameter in FMOD "wall-hit-vel" event 
	private FMOD.Studio.ParameterInstance sndVel;
	// Unity event for ball movement
	private FMOD.Studio.EventInstance rollBall;
	// parameter in FMOD for "rolling" event 
	private FMOD.Studio.ParameterInstance rollVel;

	// parameter in FMOD for "Take 2" event 
	private FMOD.Studio.EventInstance music;

	// parameter in FMOD for "Take 2" event 
	private FMOD.Studio.ParameterInstance Layer;


	// variable to hold count value; incremented on each sucessful collection of a "Pickup" or collision with a cube
	private int count;


	// function executes when game begins
	void Start ()
	{
			
		level1Text.text = "Level 1";
		/*if (TextTime >=5f)
		{
			Destroy(level1Text);
		}*/
		// initialize count at 0
		count = 0;
		intensity = 0;
		// call function to update GUIText field with count details
		SetCountText ();
		// set winning text field to be blank
		winText.text = "";

		// link the rolling sound event to rollBall EventInstance
		rollBall = FMOD_StudioSystem.instance.GetEvent("event:/rolling");


		music = FMOD_StudioSystem.instance.GetEvent("event:/Take 2");


		// ensures that roll will always have SOME value, i.e. not be null
		if (rollBall.getParameter("rollVel", out rollVel) != FMOD.RESULT.OK){
			Debug.LogError("rollVel parameter not found on rollBall sound event");
			return;
		}

		if (music.getParameter("Layer", out Layer) != FMOD.RESULT.OK){
			Debug.LogError("rollVel parameter not found on rollBall sound event");
			return;
		}
		

		// begin rollBall EventInstance
		rollBall.start ();
		music.start ();




	}

	// use this update for changes that require physics
	void FixedUpdate()
	{



		// grabs player input from the keyboard and transfers to Rigidbody physics component
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// report details of H and V movement
		mhText.text = "Horz: " + moveHorizontal.ToString();
		mvText.text = "Vert: " + moveVertical.ToString();

		// new Vector3 variable uses keyboard input for its values: x = horiz key value, y= 0.0f (ball doesn't move up) and z = vert key value
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// applies input and modulates with speed value; Time.deltaTime smoothes the whole process
		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);

		// get velocity value (magnitude converts to a floating point number)
		pVel = gameObject.GetComponent<Rigidbody>().velocity.magnitude;

		// report current velocity. This value lets you know what range to use for FMOD event parameters related to ball movement and speed/velocity
		velRollingText.text = "Rolling at: " + pVel.ToString ();

		// use velocity to set volume of rolling and x-fade position for various roll sounds
		rollVel.setValue(pVel);


		//Uncomment this line to assign music layers to velocity
		Layer.setValue(pVel/15);

	}



	void OnTriggerEnter (Collider other) 
	{
		// if the tag matches "PickUp", make it inactive
		// tags must be set in the Unity editor Tag List; tag is applied to the Prefab so that all instances are tagged identically
		if(other.gameObject.tag == "PickUp")
		{
			// hides the cube
			//other.gameObject.SetActive(false);
			// increments the count by 1
			//count++;
			//intensity++;
			//Layer.setValue(intensity/15);

			// function updates the count text field 
			SetCountText ();

				if(GameObject.Find("Level2")){
					
					Debug.Log ("here!");
					//new SinScript(other.gameObject.tag == "PickUp");
					SinScript sinScript =GetComponent<SinScript>();
					sinScript.SendMessage("Update");
					
				//sinScript.Update();
 
				}

			// play a fixed pickup sound: use either this or event:/pickup-ran
			//FMOD_StudioSystem.instance.PlayOneShot("event:/pickup-single", new Vector3(0,0,0));

			// play a randomized pickup sound: use either this or event:/pickup-single
			//FMOD_StudioSystem.instance.PlayOneShot("event:/pickup-ran", new Vector3(0,0,0));
		}



		/*if (obstacle == true)
		{
			Debug.Log("obstacale");

			//do not count, count is equal to previous count number. 
			count = count-1;

		}

		else
		{
			//count++;
			//um why is it two plus now
		}*/

		// same as tag comparison for "PickUp"
		if(other.gameObject.tag == "Wall")
		{	
			// reports velocity on moment of impact with a wall
			//velText.text = "Velocity: " + pVel.ToString();
			// play a single collide sound
			// NOTE: to do this, each wall contains a duplicate object that has "Is Trigger" checked
			//       Unity will not allow objects with Is Trigger to retain their collision physics     
			//FMOD_StudioSystem.instance.PlayOneShot("event:/pickup-hit", new Vector3(0,0,0));

			// play a collide sound based on velocity
			// this uses sndVel as a parameter in the FMOD timeline of the wall-hit-vel event
			wallHit = FMOD_StudioSystem.instance.GetEvent("event:/wall-hit-vel");

			// ensures that sndVel will always have SOME value, i.e. not be null
			if (wallHit.getParameter("sndVel", out sndVel) != FMOD.RESULT.OK){
				Debug.LogError("sndVel parameter not found on wallHit event");
				return;
			}

			// retrieve current velocity and apply to FMOD parameter sndVel (this chooses the right sound given a velocity value)
			sndVel.setValue(pVel);
			// play the sound
			wallHit.start();
		}

		if(other.gameObject.tag == "WinTrigger"){

			music.stop (0);
			winText.text = "You prevail in death!";
			// play a victory cue after all 12 cubes are collected
			FMOD_StudioSystem.instance.PlayOneShot("event:/win", new Vector3(0,0,0));
		}
	}

	// function to update the counter text
	void SetCountText ()
	{
		// "prints" new value to the GUI text object
		countText.text = "Count: " + destroyObj.count.ToString();
		// if all 12 PickUps have been collected, report a win message
		//if (destroyObj.count >= 24) 
		if (destroyObj.count >= 12) 
		{
			Destroy(GameObject.Find("Level1"));
				
			{
					//music.stop (0);
					//winText.text = "You prevail in death!";
					
			}
			//Instantiation gave strange behavior
			//GameObject instance = Instantiate(Resources.Load("Level2", typeof(GameObject))) as GameObject;
			//Instantiate(Level2,new Vector3 (4.747057f, 0.5f,-1.820937f), Quaternion.identity);
			// play a victory cue after all 12 cubes are collected
			FMOD_StudioSystem.instance.PlayOneShot("event:/win", new Vector3(0,0,0));
		}
	}
}