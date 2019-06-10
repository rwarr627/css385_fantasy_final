using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastFire : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position,
			transform.TransformDirection(Vector3.forward), out hit,
			Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position,
				transform.TransformDirection(Vector3.forward) * hit.distance,
				Color.yellow);
            // Debug.Log("Did Hit");

			GameObject foundsParent = hit.transform.parent.gameObject;
            BurnableObject hsc = foundsParent.GetComponent< BurnableObject >();
			if( hsc != null )
			{
				hsc.OnFire();
			}
        }
        else
        {
            Debug.DrawRay(transform.position,
				transform.TransformDirection(Vector3.forward) * 1000,
				Color.white);
            // Debug.Log("Did not Hit");
        }
	}
}
