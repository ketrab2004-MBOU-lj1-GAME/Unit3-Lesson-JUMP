using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMover : MonoBehaviour
{
    public float speed = 5f;
    private float destroyLeft = -10f;
    private PlayerController playerControllerSc;

    private void Start()
    {
        playerControllerSc = GameObject.Find("Player").GetComponent<PlayerController>();
        //find player and set playerControllerScript to it's script
    }

    void Update()
    {
        if (!playerControllerSc.gameOver) //not gameover
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            //move left based on speed and delta
        }

        if (transform.position.x <= destroyLeft && gameObject.CompareTag("Obstacle")) //more left than destroy distance and has tag obstacle
        {
            Destroy(gameObject);
            //destroy
        }
    }
}
