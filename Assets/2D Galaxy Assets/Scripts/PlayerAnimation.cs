using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator _controller;

	// Use this for initialization
	void Start ()
    {
        this._controller = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//turn left
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            this._controller.SetBool("turn_left", true);
            this._controller.SetBool("turn_right", false);
        }
        //back to idle
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            this._controller.SetBool("turn_left", false);            
        }

        //turn right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            this._controller.SetBool("turn_right", true);
            this._controller.SetBool("turn_left", false);
        }
        //back to idle
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            this._controller.SetBool("turn_right", false);
        }
    }
}
