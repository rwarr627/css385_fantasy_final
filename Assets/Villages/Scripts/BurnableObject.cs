using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
	public GameObject fire;		// TODO: make a particle system
	public int health = 150;
	public int burnDamagePerSec = 5;
	public int burnTime = 10;
    public int burnMoney = 2;
    public int monetaryReward = 500;
    public int burnHealth = 0;
    public int healReward = 0;

    private bool onFire = false;
	private int currBurnCount = 0;
	private Banker bank;

    private driver Dragon;

	// Use this for initialization
	void Start ()
	{
		GameObject bankObj = GameObject.FindGameObjectWithTag( "Bank" );
        bank = bankObj.GetComponent<Banker>();
        Dragon = GameObject.FindGameObjectWithTag("Dragon").GetComponent<driver>();
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        Dragon = GameObject.FindGameObjectWithTag("Dragon").GetComponent<driver>();
    }

	public void OnFire()
	{
		if( !onFire )
		{
			onFire = true;
			Instantiate( fire, transform, false );
			StartCoroutine( Burn() );
		}

		currBurnCount = 0;
		health -= Dragon.damage;
		addMoney(burnMoney * Dragon.damage);
        Dragon.heal(burnHealth * Dragon.damage);

        if ( health <= 0 )
		{
			BurnDown();
		}
	}

	private IEnumerator Burn()
	{
		while( currBurnCount < (burnTime * Dragon.damage))
		{
            health -= burnDamagePerSec * Dragon.damage;
			//addMoney(burnMoney * Dragon.damage);
            //Dragon.heal(burnHealth * Dragon.damage);
            yield return new WaitForSeconds( 1 );

			currBurnCount++;
		}

		currBurnCount = 0;
		// TODO: turn off fire particles
	}

	private void BurnDown()
    {
		// TODO: Play explosion particle
		Destroy( gameObject );
		addMoney( monetaryReward );
        Dragon.heal(healReward);
    }

	private void addMoney( int amount )
	{
		int balance = bank.GetBalance();
		bank.SetBalance( balance + amount );
	}
}
