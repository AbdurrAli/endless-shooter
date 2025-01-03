using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shoting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance;

    public AudioPlayer GetInstance() {
        return instance;
    }

    void Awake() {
        ManageSingleton();
    }

    void ManageSingleton() {
        // var instances = FindObjectsOfType<AudioPlayer>();
        if (instance != null) {
            gameObject.SetActive(false);
            Destroy(gameObject); 
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShootingAudio()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void DamageAudio()
    {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
