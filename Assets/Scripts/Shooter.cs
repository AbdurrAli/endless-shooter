using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;


    [Header("Artificial Intelligance")]

    [SerializeField] bool UseAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;


    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        if(UseAI) {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject instace = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instace.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
                 Debug.Log("Projectile Velocity: " + rb.velocity); 
            }
            Destroy(instace, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate  - firingRateVariance, baseFiringRate + firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.ShootingAudio();
            audioPlayer.GetInstance().ShootingAudio();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
