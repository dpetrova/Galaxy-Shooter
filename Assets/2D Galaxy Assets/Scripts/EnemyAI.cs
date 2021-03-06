﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;

    // Use this for initialization
    void Start () {
        //assign UIManager
        this._uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();        
    }
	
	// Update is called once per frame
	void Update () {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);        

        //--------------------------------------------------------
        // SET ENEMY BOUNDARIES
        //--------------------------------------------------------        
        if (transform.position.y < -7f)
        {
            float posX = Random.Range(-7.8f, 7.8f);
            transform.position = new Vector3(posX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //enemy hit by laser
        if(other.tag == "Laser")
        {
            //destroy triple laser parent
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            //destroy laser
            Destroy(other.gameObject);
            //play explosiaon animation
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            //update player score
            this._uiManager.UpdateScore();
            //play explosion sound at given position at world space
            AudioSource.PlayClipAtPoint(this._clip, Camera.main.transform.position, 1f);
            //AudioSource.PlayClipAtPoint(this._clip, transform.position, 1f);

            //destroy myself (enemy)
            Destroy(this.gameObject);            
        }
        //nrmy hit player
        else if(other.tag == "Player")
        {
            //apply damage to the player
            Player player = other.GetComponent<Player>();
            player.Damage();
            //play explosiaon animation
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            //play explosion sound at given position at world space
            AudioSource.PlayClipAtPoint(this._clip, Camera.main.transform.position, 1f);
            //AudioSource.PlayClipAtPoint(this._clip, transform.position, 1f);

            Destroy(this.gameObject);
        }        
    }
}
