using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 1f;
    [Header("Explode")]
    [SerializeField] AudioClip explodeClip;
    [SerializeField] [Range(0f,1f)] float explodeVolume = 1f;
    static AudioPlayer instance;
   
    void Awake() 
    {
        ManageSingleton();
    }    

    void ManageSingleton()
    {
        
        if(instance!=null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else{
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayShootingClip()
    {
        PlayClip(shootingClip,shootingVolume);

    }
     public void PlayExplodeClip()
    {
        PlayClip(explodeClip,explodeVolume);

    }
    void PlayClip (AudioClip clip,float volume)
    {
        if(clip!=null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip,cameraPos,volume);
        }
    }




}
