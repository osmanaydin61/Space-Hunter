using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Config",fileName ="New Wave Config")]
public class Waves : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefanbs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns=1f;
    [SerializeField] float spawnTimeVariance =0f;
    [SerializeField] float minumumSpawnTime = 0.2f;


    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWayPoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public int GetEnemyCount()
    {
        return enemyPrefanbs.Count;
    }
    public GameObject GetEnemyPrefabs(int index)
    {
        return enemyPrefanbs[index];
    }
    public float GetRandomSpawnTime()
    {
        float spawnTime =Random.Range(timeBetweenEnemySpawns-spawnTimeVariance,timeBetweenEnemySpawns+spawnTimeVariance);
        return Mathf.Clamp(spawnTime,minumumSpawnTime,float.MaxValue);
    }

}
