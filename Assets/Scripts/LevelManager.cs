using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField] float sceneDelay = 2f;
    ScoreKeeper scoreKeeper;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame() {
        if (ScoreKeeper.instance != null) {
            ScoreKeeper.instance.ResetScore();
        } else {
            Debug.LogWarning("ScoreKeeper instance not found. Cannot reset score.");
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadEntryPoint() {
        SceneManager.LoadScene("Entry Point");
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad("Game Over", sceneDelay));
    }

    public void QuitGame() {
        Debug.Log("Quiting Game.....");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
