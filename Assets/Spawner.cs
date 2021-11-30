using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public float spawnRadius;
    public float maxBugs;
    public float bugCount;
    public List<GameObject> bugsPrefabs;

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius * 2);
    }

    void Update() {
        if (bugCount < maxBugs) {
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * (spawnRadius * 2);
            GameObject randomEnemy = bugsPrefabs[Random.Range(0, bugsPrefabs.Count)];
            GameObject instantiatedBug = Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
            instantiatedBug.GetComponent<Bug>().spawner = this;
            bugCount++;
        }
    }
}