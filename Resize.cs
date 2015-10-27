using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

    public int MaxFillSize = 0;
	// Use this for initialization
	void Start () 
    {
        float width = Screen.width;
        float height = Screen.height;
        Debug.Log("Screen Height " + height + " Screen width " + width + " " + (width / height));
        Debug.Log(transform.localScale.y + " " + (transform.localScale.y * height)/MaxFillSize);
        Debug.Log(transform.localScale.x * (width / height) / (3.000f / 2.000f));
        transform.localScale = new Vector3(transform.localScale.x, (transform.localScale.y * height) / MaxFillSize);
    }
	
}
