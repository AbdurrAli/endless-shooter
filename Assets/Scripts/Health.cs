using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50 ;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    LevelManager levelManager;

    void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();    
    }

     void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null) {
            TakeDamage(damageDealer.GetDamage());
            HitEffect();
            audioPlayer.DamageAudio();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    public int GetHealth() {
        return health;
    }

    void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    void Die() {

        if(!isPlayer) {
            scoreKeeper.ModifiyScore(score);
        } else {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void HitEffect() {
        if (hitEffect != null) {
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        float totalDuration = instance.main.duration + instance.main.startLifetime.constantMax;
        Destroy(instance.gameObject, totalDuration);
        }
    }

    void ShakeCamera() {
        if (cameraShake != null && applyCameraShake) {
            cameraShake.StartShake();
        }
    }
}
