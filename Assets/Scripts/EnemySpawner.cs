using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Waves> Waves;
    [SerializeField] float timeBetweenWaves = 0f;
    Waves currentWave;
    [SerializeField] bool isLooping = true;
    void Start()
    {
        StartCoroutine(SpawnEnemysWaves());
    }
    public Waves GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemysWaves()
    {
        do
        {

        
        foreach(Waves wave in Waves)
        {
            currentWave = wave;
            for(int i=0;i<currentWave.GetEnemyCount();i++)
        {
                    Instantiate(currentWave.GetEnemyPrefabs(i),
                    currentWave.GetStartingWaypoint().position,Quaternion.Euler(0,0,180)
                    ,transform);

            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
        yield return new WaitForSeconds(timeBetweenWaves);
        }
        
        }
        while(isLooping);
    }
   
}
