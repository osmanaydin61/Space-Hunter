using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    Coroutine firingCoroutine;
    [SerializeField] float baseFiringRate =0.2f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minumumFiringRate = 0.1f;

    [HideInInspector]public bool isFiring;
    AudioPlayer audioPlayer;

    void Awake() {
        audioPlayer=FindAnyObjectByType<AudioPlayer>();
    }
    void Start()
    {
        if(useAI)
        {
            isFiring=true;
        }
    }

    
    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if(isFiring&&firingCoroutine==null)
        {
         firingCoroutine = StartCoroutine(FireContinuously());   
        }
        else if(!isFiring && firingCoroutine!=null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine=null;
        }
        
    }
    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab,transform.position,
                                                                quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb!=null)
            {
                rb.velocity = transform.up*projectileSpeed;
            }
            Destroy(instance, projectileLifeTime);
            float timeToNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance, 
                                                    baseFiringRate+firingRateVariance);
            timeToNextProjectile= Mathf.Clamp(timeToNextProjectile,minumumFiringRate,float.MaxValue);
            audioPlayer.PlayShootingClip();
            
            yield return new WaitForSeconds(timeToNextProjectile);
            
        }
    }
}
