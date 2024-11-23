using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;

    public static ScoreKeeper instance { get; private set; }

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

    public int GetScore() {
        return score;
    }

    public void ModifiyScore(int value) {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }

    public void ResetScore() {
        score = 0;
    }
}
