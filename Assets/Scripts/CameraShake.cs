using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    public void StartShake() {
        StartCoroutine(ShakeEffect());
    }

    IEnumerator ShakeEffect() {
        float elapsedTime = 0;
        while(elapsedTime < shakeDuration) {
            transform.position = initialPos + (Vector3)Random.insideUnitCircle * shakeMagnitude; 
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPos;
    }
}
