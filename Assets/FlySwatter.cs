using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FlySwatter : MonoBehaviour {
    private Animator animator;
    private HashSet<GameObject> potentialTargets;
    private bool canHit = true;

    public GameObject doubleKillObj;
    public GameObject tripleKillObj;
    public GameObject juicyKillObj;

    private void Awake() {
        animator = GetComponent<Animator>();
        potentialTargets = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.isPaused) {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // var position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
            transform.position = Vector2.Lerp(transform.position, position, 0.5f);
            var transformPosition = transform.position;
            // transform.Translate( position * (10 * Time.deltaTime));
        
            if (transformPosition.x < -5.5f) {
                transform.position = Vector2.Lerp(new Vector2(-5.5f, transformPosition.y), position, 0.5f);
            }
        
            if (transformPosition.y > 5.5f) {
                transform.position = Vector2.Lerp(new Vector2(transformPosition.x, 5.5f), position, 0.5f);
            }
        
            if (canHit && Input.GetMouseButtonDown(0)) {
                animator.SetTrigger("hit");
            }
        }
    }

    public void SetCanHit(int ableToHit) {
        canHit = Convert.ToBoolean(ableToHit);
    }

    public void playFlySwatterSound() {
        AudioManager.PlayRandomFlySwatterSound();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        potentialTargets.Add(other.gameObject);
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        potentialTargets.Remove(other.gameObject);
    }
    
    private void testFunction() {
        if (potentialTargets.Count > 0) {

            GameObject killingText = null;
            int extraPoints = 0;

            if (potentialTargets.Count == 2) {
                killingText = Instantiate(doubleKillObj, gameObject.transform.position, Quaternion.identity);
                extraPoints = 20;
            }

            if (potentialTargets.Count == 3) {
                killingText = Instantiate(tripleKillObj, gameObject.transform.position, Quaternion.identity);
                extraPoints = 50;
            }

            if (potentialTargets.Count > 3) {
                killingText = Instantiate(juicyKillObj, gameObject.transform.position, Quaternion.identity);
                extraPoints = 100;
            }

            StartCoroutine(DestroyKillingText(killingText));
            

            foreach (var target in potentialTargets.ToList()) {
                Debug.Log(target.name);
                potentialTargets.Remove(target);
                target.GetComponent<Bug>().Die();
            }

            GameManager.addScore(extraPoints);
            AudioManager.PlayRandomBugDeathSound();
        }
    }

    private IEnumerator DestroyKillingText(GameObject killingText) {
        if (killingText != null) {
            yield return new WaitForSeconds(1f);
            Destroy(killingText);
        }
    }
}
