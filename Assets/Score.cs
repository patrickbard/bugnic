using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ScoreType {
    SCORE,
    MISS
}

public class Score : MonoBehaviour {
    public ScoreType type;
    private TextMeshProUGUI textMeshPro;

    private void Awake() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        switch (type) {
            case ScoreType.SCORE:
                textMeshPro.text = GameManager.Score.ToString();
                break;
            case ScoreType.MISS:
                textMeshPro.text = GameManager.MissedBugs.ToString();
                break;
        }
    }
}