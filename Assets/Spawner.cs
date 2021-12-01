using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    public float spawnRadius;
    public List<GameObject> bugsPrefabs;
    public int absoluteMaxBugs;
    public int minBugs;
    public int spawnIncrement;

    [HideInInspector]
    public int maxBugs;

    [HideInInspector]
    public int bugCount;

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius * 2);
    }

    void Update() {
        int incrementCount = (GameManager.Score/200) + 1;
        int newMax = minBugs + incrementCount * spawnIncrement;
        maxBugs = (newMax <= absoluteMaxBugs) ? newMax : absoluteMaxBugs;
        
        if (bugCount < maxBugs) {
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * (spawnRadius * 2);
            GameObject randomEnemy = bugsPrefabs[Random.Range(0, bugsPrefabs.Count)];
            GameObject instantiatedBug = Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
            instantiatedBug.GetComponent<Bug>().spawner = this;
            bugCount++;
        }
    }
}