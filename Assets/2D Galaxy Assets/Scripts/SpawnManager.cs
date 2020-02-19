using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerups;

	
	void Start () {

        //spawn eneme every 5 sec
        StartCoroutine(SpawnEnemyRoutine());

        //spawn powerup every 7 sec
        StartCoroutine(SpawnPowerupRoutine());
    }
      

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            Vector3 randomPos = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            Instantiate(this._enemyPrefab, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            int randomPowerup = Random.Range(0, 3); //will return number between 0-2 (exclusive)
            Vector3 randomPos = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            Instantiate(this._powerups[randomPowerup], randomPos, Quaternion.identity);
            yield return new WaitForSeconds(7f);
        }
    }
}
