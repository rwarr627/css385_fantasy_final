using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {

    public int price = 1000;
    public GameObject upgradedVersion;
    public string playerTag = "Dragon";
    Banker banker;
    Text priceText;
    //GameObject gameCamera;
    public bool unlocked = false;

	// Use this for initialization
	void Start () {
        priceText = gameObject.transform.Find("Price").gameObject.GetComponent<Text>();
        priceText.text = "$" + price;
        banker = GameObject.FindGameObjectWithTag("Bank").GetComponent<Banker>();
        //gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        if (unlocked)
        {
            priceText.color = Color.green;
            priceText.text = "Unlocked";
        }
    }

    public void OnClick()
    {
        if (upgradedVersion == null)
            return;
        float healthpercentage = 1.0f;
        bool firstUnlock = false;
        if (!unlocked)
        {
            if (banker.GetBalance() < price)
                return;
            banker.AddBalance(-price);
            priceText.color = Color.green;
            priceText.text = "Unlocked";
            firstUnlock = true;
            unlocked = true;
        }
        Transform trans = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(playerTag))
        {
            if (obj.activeInHierarchy)
            {
                trans = obj.transform;
                if(!firstUnlock)
                    healthpercentage = obj.GetComponent<driver>().getHealthPercentage();
            }
            obj.SetActive(false);
        }
        // ensure keeping position and rotation
        upgradedVersion.transform.position = trans.position;
        upgradedVersion.transform.rotation = trans.rotation;
        upgradedVersion.GetComponent<driver>().setHealthPercentage(healthpercentage);
        upgradedVersion.SetActive(true);
        GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>().dragon = upgradedVersion.GetComponent<driver>();
    }
}
