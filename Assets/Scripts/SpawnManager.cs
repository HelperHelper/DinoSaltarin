using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos;
    private int randomObstacle;
    private float starDelay = 2;
    private float repeatRate = 3;
    void Start()
    {
        InvokeRepeating("SpawnObstacle", starDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        if (PlayerController.Instance.gameOver == false)
        {
            spawnPos = new Vector3(30, 3.1f, 0);
            randomObstacle = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomObstacle], spawnPos, obstaclePrefabs[randomObstacle].transform.rotation);

        }
    }

}
