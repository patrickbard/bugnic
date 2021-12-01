using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{    
    public static LevelLoader instance;

    private void Awake() {
        if (instance == null || instance == this) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }
    
    public static void RestartGame() {
        SceneManager.LoadScene(1);
    }
    
    public static void GoBackToMainMenu() {
        SceneManager.LoadScene(0);
    }
    public static void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static AsyncOperation LoadSceneAsync() {
        return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
