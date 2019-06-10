using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour {

	public float time = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine( Despawn() );
	}

	private IEnumerator Despawn() {
		yield return new WaitForSeconds( time );
		Destroy( gameObject );
	}
}
