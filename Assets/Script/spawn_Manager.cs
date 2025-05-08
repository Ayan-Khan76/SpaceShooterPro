using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    [SerializeField]
    private GameObject[] _powerups;

    



    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
               
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyroutine());
        StartCoroutine(TripleShotSpawnRoutine());
        StartCoroutine(LaserSpawnRoutine());
    }

    IEnumerator SpawnEnemyroutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false) 
        {
            float random = Random.Range(2.0f, 4.0f);
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f,8.0f),7,0);
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(random);
        }
        
    }

    IEnumerator TripleShotSpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnpos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            int randompowerUp = Random.Range(0, 4);
            Instantiate(_powerups[randompowerUp],spawnpos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 7));
        }
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    IEnumerator LaserSpawnRoutine()
    {
        yield return new WaitForSeconds(0.5f);
    }
    
}
