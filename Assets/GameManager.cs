using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score => score;
    public static int MissedBugs => missedBugs;

    private static GameManager instance;
    private static int score = 0;
    private static int scoreIncrement = 5;
    private static int missedBugs = 0;

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
        Time.timeScale = 1;
    }

    public static void addScore() {
        score += scoreIncrement;
    }
    
    public static void reportMissedBug() {
        missedBugs++;
    }


    public static void GameOver() {
        // GameUiManager.openGameoverScreen();
    }
}