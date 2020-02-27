using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public GameObject palyerPrefab;

    private UIManager _uiManager;

    // Use this for initialization
    void Start () {
        //assign UIManager
        this._uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //show title screen
        this._uiManager.ShowTitleScreen();
    }
	
	// Update is called once per frame
	void Update () {

        //if game is over
		if(gameOver)
        {
            //listen to space key
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //spawn the player
                Instantiate(this.palyerPrefab, Vector3.zero, Quaternion.identity);
                //hide title screen
                this._uiManager.HideTitleScreen();

                gameOver = false;
            }
        }
	}
}
