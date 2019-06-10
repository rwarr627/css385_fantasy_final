using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banker : MonoBehaviour {

	private int balance = 0;
	Text balanceDisp;

	// Use this for initialization
	void Start () {
		balanceDisp = gameObject.GetComponent< Text >();
	}

	public int GetBalance()
	{
		return balance;
	}

	public void SetBalance( int newBalance )
	{
		balance = newBalance;
        UpdateText();
    }

    public void AddBalance(int addedBalance)
    {
        balance += addedBalance;
        UpdateText();
    }

    public void UpdateText()
    {
        balanceDisp.text = "$ " + balance.ToString();
    }
}
