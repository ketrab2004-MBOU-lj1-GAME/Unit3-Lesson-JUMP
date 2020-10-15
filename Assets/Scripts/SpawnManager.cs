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
        //find player and set playerControllerScript to it's script
        InvokeRepeating("spawnObstacle", startDelay, repeatRate);
        //do spawnObstacle every repeatRate after startDelay
    }

    void spawnObstacle()
    {
        if (!playerControllerSc.gameOver) //not gameover
        {
            GameObject chosenPrefab = prefabList[Random.Range(0, prefabList.Length)];
            //choose prefab from list
            Instantiate(chosenPrefab, transform.position + spawnPos, chosenPrefab.transform.rotation, transform);
            //use chosen prefab and spawn with spawnPos as offset, and prefab's rotation
        }
    }
}
