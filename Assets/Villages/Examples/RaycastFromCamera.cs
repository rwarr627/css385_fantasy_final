using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromCamera : MonoBehaviour {

	public int buildingLayer = 8;
	public int dragonLayer = 10;
	public float hitDistance = 50.0f;

	public Transform pointer;

	private int buildingLayerMask;
	private int dragonLayerMask;
	private FlameController[] flames;
	private ColorToggle targetColor;

	// Use this for initialization
	void Start()
	{
		// Bit shift the index of the layers to get a bit mask
	    buildingLayerMask = 1 << buildingLayer;
		dragonLayerMask = ~( 1 << dragonLayer );
		flames = ( FlameController[] )Object.FindObjectsOfType( typeof( FlameController ) );
		targetColor = pointer.gameObject.GetComponent< ColorToggle >();
	}

	// Update is called once per frame
	void Update()
	{
		bool flaming = false;
		RaycastHit hit;

		Physics.Raycast( transform.position,
			                 transform.TransformDirection( Vector3.forward ),
			                 out hit,
							 Mathf.Infinity,
							 dragonLayerMask );


		// move the target to where the camera is looking
		pointer.position = hit.point + Vector3.up;

		if( Input.GetMouseButton( 0 ) && hit.point != Vector3.zero )
		{
			// spitting fire
			flaming = true;
		}

		// check if an object is within firing range
		if( Physics.Raycast( transform.position,
			                 transform.TransformDirection( Vector3.forward ),
			                 out hit,
							 hitDistance,
							 buildingLayerMask ) )
		{
			targetColor.SetSelectedMat();

			// checks if a village building is being hit
			if( flaming )
				BurnTarget( hit.transform.gameObject );
		}
		else
		{
			targetColor.SetNormalMat();
		}

		SpitFire( flaming );
	}

	private void SpitFire( bool flaming )
	{
		foreach( FlameController flame in flames )
		{
			flame.ToggleFlame( pointer, flaming );
		}
	}

	private void BurnTarget( GameObject target )
	{
		BurnableObject hsc = target.GetComponent< BurnableObject >();
		if( hsc != null )
		{
			hsc.OnFire();
		}
	}
}
