using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    private bool _sawTutorial;
    public GameObject mainMenu;
    public GameObject startScript;
    public GameObject tutorial;
    public GameObject about;
    private float defaultWaitTime = 0.5f;

    private void Awake() {
        if (instance == null || instance == this) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }

        Time.timeScale = 1f;

        mainMenu.SetActive(true);
        startScript.SetActive(false);
        tutorial.SetActive(false);
        about.SetActive(false);
    }
    
    public void setSawTutorial(bool value) {
        _sawTutorial = value;
    }

    public void startGame() {
        StartCoroutine(_startGame());
    }

    private IEnumerator _startGame() {
        if (!_sawTutorial) {
            tutorial.gameObject.SetActive(true);
        }

        while (!_sawTutorial) {
            yield return null;
        }

        mainMenu.gameObject.SetActive(false);
        startScript.gameObject.SetActive(true);
    }
    
    public void startGameDelayed() {
        StartCoroutine(_startGameDelayed(defaultWaitTime));
    }

    private IEnumerator _startGameDelayed(float seconds) {
        yield return new WaitForSeconds(seconds);

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!loadSceneAsync.isDone) {
            yield return null;
        }

        startScript.gameObject.SetActive(false);
    }
}