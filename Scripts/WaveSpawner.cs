using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{   public enum SpawnState { SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;
    public bool isRandom;
    public float timebetweenWaves = 5f;
    private float wavecountDown;
    public bool triggered;
    private float searchCountdown = 1f;
    public Transform[] spawnPoints;

    public SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        wavecountDown = timebetweenWaves;

    }

    private void Update()
    {
        if (triggered)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyisAlive())
                {
                    //Begin a New Round
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (wavecountDown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    //Start Spawning Wave
                    StartCoroutine(SpawnWave(waves[nextWave]));

                }
            }

            else
            {
                wavecountDown -= Time.deltaTime;
            }

        }
    }

    bool EnemyisAlive()
    {
        //Debug.Log("Checking");

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
            //Debug.Log("All Dead");
                return false;
            }
            
            
        

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        //Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;



        yield break;
    }

    void WaveCompleted()
    {
        //Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        wavecountDown = timebetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
           // if (isRandom)
            
              //  nextWave = 0;
            
            //else
            {
                Destroy(this.gameObject);
            }
            Debug.Log("Completed All Waves. Looping...");
        }
        else
        {
            nextWave++;
            Debug.Log("All Done");
        }
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning Enemy: " + _enemy.name);

        if (isRandom)
        {   //Chooses one random spawn point
            Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
            Instantiate(_enemy, _sp.position, _sp.rotation);

        }
        else
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {  // Spawns at all spawn points
                Transform _sp = spawnPoints[i];
               Instantiate(_enemy, _sp.position, _sp.rotation);
            }
        }
        
    }
}

