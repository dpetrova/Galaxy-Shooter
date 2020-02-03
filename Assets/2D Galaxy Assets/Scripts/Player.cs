using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool canTripleShot = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField] //keep variable private but visible in the Inspector
    private float _speed = 7.0f;

    //variables needed for cool down system
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;

	// Use this for initialization
	void Start () {
        //current position = new position
        transform.position = new Vector3(0, 0, 0);		
	}
	
	// Update is called once per frame
	void Update () {        
        Movement();

        //if space key/or left mouse is pressed spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();            
        }
    }

    
    private void Movement()
    {
        //--------------------------------------------------------
        // PROCESS PLAYER MOVEMENT
        //--------------------------------------------------------
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertivalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * vertivalInput * Time.deltaTime);

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

    private void Shoot()
    {
        //cool down system
        if (Time.time > _nextFire)
        {
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }            
            
            _nextFire = Time.time + _fireRate;
        }
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        //disable tripleshot mode after 5 sec
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void TripleShotPowerupOn()
    {
        this.canTripleShot = true;
        //start coroutine which disable tripleshot mode after 5 sec
        StartCoroutine(this.TripleShotPowerDownRoutine());
    }
}
