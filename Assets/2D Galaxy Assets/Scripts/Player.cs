using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool canTripleShot = false;

    public bool isSpeedBoostActive = false;
    public bool isShieldActive = false;

    [SerializeField]
    private GameObject _explosionPrefab;

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

    private int lives = 3;

    private GameObject _shield;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    // Use this for initialization
    void Start () {
        //current position = new position
        transform.position = new Vector3(0, 0, 0);

        //find child shield
        this._shield = transform.Find("Shield_sprite").gameObject;        
        if (this._shield != null) this._shield.SetActive(false);

        //assign UIManager
        this._uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (this._uiManager != null) this._uiManager.UpdateLives(this.lives);

        //assign GameManager
        this._gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //assign SpawnManager
        this._spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        this._spawnManager.StartSpawnRoutines();

        //assign AudioSource
        this._audioSource = GetComponent<AudioSource>();
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

        if(isSpeedBoostActive)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed *1.5f * vertivalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * vertivalInput * Time.deltaTime);
        }
        
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
        if (Time.time > this._nextFire)
        {
            if (this.canTripleShot)
            {
                Instantiate(this._tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(this._laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }

            //play laser sound
            this._audioSource.Play();

            this._nextFire = Time.time + this._fireRate;
        }
    }


    private IEnumerator TripleShotPowerDownRoutine()
    {
        //disable tripleshot mode after 5 sec
        yield return new WaitForSeconds(5.0f);
        this.canTripleShot = false;
    }


    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        //disable speed boost after 5 sec
        yield return new WaitForSeconds(5.0f);
        this.isSpeedBoostActive = false;
    }


    private IEnumerator ShieldPowerDownRoutine()
    {
        //disable speed boost after 60 sec
        yield return new WaitForSeconds(60.0f);
        this.isShieldActive = false;
        this._shield.SetActive(false);
    }


    public void TripleShotPowerupOn()
    {
        this.canTripleShot = true;
        //start coroutine which disable tripleshot mode after 5 sec
        StartCoroutine(this.TripleShotPowerDownRoutine());
    }    


    public void SpeeedBoostPowerupOn()
    {
        this.isSpeedBoostActive = true;
        StartCoroutine(this.SpeedBoostPowerDownRoutine());
    }


    public void ShieldPowerupOn()
    {
        this.isShieldActive = true;
        this._shield.SetActive(true);
        StartCoroutine(this.ShieldPowerDownRoutine());
    }


    public void Damage()
    {
        //check if shield is active
        if(this.isShieldActive == true)
        {
            this.isShieldActive = false;
            this._shield.SetActive(false);
            return;
        }

        //substract 1 life
        this.lives--;
        //update lives UI
        this._uiManager.UpdateLives(this.lives);

        //if no lives destroy player
        if (this.lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //show title screen
            this._uiManager.ShowTitleScreen();
            //game over
            this._gameManager.gameOver = true;

            Destroy(this.gameObject);
        }
    }
}
