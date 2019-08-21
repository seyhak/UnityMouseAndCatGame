using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouAreDead : MonoBehaviour {
    Text text;
    float textSpeed = 0.01f;
    // Use this for initialization
    void Start () {
       text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.color = Color.Lerp(text.color, Color.black, textSpeed);
	}
}
