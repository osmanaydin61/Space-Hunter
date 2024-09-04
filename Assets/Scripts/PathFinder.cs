using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Waves Waves;
    EnemySpawner enemySpawner;
    List<Transform> waypoints;
    int waypointsIndex = 0;

    void Awake() {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }


    void Start()
    {
        Waves = enemySpawner.GetCurrentWave();
        waypoints = Waves.GetWayPoints();
        transform.position = waypoints[waypointsIndex].position;

    }

   
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(waypointsIndex<waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointsIndex].position;
            float delta = Waves.GetMoveSpeed()*Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition,delta);
            if(transform.position==targetPosition)
            {
                waypointsIndex++;
            }
            
        
        }
        else
            {
                Destroy(gameObject);
            }
    }

}
