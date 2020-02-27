using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;


    void Start () {

        this._gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
    }

    public void StartSpawnRoutines()
    {
        //spawn eneme every 5 sec
        StartCoroutine(SpawnEnemyRoutine());
        //spawn powerup every 7 sec
        StartCoroutine(SpawnPowerupRoutine());
    }
      

    private IEnumerator SpawnEnemyRoutine()
    {
        while (this._gameManager.gameOver == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            Instantiate(this._enemyPrefab, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (this._gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3); //will return number between 0-2 (exclusive)
            Vector3 randomPos = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            Instantiate(this._powerups[randomPowerup], randomPos, Quaternion.identity);
            yield return new WaitForSeconds(7f);
        }
    }
}
