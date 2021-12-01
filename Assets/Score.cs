using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScoreType {
    SCORE,
    MISS
}

public class Score : MonoBehaviour {
    public ScoreType type;
    private TextMeshProUGUI textMeshPro;
    private bool shouldDislay;

    private void Awake() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        switch (type) {
            case ScoreType.SCORE:
                string baseText = "Score: ";

                if (SceneManager.GetActiveScene().buildIndex == 2) {
                    baseText = "";
                }

                textMeshPro.text = baseText + GameManager.Score;
                break;
            case ScoreType.MISS:
                textMeshPro.text = "Missed: " + GameManager.MissedBugs;
                break;
        }
    }
}