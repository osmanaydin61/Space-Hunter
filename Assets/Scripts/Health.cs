using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

   
    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer=FindAnyObjectByType<AudioPlayer>();
        scoreKeeper=FindAnyObjectByType<ScoreKeeper>();
        levelManager=FindAnyObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }
    public int GetHealth()
    {
        return health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health<=0)
        {
            Die();
        }
    }

    void Die()
    {
        int healthScore = health;
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            
        }
        else{
            levelManager.GameOver();
        }
        Destroy(gameObject);
        
        audioPlayer.PlayExplodeClip();
    }

    void PlayHitEffect()
    {
        if(hitEffect!=null)
        {
            ParticleSystem instance = Instantiate(hitEffect,transform.position,quaternion.identity );
            Destroy(instance.gameObject,instance.main.duration+instance.main.startLifetime.constantMax);
        
        }
    }
    void ShakeCamera()
    {
        if(cameraShake!=null&&applyCameraShake)
        {
            cameraShake.Play();
        }
    }

}
