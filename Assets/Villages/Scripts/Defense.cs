using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	public List< GameObject > weapons = new List< GameObject >();
	public float throwingForce = 75.0f;
	public float attackRateMin = 1.0f;
	public float attackRateMax = 5.0f;
	public float dangerThreshold = 50.0f;

	private bool attacking = false;

	void Update()
	{
		float distFromDragon = Vector3.Distance( transform.position,
			GameObject.FindGameObjectWithTag( "Dragon" ).transform.position );
		bool dragonIsNear = distFromDragon < dangerThreshold ? true : false;

		if( dragonIsNear )
		{
			if( !attacking )
			{
				attacking = true;
				StartCoroutine( FullVillageRepeatAttack() );
			}
		}
		else
		{
			attacking = false;
		}
	}

	private IEnumerator FullVillageRepeatAttack()
	{
		while( transform.childCount > 0 && attacking )
		{
			int attackingChild = Random.Range( 0, transform.childCount - 1 );
			SingleAttack( transform.GetChild( attackingChild ) );
			yield return new WaitForSeconds( Random.Range( attackRateMin, attackRateMax ) );
		}
	}

	private void SingleAttack( Transform building )
	{
		// choose a random weapon from the list and instantiate it
		int randIdx = Random.Range( 0, weapons.Count - 1 );
		GameObject randWeapon = weapons[ randIdx ];
		GameObject weapon = Instantiate( randWeapon, new Vector3( building.position.x,
			building.position.y + 5, building.position.z ), Quaternion.identity );

		// ignore the collision between the building and the weapon
		Collider[] colliders = building.gameObject.GetComponents< Collider >();
		foreach( Collider coll in colliders )
		{
			Physics.IgnoreCollision( weapon.GetComponent<Collider>(), coll );
		}

		// force the weapon in the direction of the dragon
		Vector3 dragonPos = GameObject.FindGameObjectWithTag( "Dragon" ).transform.position;
		Vector3 weaponDest = building.position;
		Rigidbody weaponRb = weapon.GetComponent< Rigidbody >();
		weaponRb.AddForce( ( dragonPos - weaponDest ) * throwingForce );
	}
}
