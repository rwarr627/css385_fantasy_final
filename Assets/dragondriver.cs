using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragondriver : MonoBehaviour {

	Animator anim;

	public float speed = 10.0f;
    public float maxSpeed = 100.0f;
    public float flapSpeed = 50.0f;
    public float maxFlapSpeed = 100.0f;
    public float rotationSpeed = 100.0f;

	void Start()
	{
		anim = this.GetComponent<Animator> ();
	}


	void FixedUpdate()
	{
		// Get the horizontal and vertical axis.
		// By default they are mapped to the arrow keys.
		// The value is in the range -1 to 1
	    float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * 0.1f;

        // Make it move 10 meters per second instead of 10 meters per frame...
        //translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        if ((translation > 0 && GetComponent<Rigidbody>().velocity.z < maxSpeed) || (translation < 0 && (GetComponent<Rigidbody>().velocity.z * -1) > (maxSpeed * -1)))
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

		if (Input.GetKey ("space")) 
		{
			anim.SetBool ("isFlapping", true);
            this.GetComponent<Rigidbody>().useGravity = false;
            if (GetComponent<Rigidbody>().velocity.y < maxFlapSpeed)
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0, flapSpeed, 0));
        }
		else 
		{
			anim.SetBool("isFlapping", false);
            this.GetComponent<Rigidbody>().useGravity = true;
        }

		if (rotation != 0) {
			anim.SetBool ("goStraight", false);
			this.transform.Rotate (0, rotation, 0);
			if (rotation < 0)
				anim.SetBool ("turnLeft", true);
			else
				anim.SetBool ("turnRight", true);
		} 

		//without rotation go straight
		else 
		{
			anim.SetBool ("goStraight", true);
			anim.SetBool ("turnLeft", false);
			anim.SetBool ("turnRight", false);
		}

		//transform.Translate (0, -0.1f, 0);
	}
}
