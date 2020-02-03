using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float _speed = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //access the player script component
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                //setup poweruptriple shot mode
                player.TripleShotPowerupOn();          
            }
            
            //destroy ourself
            Destroy(this.gameObject);
        }
        
    }
}
