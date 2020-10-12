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
    }

    void Update()
    {
        if (!playerControllerSc.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x <= destroyLeft && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
