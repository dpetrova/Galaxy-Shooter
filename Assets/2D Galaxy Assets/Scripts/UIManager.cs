using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;

    private Image _livesImage;
    private Text _scoreText;
    private GameObject _titleScreen;

    public int score;

	// Use this for initialization
	void Start () {
        this._livesImage = GameObject.Find("Lives_image").GetComponent<Image>();
        this._scoreText = GameObject.Find("Score_text").GetComponent<Text>();
        this._titleScreen = GameObject.Find("TitleScreen_image");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateLives(int currLives)
    {
        this._livesImage.sprite = this.lives[currLives];
    }

    public void UpdateScore()
    {
        this.score += 10;
        this._scoreText.text = "Score: " + this.score;
    }

    public void ShowTitleScreen()
    {
        this._titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        this._titleScreen.SetActive(false);
        this._scoreText.text = "Score: 0";
    }
}
