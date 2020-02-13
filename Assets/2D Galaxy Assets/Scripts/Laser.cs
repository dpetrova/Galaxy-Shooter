using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //destroy the laser when dissapear from the screen
        if(transform.position.y > 6)
        {
            //if is a triple laser that is a parent with 3 laser childs
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
	}
}
