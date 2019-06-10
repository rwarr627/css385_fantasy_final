using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flame_controller : MonoBehaviour
{

	public Transform flameDestination;

	// Use this for initialization
	void Start ()
	{
		foreach( Transform child in transform )
		{
			child.gameObject.GetComponent< ParticleSystem >().Stop();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if( Input.GetMouseButton(0) )
		{
			transform.LookAt( flameDestination );
			foreach( Transform child in transform )
			{
				child.gameObject.GetComponent< ParticleSystem >().Play();
			}
		}
		else
		{
			foreach( Transform child in transform )
			{
				child.gameObject.GetComponent< ParticleSystem >().Stop();
			}
		}
	}
}
