
using UnityEngine;

public class FlameThrowerAttack : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public Camera flameCam;
	public ParticleSystem Flamethrower;
	
	// Update is called once per frame
	void Update () {

		//default unity button
		if (Input.GetButtonDown ("Fire1")) 
		{
			Shoot();
		}
		
	}

	void Shoot() 
	{
		Flamethrower.Play ();

		RaycastHit hit;
		if(Physics.Raycast(flameCam.transform.position, flameCam.transform.forward, out hit, range))
		{
			Debug.Log (hit.transform.name);

			Target target = hit.transform.GetComponent<Target>();
			if (target != null) 
			{
				target.TakeDamage(damage);
			}
		}
	}
}
