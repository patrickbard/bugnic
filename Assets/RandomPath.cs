using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPath : MonoBehaviour {
    public List<PathCreator> paths;
    public PathCreator instantiatedPath;

    public PathCreator GetRandomPath() {
        return paths[Random.Range(0, paths.Count)];
    }

    public void CreateRandomPath() {
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, Random.Range(-360, 360)));
        instantiatedPath = Instantiate(GetRandomPath(), Vector3.zero, quaternion, transform);
        instantiatedPath.gameObject.SetActive(true);
    }

    private void Awake() {
        CreateRandomPath();
    }
}
