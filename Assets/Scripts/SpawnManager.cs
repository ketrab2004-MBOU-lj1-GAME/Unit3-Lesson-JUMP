using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabList;
    private Vector3 spawnPos = new Vector3(0,0,0);
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private PlayerController playerControllerSc;
    private void Start()
    {
        playerControllerSc = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("spawnObstacle", startDelay, repeatRate);
    }

    void spawnObstacle()
    {
        if (!playerControllerSc.gameOver)
        {
            GameObject chosenPrefab = prefabList[Random.Range(0, prefabList.Length)];
            Instantiate(chosenPrefab, transform.position + spawnPos, chosenPrefab.transform.rotation, transform);
        }
    }
}
