using System.Collections;
using PathCreation.Examples;
using UnityEngine;

public class Bug : MonoBehaviour {
    private PathFollower pathFollower;
    public GameObject randomPath;
    [HideInInspector]
    public Spawner spawner;
    public bool isVisible;
    private Animator animator;
    private Rigidbody2D rigidbody;

    private void Awake() {
        pathFollower = GetComponent<PathFollower>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
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
        animator.SetBool("dead",true);
        
        Destroy(rigidbody);
        Destroy(randomPath);
    }

    public void DestroyBug() {
        Destroy(gameObject);
    }
}