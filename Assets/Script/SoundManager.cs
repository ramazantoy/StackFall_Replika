using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance2;//level değiştiğinde tekrar tekrar yüklenmesini önlemek amacıyla
    private AudioSource audioSource;
    public bool sound;
    void Start()
    {
        if (sound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

    }
    private void Awake()
    {
        makeSingle();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

    }
    void makeSingle()
    {
        if (instance2 != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance2 = this;
            DontDestroyOnLoad(this);
        }
    }
    public void soundOnOff()
    {
        sound = !sound;
        if (sound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    
    public void playSoundFx(AudioClip clip,float volume)
    {
        if (audioSource != null)
        {
            if (sound)
            {
                audioSource.PlayOneShot(clip, volume);
            }
        }
     
    }
}
