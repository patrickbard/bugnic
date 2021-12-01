using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSelection : MonoBehaviour {
    public GameObject gameOverMain;
    public GameObject gameOverByInactivity;

    private void Awake() {
        Time.timeScale = 1f;
        
        switch (GameManager.gameOverReason) {
            case GameOverReason.INACTIVITY:
                gameOverMain.SetActive(false);
                gameOverByInactivity.SetActive(true);
                break;
            case GameOverReason.TOO_MANY_ESCAPED:
                gameOverMain.SetActive(true);
                gameOverByInactivity.SetActive(false);
                break;
        }
    }
}
