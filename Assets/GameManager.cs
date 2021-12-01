using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Score => score;
    public static int MissedBugs => missedBugs;

    private static GameManager instance;
    private static int score = 0;
    private static int scoreIncrement = 5;
    private static int missedBugs = 0;
    private static bool shouldDislayScore;

    public Spawner spawner;
    public List<GameObject> scores;

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
        Time.timeScale = 1;
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (Input.GetKeyDown(KeyCode.D)) {
                shouldDislayScore = !shouldDislayScore;
            }

            foreach (var scoreText in scores) {
                scoreText.SetActive(shouldDislayScore);
            }

            if (missedBugs > 30 && instance.spawner.bugCount > 10) {
                LevelLoader.LoadNextScene();
            }
        }
    }

    public static void RestartGame() {
        LevelLoader.RestartGame();
    }
    
    public static void GoBackToMainMenu() {
        LevelLoader.GoBackToMainMenu();
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