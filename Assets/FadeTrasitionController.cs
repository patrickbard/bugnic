using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrasitionController : MonoBehaviour
{
    // public static FadeTrasitionController instance;
    // private void Awake() {
    //     if (instance == null || instance == this) {
    //         instance = this;
    //     } else {
    //         Destroy(gameObject);
    //     }
    //
    //     DontDestroyOnLoad(this);
    // }

    public void SetIsTransitioning(int trasitioning) {
        LevelLoader.SetIsTransitioning(Convert.ToBoolean(trasitioning));
    }
}
