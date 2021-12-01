using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameOverReason {
    TOO_MANY_ESCAPED, INACTIVITY
}

public class GameManager : MonoBehaviour
{
    public static int Score => score;
    public static int MissedBugs => missedBugs;
    public static GameOverReason gameOverReason;
    public static bool isPaused;
    
    private static GameManager instance;
    private static int score = 0;
    private static int scoreIncrement = 5;
    private static int missedBugs = 0;
    private static bool shouldDislayMissed;
    private static Spawner _spawner;
    private static GameObject _pauseGameObjet;

    public Spawner spawner;
    public GameObject missedText;
    public GameObject pauseGameObjet;

    private void Awake() {
        if (instance == null || instance == this) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        Debug.Log("Scene reloaded");
        init();
        Debug.Log("Variables initialized!");
    }

    void init() {
        score = 0;
        missedBugs = 0;
        isPaused = false;
        Time.timeScale = 1;
        _spawner = spawner;
        _pauseGameObjet = pauseGameObjet;
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            // if (Input.GetKeyDown(KeyCode.D)) {
            //     shouldDislayMissed = !shouldDislayMissed;
            // }
            //
            // if (shouldDislayMissed) {
            //     missedText.SetActive(true);
            // }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
                if (isPaused) {
                    ResumeGame();
                } else {
                    PauseGame();
                }
            }

            if (missedBugs > 30) {
                EndGame();
            }
        }
    }

    public static void PauseGame() {
        isPaused = true;
        Time.timeScale = 0;
        _pauseGameObjet.SetActive(true);
    }
    
    public static void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1;
        _pauseGameObjet.SetActive(false);
    }

    public static void RestartGame() {
        LevelLoader.RestartGame();
    }
    
    public static void GoBackToMainMenu() {
        LevelLoader.GoBackToMainMenu();
    }
    
    public static void EndGame() {
        gameOverReason = score > 300 ? GameOverReason.TOO_MANY_ESCAPED : GameOverReason.INACTIVITY;
        LevelLoader.LoadNextScene();
    }

    public static void addScore() {
        score += scoreIncrement;
    }
    
    public static void addScore(int customAmount) {
        score += customAmount;
    }
    
    public static void reportMissedBug() {
        missedBugs++;
    }


    public static void GameOver() {
        // GameUiManager.openGameoverScreen();
    }
}