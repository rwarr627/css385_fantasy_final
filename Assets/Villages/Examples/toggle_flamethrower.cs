using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_flamethrower : MonoBehaviour {

	public GameObject flame;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButton(0) )
		{
			// bool flaming = GameObject.Find( flame.name + "(Clone)" ) != null;
			// print( "FLAMING: " + flaming );
			// if( !flaming )
			// {
			Instantiate( flame, transform, false );
			// }
		}
	}
}
