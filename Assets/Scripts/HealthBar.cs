using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GameObject bg;
    public Texture2D Tex;
    public bool rescale = true;
    // public float health; // = 250;
    // public float maxHealth; // = 250;
    public driver dragon;

	// Use this for initialization
	void Start ()
    {

	}

	// Update is called once per frame
	void Update ()
    {
        float health = dragon.getCurrHealth();
        float maxHealth = dragon.getMaxHealth();

        bg.transform.localScale = new Vector3(0.01f*maxHealth, 0.3f, 1);
        transform.localScale = new Vector3(bg.transform.localScale.x * (health/maxHealth), 0.3f, 1);
        transform.position = new Vector3(bg.transform.position.x, bg.transform.position.y, bg.transform.position.z + 0.01f);
        GetComponent<Image>().color = new Color(1.0f - (health / maxHealth), health / maxHealth, 0, 1);

        // health -= 1.0f;
        // if (health < 0)
        //     health = maxHealth;
    }
}
