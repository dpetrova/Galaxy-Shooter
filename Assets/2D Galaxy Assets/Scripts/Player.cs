using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject laserPrefab;

    [SerializeField] //keep variable private but visible in the Inspector
    private float speed = 5.0f;    

	// Use this for initialization
	void Start () {
        //current position = new position
        transform.position = new Vector3(0, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();

        //if space key is pressed spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
        }
    }

    
    private void Movement()
    {
        //--------------------------------------------------------
        // PROCESS PLAYER MOVEMENT
        //--------------------------------------------------------
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertivalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * vertivalInput * Time.deltaTime);

        //--------------------------------------------------------
        // SET PLAYER BOUNDARIES
        //--------------------------------------------------------
        //if player on the Y is > 0 -> set player position on the Y to 0
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        //if player on the Y is < -4.2 -> set player position on the Y to -4.2
        else if (transform.position.y <= -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //wrapping feature for moving on X -> when exit from the left, enter from the right and opposite
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }
}
