using System.Collections;
using PathCreation.Examples;
using UnityEngine;

public class Bug : MonoBehaviour {
    private PathFollower pathFollower;
    public GameObject randomPath;
    [HideInInspector]
    public Spawner spawner;
    public bool isVisible;

    private void Awake() {
        pathFollower = GetComponent<PathFollower>();
        randomPath = Instantiate(randomPath);
        pathFollower.pathCreator = randomPath.GetComponent<RandomPath>().instantiatedPath;
    }

    void Update() {
        
    }
    
    
    private void OnBecameVisible() {
        isVisible = true;
    }

    private void OnBecameInvisible() {
        if (gameObject.activeSelf) {
            isVisible = false;
            StartCoroutine(miss());
        }
    }

    private IEnumerator miss() {
        yield return new WaitForSeconds(0.1f);

        if (!isVisible) {
            GameManager.reportMissedBug();
        }
    }

    public void Die() {
        spawner.bugCount--;
        GameManager.addScore();
        GetComponent<Animator>().SetBool("dead",true);
        Destroy(GetComponent<Rigidbody2D>());
        // Destroy(gameObject);
        Destroy(randomPath);
    }

    public void DestroyBug() {
        Destroy(gameObject);
    }
}