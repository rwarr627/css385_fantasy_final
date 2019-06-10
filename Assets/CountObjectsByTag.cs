using UnityEngine;
using UnityEngine.UI;

public class CountObjectsByTag : MonoBehaviour
{

    public string tag;
    public string postText;
    Text disp;
    private int totalCount;

	// Use this for initialization
	void Start ()
    {
        disp = gameObject.GetComponent<Text>();
        totalCount = GameObject.FindGameObjectsWithTag(tag).Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.disp.text = (totalCount - GameObject.FindGameObjectsWithTag(tag).Length).ToString() + '/' + totalCount + (string.IsNullOrEmpty(postText) ? "" : ('\n' + postText));
    }
}
