using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerupID; //0 -> triple shot; 1 -> speed boost; 2 -> shields

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
                if(powerupID == 0)
                {
                    //setup poweruptriple shot mode
                    player.TripleShotPowerupOn();
                }
                else if(powerupID == 1)
                {
                    //set speed boost
                    player.SpeeedBoostPowerupOn();
                }
                else if(powerupID == 2)
                {
                    //set shields
                }
            }
            
            //destroy ourself
            Destroy(this.gameObject);
        }
        
    }
}
