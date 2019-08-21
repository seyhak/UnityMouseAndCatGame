using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTxt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Fading());
	}
	IEnumerator Fading()
    {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
    }
	
}
