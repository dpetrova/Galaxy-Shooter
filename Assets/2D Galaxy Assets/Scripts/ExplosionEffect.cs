using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    private float delay = 4f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
